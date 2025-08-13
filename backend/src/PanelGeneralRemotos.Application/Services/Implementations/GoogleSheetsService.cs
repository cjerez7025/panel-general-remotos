using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PanelGeneralRemotos.Application.Services.Interfaces;
using PanelGeneralRemotos.Domain.Entities;
using PanelGeneralRemotos.Domain.Enums;
using System.Text.Json;

namespace PanelGeneralRemotos.Application.Services.Implementations
{
    public class GoogleSheetsService : IGoogleSheetsService
    {
        private readonly ILogger<GoogleSheetsService> _logger;
        private readonly IConfiguration _configuration;
        private SheetsService? _sheetsService;
        private readonly Dictionary<string, DateTime> _lastSyncTimes;
        private readonly Dictionary<string, List<CallRecordData>> _cachedData;
        private readonly object _lockObject = new object();

        // URLs reales identificadas del análisis
        private readonly Dictionary<string, GoogleSheetConfig> _sheetConfigurations = new()
        {
            { "ACHS_Remoto_8", new GoogleSheetConfig("1EB7VrcQNxJ5IsdhiJ9sieLmz-Q91rZTu6Xw81y1VPPA", "ACHS", "Remoto 8") },
            { "ACHS_Remoto_13", new GoogleSheetConfig("1hnfWQhOKxUUe8U6KvswiK3hAkmRDYQh627M_5P_C2pU", "ACHS", "Remoto 13") },
            { "ACHS_Remoto_12", new GoogleSheetConfig("1myPCMkeZuYQA3Krdwxw_IjL8Cmhik6eQjT9U60y6ZKc", "ACHS", "Remoto 12") },
            { "ACHS_Remoto_9", new GoogleSheetConfig("1RFf0bKBOZBI_6kNVdp6nQ6ZAd9glhhzHY-AKEUZHcnA", "ACHS", "Remoto 9") },
            { "CAS_Remoto_8", new GoogleSheetConfig("1tnqxKzX3ZS6rmtYohgdrCSY8q92St0dymOuCNlPsFXQ", "BANMEDICA", "Remoto 8") },
            { "CAS_Remoto_9", new GoogleSheetConfig("1vBYZkH8n8CT0_gTQcQtjYpMmm_A0jt6yY3eEkw9SMFE", "BANMEDICA", "Remoto 9") },
            { "CAS_Remoto_11", new GoogleSheetConfig("1rH596gWu0wmzyxsnnQL_S2njXK_oWDBg9zlCPUa8LSs", "BANMEDICA", "Remoto 11") },
            { "INTER_Remoto_7", new GoogleSheetConfig("1wMaZKsIdNoDUUgg50iXfzd1keUPMCa8uzLp17_xwl_g", "INTERCLINICA", "Remoto 7") },
            { "INTER_Remoto_12", new GoogleSheetConfig("19ZX8vybTpYMd5AP2BwXt4PwSMXKp41EiirVBxtdnwJ4", "INTERCLINICA", "Remoto 12") },
            { "Sanatorio_Remoto_7", new GoogleSheetConfig("1Dz8HzzeNLoUN3ttlAjD5seuQXAjuoXIt7gAhOBmEOrY", "Sanatorio Aleman", "Remoto 7") },
            { "Indisa_Remoto_11", new GoogleSheetConfig("1wAjVwT0idt4JCM4knDXjJgnHRLETIrpEPSZxwVl42Ow", "INDISA", "Remoto 11") }
        };

        public GoogleSheetsService(ILogger<GoogleSheetsService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _lastSyncTimes = new Dictionary<string, DateTime>();
            _cachedData = new Dictionary<string, List<CallRecordData>>();
            
            InitializeGoogleSheetsService();
        }

        private void InitializeGoogleSheetsService()
        {
            try
            {
                var credentialsPath = _configuration["GoogleSheets:CredentialsPath"];
                
                if (string.IsNullOrEmpty(credentialsPath) || !File.Exists(credentialsPath))
                {
                    _logger.LogError("Google Sheets credentials file not found at: {CredentialsPath}", credentialsPath);
                    return;
                }

                var credential = GoogleCredential.FromFile(credentialsPath)
                    .CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);

                _sheetsService = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = _configuration["GoogleSheets:ApplicationName"] ?? "Panel General Remotos"
                });

                _logger.LogInformation("Google Sheets Service initialized successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize Google Sheets Service");
            }
        }

        public async Task<SyncResult> SyncAllSheetsAsync(CancellationToken cancellationToken = default)
        {
            var startTime = DateTime.UtcNow;
            var result = new SyncResult
            {
                SyncDateTime = startTime,
                SheetsProcessed = 0,
                SheetsWithErrors = 0,
                CallRecordsUpdated = 0,
                PerformanceMetricsUpdated = 0
            };

            _logger.LogInformation("Starting sync of all {Count} Google Sheets", _sheetConfigurations.Count);

            try
            {
                var syncTasks = _sheetConfigurations.Select(async kvp => 
                {
                    try
                    {
                        await SyncSingleSheetAsync(kvp.Key, kvp.Value, cancellationToken);
                        return new { Success = true, SheetName = kvp.Key, Error = (SyncError?)null };
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to sync sheet {SheetName}", kvp.Key);
                        return new { 
                            Success = false, 
                            SheetName = kvp.Key, 
                            Error = (SyncError?)new SyncError
                            {
                                ConfigurationId = 0,
                                SheetName = kvp.Key,
                                ErrorType = SheetErrorType.ConnectionError,
                                Message = ex.Message,
                                Exception = ex.ToString()
                            }
                        };
                    }
                });

                var syncResults = await Task.WhenAll(syncTasks);
                
                result.SheetsProcessed = syncResults.Length;
                result.SheetsWithErrors = syncResults.Count(r => !r.Success);
                result.Errors = syncResults.Where(r => r.Error != null).Select(r => r.Error!).ToList();
                result.Success = result.SheetsWithErrors == 0;

                // Calcular registros actualizados
                lock (_lockObject)
                {
                    result.CallRecordsUpdated = _cachedData.Values.SelectMany(records => records).Count();
                }

                result.Duration = DateTime.UtcNow - startTime;

                _logger.LogInformation("Sync completed: {Successful}/{Total} sheets successful", 
                    result.SheetsProcessed - result.SheetsWithErrors, result.SheetsProcessed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Critical error during sync operation");
                result.Success = false;
                result.Errors.Add(new SyncError
                {
                    ConfigurationId = 0,
                    SheetName = "System",
                    ErrorType = SheetErrorType.ConnectionError,
                    Message = $"Critical sync error: {ex.Message}",
                    Exception = ex.ToString()
                });
            }

            return result;
        }

        public async Task<SyncResult> SyncSponsorDataAsync(int sponsorId, CancellationToken cancellationToken = default)
        {
            // Para simplificar, sincronizamos todas las hojas relacionadas con el sponsor
            // En una implementación más avanzada, mapearíamos sponsorId a hojas específicas
            return await SyncAllSheetsAsync(cancellationToken);
        }

        public async Task<SyncResult> SyncExecutiveDataAsync(int executiveId, CancellationToken cancellationToken = default)
        {
            // Similar al sponsor, sincronizamos todas las hojas
            // En futuras versiones se podría filtrar por ejecutivo específico
            return await SyncAllSheetsAsync(cancellationToken);
        }

        private async Task SyncSingleSheetAsync(string sheetName, GoogleSheetConfig config, CancellationToken cancellationToken)
        {
            if (_sheetsService == null)
            {
                throw new InvalidOperationException("Google Sheets Service not initialized");
            }

            _logger.LogDebug("Syncing sheet: {SheetName}", sheetName);

            var range = "A:T"; // Columnas A a T (20 columnas como en BBDD_REPORTE)
            
            var request = _sheetsService.Spreadsheets.Values.Get(config.SpreadsheetId, range);
            var response = await request.ExecuteAsync();

            if (response?.Values == null || response.Values.Count == 0)
            {
                _logger.LogWarning("No data found in sheet {SheetName}", sheetName);
                return;
            }

            var callRecords = ParseSheetData(response.Values, config);
            
            lock (_lockObject)
            {
                _cachedData[sheetName] = callRecords;
                _lastSyncTimes[sheetName] = DateTime.UtcNow;
            }

            _logger.LogInformation("Successfully synced {Count} records from sheet {SheetName}", 
                callRecords.Count, sheetName);
        }

        public async Task<SheetDataResult> ReadSheetDataAsync(GoogleSheetConfiguration configuration, CancellationToken cancellationToken = default)
        {
            try
            {
                if (_sheetsService == null)
                {
                    return new SheetDataResult
                    {
                        Success = false,
                        ErrorMessage = "Google Sheets Service not initialized",
                        ErrorType = SheetErrorType.ConnectionError
                    };
                }

                var range = $"{configuration.SheetName}!A:Z";
                var request = _sheetsService.Spreadsheets.Values.Get(configuration.SpreadsheetId, range);
                var response = await request.ExecuteAsync();

                if (response?.Values == null)
                {
                    return new SheetDataResult
                    {
                        Success = false,
                        ErrorMessage = "No data found in sheet",
                        ErrorType = SheetErrorType.MissingData
                    };
                }

                return new SheetDataResult
                {
                    Success = true,
                    RawData = response.Values.Select(row => row.ToArray()).ToArray(),
                    RowsRead = response.Values.Count,
                    Metadata = new SheetMetadata
                    {
                        Title = configuration.SheetName,
                        TotalRows = response.Values.Count,
                        TotalColumns = response.Values.FirstOrDefault()?.Count ?? 0,
                        Headers = response.Values.FirstOrDefault()?.Select(h => h?.ToString() ?? "").ToList() ?? new()
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading sheet data for {SheetName}", configuration.SheetName);
                return new SheetDataResult
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ErrorType = SheetErrorType.ConnectionError
                };
            }
        }

        public async Task<ConnectionStatus> CheckConnectionAsync(CancellationToken cancellationToken = default)
        {
            var startTime = DateTime.UtcNow;
            
            try
            {
                if (_sheetsService == null)
                {
                    return new ConnectionStatus
                    {
                        IsConnected = false,
                        Message = "Google Sheets Service not initialized",
                        ResponseTime = DateTime.UtcNow - startTime
                    };
                }

                // Probar con la primera hoja disponible
                var firstSheet = _sheetConfigurations.First();
                var request = _sheetsService.Spreadsheets.Get(firstSheet.Value.SpreadsheetId);
                var response = await request.ExecuteAsync();

                return new ConnectionStatus
                {
                    IsConnected = response != null,
                    Message = response != null ? "Connection successful" : "Connection failed",
                    ResponseTime = DateTime.UtcNow - startTime
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection test failed");
                return new ConnectionStatus
                {
                    IsConnected = false,
                    Message = $"Connection error: {ex.Message}",
                    ResponseTime = DateTime.UtcNow - startTime
                };
            }
        }

        public async Task<DateTime?> GetLastSyncDateAsync()
        {
            return await Task.FromResult<DateTime?>(_lastSyncTimes.Values.Any() ? _lastSyncTimes.Values.Max() : null);
        }

        public async Task<List<SheetStatusInfo>> GetAllSheetsStatusAsync()
        {
            return await Task.Run(() =>
            {
                var statusList = new List<SheetStatusInfo>();

                lock (_lockObject)
                {
                    int configId = 1;
                    foreach (var kvp in _sheetConfigurations)
                    {
                        var hasData = _cachedData.ContainsKey(kvp.Key);
                        var lastSync = _lastSyncTimes.ContainsKey(kvp.Key) ? _lastSyncTimes[kvp.Key] : (DateTime?)null;

                        statusList.Add(new SheetStatusInfo
                        {
                            ConfigurationId = configId++,
                            SheetName = kvp.Key,
                            SponsorName = kvp.Value.SponsorName,
                            ExecutiveName = kvp.Value.ExecutiveName,
                            Status = hasData ? SyncStatus.Success : SyncStatus.Failed,
                            LastSyncDate = lastSync,
                            ConsecutiveFailures = hasData ? 0 : 1
                        });
                    }
                }

                return statusList;
            });
        }

        public async Task<ValidationResult> ValidateSheetConfigurationAsync(GoogleSheetConfiguration configuration, CancellationToken cancellationToken = default)
        {
            var result = new ValidationResult { IsValid = false };

            try
            {
                if (_sheetsService == null)
                {
                    result.Errors.Add("Google Sheets Service not initialized");
                    return result;
                }

                var request = _sheetsService.Spreadsheets.Values.Get(configuration.SpreadsheetId, "1:1");
                var response = await request.ExecuteAsync();

                if (response?.Values == null || !response.Values.Any())
                {
                    result.Errors.Add("No headers found in sheet");
                    return result;
                }

                var headers = response.Values[0].Select(h => h?.ToString() ?? "").ToList();
                
                // Validar headers críticos
                var requiredHeaders = new[] { "ejecutivo", "sponsor", "estado" };
                var missingHeaders = requiredHeaders.Where(req => 
                    !headers.Any(h => h.ToLower().Contains(req.ToLower()))).ToList();

                if (missingHeaders.Any())
                {
                    result.Errors.AddRange(missingHeaders.Select(h => $"Missing required header: {h}"));
                }
                else
                {
                    result.IsValid = true;
                }

                result.SheetInfo = new SheetMetadata
                {
                    Title = configuration.SheetName,
                    Headers = headers,
                    TotalColumns = headers.Count
                };

            }
            catch (Exception ex)
            {
                result.Errors.Add($"Validation error: {ex.Message}");
                _logger.LogError(ex, "Failed to validate sheet configuration");
            }

            return result;
        }

        public async Task<ProcessedSheetData> ProcessSheetDataAsync(object[][] rawData, GoogleSheetConfiguration configuration)
        {
            var result = new ProcessedSheetData();

            try
            {
                if (rawData == null || rawData.Length < 2)
                {
                    return result; // No hay datos para procesar
                }

                var headers = rawData[0].Select(h => h?.ToString() ?? "").ToList();
                var columnMap = MapColumns(headers);

                for (int i = 1; i < rawData.Length; i++)
                {
                    try
                    {
                        var callRecord = ParseRowToCallRecordData(rawData[i], columnMap, configuration.SheetName);
                        if (callRecord != null)
                        {
                            // Nota: Aquí deberías convertir CallRecordData a CallRecord entidad
                            // result.CallRecords.Add(ConvertToEntity(callRecord));
                        }
                        result.ProcessedRows++;
                    }
                    catch (Exception ex)
                    {
                        result.ErrorRows++;
                        result.ProcessingErrors.Add($"Row {i}: {ex.Message}");
                        _logger.LogWarning(ex, "Failed to process row {RowIndex}", i);
                    }
                }
            }
            catch (Exception ex)
            {
                result.ProcessingErrors.Add($"Critical processing error: {ex.Message}");
                _logger.LogError(ex, "Critical error processing sheet data");
            }

            return result;
        }

        public async Task<SyncStatistics> GetSyncStatisticsAsync()
        {
            return await Task.Run(() =>
            {
                lock (_lockObject)
                {
                    return new SyncStatistics
                    {
                        LastSuccessfulSync = _lastSyncTimes.Values.Any() ? _lastSyncTimes.Values.Max() : null,
                        TotalSheets = _sheetConfigurations.Count,
                        SuccessfulSheets = _cachedData.Count,
                        FailedSheets = _sheetConfigurations.Count - _cachedData.Count,
                        TotalRecordsSyncedToday = _cachedData.Values.SelectMany(records => records)
                            .Count(r => r.CallDate.Date == DateTime.Today)
                    };
                }
            });
        }

        // Métodos helper privados
        private List<CallRecordData> ParseSheetData(IList<IList<object>> values, GoogleSheetConfig config)
        {
            var records = new List<CallRecordData>();
            
            if (values.Count < 2) return records;

            var headers = values[0].Select(h => h?.ToString() ?? "").ToList();
            var columnMap = MapColumns(headers);

            for (int i = 1; i < values.Count; i++)
            {
                var row = values[i];
                
                try
                {
                    var record = ParseRowToCallRecordData(row, columnMap, config.SponsorName);
                    if (record != null)
                    {
                        records.Add(record);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to parse row {RowIndex} in sheet {SheetName}", i, config.SponsorName);
                }
            }

            return records;
        }

        private Dictionary<string, int> MapColumns(List<string> headers)
        {
            var map = new Dictionary<string, int>();
            
            for (int i = 0; i < headers.Count; i++)
            {
                var header = headers[i].ToLower().Trim();
                
                if (header.Contains("sponsor")) map["sponsor"] = i;
                else if (header.Contains("ejecutivo")) map["ejecutivo"] = i;
                else if (header.Contains("equipo")) map["equipo"] = i;
                else if (header.Contains("rut")) map["rut_cliente"] = i;
                else if (header.Contains("nombre") && !header.Contains("ejecutivo")) map["nombre"] = i;
                else if (header.Contains("edad")) map["edad"] = i;
                else if (header.Contains("telefono_1") || (header.Contains("telefono") && !header.Contains("2"))) map["telefono_1"] = i;
                else if (header.Contains("telefono_2")) map["telefono_2"] = i;
                else if (header.Contains("correo") && !header.Contains("2")) map["correo"] = i;
                else if (header.Contains("correo2")) map["correo2"] = i;
                else if (header.Contains("fecha_llamada")) map["fecha_llamada"] = i;
                else if (header.Contains("fecha_compromiso")) map["fecha_compromiso"] = i;
                else if (header.Contains("estado") && !header.Contains("sub") && !header.Contains("compromiso")) map["estado"] = i;
                else if (header.Contains("sub_estado")) map["sub_estado"] = i;
                else if (header.Contains("nota")) map["nota_ejecutivo"] = i;
                else if (header.Contains("sucursal")) map["sucursal"] = i;
            }

            return map;
        }

        private CallRecordData? ParseRowToCallRecordData(IList<object> row, Dictionary<string, int> columnMap, string sheetName)
        {
            string GetValue(string columnKey) =>
                columnMap.ContainsKey(columnKey) && columnMap[columnKey] < row.Count
                    ? row[columnMap[columnKey]]?.ToString()?.Trim() ?? ""
                    : "";

            var ejecutivo = GetValue("ejecutivo");
            if (string.IsNullOrEmpty(ejecutivo)) return null;

            var callDate = ParseDate(GetValue("fecha_llamada"));
            if (!callDate.HasValue) callDate = DateTime.Today; // Default to today if no date

            var record = new CallRecordData
            {
                CallDate = callDate.Value,
                ExecutiveName = ejecutivo,
                SponsorName = GetValue("sponsor"),
                SheetName = sheetName,
                TotalCalls = 1, // Por ahora, cada fila representa una llamada
                Status = ParseCallStatus(GetValue("estado")),
                Notes = GetValue("nota_ejecutivo"),
                LastUpdated = DateTime.UtcNow
            };

            return record;
        }

        private DateTime? ParseDate(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) return null;

            var formats = new[]
            {
                "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "dd-MM-yyyy", "yyyy/MM/dd",
                "dd/MM/yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss"
            };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(dateString, format, null, 
                    System.Globalization.DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }

            if (DateTime.TryParse(dateString, out DateTime autoResult))
            {
                return autoResult;
            }

            return null;
        }

        private CallStatus ParseCallStatus(string status)
        {
            if (string.IsNullOrEmpty(status)) return CallStatus.Unknown;

            return status.ToLower().Trim() switch
            {
                "sin gestión" or "sin gestion" => CallStatus.NotManaged,
                "en gestión" or "en gestion" => CallStatus.InProgress,
                "sin interés" or "sin interes" => CallStatus.NotInterested,
                "no contactado" => CallStatus.NotContacted,
                "interesado" => CallStatus.Interested,
                "cerrado" => CallStatus.Closed,
                "contactado" => CallStatus.Contacted,
                _ => CallStatus.Unknown
            };
        }
    }

    // Clases helper para configuración y datos
    public class GoogleSheetConfig
    {
        public string SpreadsheetId { get; }
        public string SponsorName { get; }
        public string ExecutiveName { get; }

        public GoogleSheetConfig(string spreadsheetId, string sponsorName, string executiveName)
        {
            SpreadsheetId = spreadsheetId;
            SponsorName = sponsorName;
            ExecutiveName = executiveName;
        }
    }

    public class CallRecordData
    {
        public DateTime CallDate { get; set; }
        public string ExecutiveName { get; set; } = string.Empty;
        public string SponsorName { get; set; } = string.Empty;
        public string SheetName { get; set; } = string.Empty;
        public int TotalCalls { get; set; }
        public CallStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
    }
}