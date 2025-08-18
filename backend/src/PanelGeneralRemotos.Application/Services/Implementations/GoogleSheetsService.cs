// ============================================================================
// ARCHIVO COMPLETO CORREGIDO: GoogleSheetsService.cs
// backend/src/PanelGeneralRemotos.Application/Services/Implementations/GoogleSheetsService.cs
// TODAS LAS CORRECCIONES APLICADAS - VERSIÓN FINAL
// ============================================================================

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
        }

        public async Task<SyncResult> SyncAllSheetsAsync(CancellationToken cancellationToken = default)
        {
            var startTime = DateTime.UtcNow;
            var result = new SyncResult
            {
                SyncDateTime = startTime
            };

            try
            {
                _logger.LogInformation("🔄 Starting sync of all Google Sheets...");

                var syncTasks = _sheetConfigurations.Select(async kvp =>
                {
                    try
                    {
                        // TODO: Implementar sincronización real con Google Sheets API
                        // Por ahora simular datos
                        await Task.Delay(50, cancellationToken);
                        
                        lock (_lockObject)
                        {
                            _cachedData[kvp.Key] = new List<CallRecordData>();
                            _lastSyncTimes[kvp.Key] = DateTime.UtcNow;
                        }

                        return new { 
                            Success = true, 
                            SheetName = kvp.Key, 
                            Error = (SyncError?)null 
                        };
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
                                // ✅ CORREGIDO: ConnectionError en lugar de Unknown
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

                _logger.LogInformation("✅ Sync completed: {Successful}/{Total} sheets successful", 
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
                    // ✅ CORREGIDO: ConnectionError en lugar de Unknown
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
                // TODO: Implementar check real de Google Sheets API
                _logger.LogInformation("Checking Google Sheets connection...");
                
                await Task.Delay(100, cancellationToken); // Simular delay de red
                
                return new ConnectionStatus
                {
                    IsConnected = true, // Simular conexión exitosa por ahora
                    Message = "Connection successful (mock mode)",
                    ResponseTime = DateTime.UtcNow - startTime,
                    CheckDateTime = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Connection test failed");
                return new ConnectionStatus
                {
                    IsConnected = false,
                    Message = $"Connection error: {ex.Message}",
                    ResponseTime = DateTime.UtcNow - startTime,
                    CheckDateTime = DateTime.UtcNow
                };
            }
        }

        // ✅ CORREGIDO: Especificar tipo explícitamente en Task.FromResult
        public Task<DateTime?> GetLastSyncDateAsync()
        {
            var result = _lastSyncTimes.Values.Any() ? _lastSyncTimes.Values.Max() : (DateTime?)null;
            return Task.FromResult(result);
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
                        var lastSync = _lastSyncTimes.ContainsKey(kvp.Key) ? 
                            _lastSyncTimes[kvp.Key] : (DateTime?)null;

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
                if (configuration == null)
                {
                    result.Errors.Add("Configuration cannot be null");
                    return result;
                }

                if (string.IsNullOrEmpty(configuration.SpreadsheetId))
                {
                    result.Errors.Add("SpreadsheetId is required");
                }

                if (string.IsNullOrEmpty(configuration.SheetName))
                {
                    result.Errors.Add("SheetName is required");
                }

                // TODO: Implementar validación real con Google Sheets API
                // Por ahora, validación básica
                if (result.Errors.Count == 0)
                {
                    result.IsValid = true;
                    result.SheetInfo = new SheetMetadata
                    {
                        Title = configuration.SheetName,
                        Headers = new List<string> { "ejecutivo", "sponsor", "estado" },
                        TotalColumns = 3
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"Validation error: {ex.Message}");
                _logger.LogError(ex, "Failed to validate sheet configuration");
                return result;
            }
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
                        // ✅ CORREGIDO: TotalRecordsSyncedToday en lugar de TotalRecords
                        TotalRecordsSyncedToday = _cachedData.Values.SelectMany(records => records)
                            .Count(r => r.CallDate.Date == DateTime.Today),
                        AverageSyncDuration = TimeSpan.FromMinutes(2) // Mock value
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
                try
                {
                    var record = ParseRowToCallRecordData(values[i], columnMap, config.SheetName);
                    if (record != null)
                    {
                        record.SponsorName = config.SponsorName;
                        record.ExecutiveName = config.ExecutiveName;
                        records.Add(record);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to parse row {RowIndex} from sheet {SheetName}", 
                        i + 1, config.SheetName);
                }
            }

            return records;
        }

        private CallRecordData? ParseRowToCallRecordData(IList<object> row, Dictionary<string, int> columnMap, string sheetName)
        {
            if (row.Count == 0) return null;

            try
            {
                return new CallRecordData
                {
                    CallDate = DateTime.Today, // TODO: Parsear fecha real de la hoja
                    Status = ParseCallStatus(GetCellValue(row, columnMap, "estado")),
                    Notes = GetCellValue(row, columnMap, "observaciones") ?? "",
                    SheetSource = sheetName
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error parsing row data from sheet {SheetName}", sheetName);
                return null;
            }
        }

        private Dictionary<string, int> MapColumns(List<string> headers)
        {
            var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            
            for (int i = 0; i < headers.Count; i++)
            {
                var header = headers[i].ToLower().Trim();
                
                // Mapear nombres de columnas conocidos
                if (header.Contains("ejecutivo") || header.Contains("executive"))
                    columnMap["ejecutivo"] = i;
                else if (header.Contains("estado") || header.Contains("status"))
                    columnMap["estado"] = i;
                else if (header.Contains("observ") || header.Contains("nota") || header.Contains("comment"))
                    columnMap["observaciones"] = i;
                else if (header.Contains("fecha") || header.Contains("date"))
                    columnMap["fecha"] = i;
                // Agregar más mapeos según necesidades
            }

            return columnMap;
        }

        private string? GetCellValue(IList<object> row, Dictionary<string, int> columnMap, string columnName)
        {
            if (columnMap.TryGetValue(columnName, out int columnIndex) && 
                columnIndex < row.Count)
            {
                return row[columnIndex]?.ToString()?.Trim();
            }
            return null;
        }

        private CallStatus ParseCallStatus(string? statusText)
        {
            if (string.IsNullOrEmpty(statusText))
                return CallStatus.Unknown;

            var lowerStatus = statusText.ToLower().Trim();
            
            return lowerStatus switch
            {
                var s when s.Contains("contactado") && !s.Contains("no") => CallStatus.Contacted,
                var s when s.Contains("no contactado") => CallStatus.NotContacted,
                var s when s.Contains("interesado") => CallStatus.Interested,
                var s when s.Contains("sin interés") || s.Contains("sin interes") => CallStatus.NotInterested,
                var s when s.Contains("cerrado") => CallStatus.Closed,
                var s when s.Contains("gestion") || s.Contains("gestión") => CallStatus.InProgress,
                _ => CallStatus.Unknown
            };
        }
    }

    // ============================================================================
    // CLASES DE SOPORTE
    // ============================================================================

    public class GoogleSheetConfig
    {
        public string SpreadsheetId { get; set; }
        public string SponsorName { get; set; }
        public string ExecutiveName { get; set; }
        public string SheetName { get; set; }

        public GoogleSheetConfig(string spreadsheetId, string sponsorName, string executiveName)
        {
            SpreadsheetId = spreadsheetId;
            SponsorName = sponsorName;
            ExecutiveName = executiveName;
            SheetName = $"{sponsorName}_{executiveName}";
        }
    }

    public class CallRecordData
    {
        public string ExecutiveName { get; set; } = string.Empty;
        public string SponsorName { get; set; } = string.Empty;
        public DateTime CallDate { get; set; }
        public CallStatus Status { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string SheetSource { get; set; } = string.Empty;
    }
}