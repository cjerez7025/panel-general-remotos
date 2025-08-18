// ============================================================================
// ARCHIVO COMPLETO CORREGIDO: TestController.cs
// backend/src/PanelGeneralRemotos.Api/Controllers/TestController.cs
// TODAS LAS CORRECCIONES APLICADAS - Eliminados errores async/await
// ============================================================================

using Microsoft.AspNetCore.Mvc;
using PanelGeneralRemotos.Application.Services.Interfaces;

namespace PanelGeneralRemotos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly ILogger<TestController> _logger;

        public TestController(IGoogleSheetsService googleSheetsService, ILogger<TestController> logger)
        {
            _googleSheetsService = googleSheetsService;
            _logger = logger;
        }

        /// <summary>
        /// Prueba de conexión con Google Sheets API
        /// </summary>
        [HttpGet("connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                _logger.LogInformation("Testing Google Sheets connection...");
                
                var connectionStatus = await _googleSheetsService.CheckConnectionAsync();
                
                var result = new
                {
                    success = connectionStatus.IsConnected,
                    message = connectionStatus.Message,
                    responseTime = connectionStatus.ResponseTime.TotalMilliseconds,
                    timestamp = connectionStatus.CheckDateTime
                };

                if (connectionStatus.IsConnected)
                {
                    _logger.LogInformation("✅ Google Sheets connection successful!");
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("⚠️ Google Sheets connection failed: {Message}", connectionStatus.Message);
                    return StatusCode(503, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error testing Google Sheets connection");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Prueba de sincronización completa
        /// </summary>
        [HttpPost("sync-all")]
        public async Task<IActionResult> TestSyncAll()
        {
            try
            {
                _logger.LogInformation("Testing full synchronization...");
                
                var syncResult = await _googleSheetsService.SyncAllSheetsAsync();
                
                var result = new
                {
                    success = syncResult.Success,
                    sheetsProcessed = syncResult.SheetsProcessed,
                    sheetsWithErrors = syncResult.SheetsWithErrors,
                    callRecordsUpdated = syncResult.CallRecordsUpdated,
                    performanceMetricsUpdated = syncResult.PerformanceMetricsUpdated,
                    duration = syncResult.Duration.TotalMilliseconds,
                    syncDateTime = syncResult.SyncDateTime,
                    errors = syncResult.Errors.Select(e => new
                    {
                        sheetName = e.SheetName,
                        errorType = e.ErrorType.ToString(),
                        message = e.Message
                    }).ToList(),
                    warnings = syncResult.Warnings
                };

                if (syncResult.Success)
                {
                    _logger.LogInformation("✅ Sync test completed successfully. Processed {SheetsProcessed} sheets", syncResult.SheetsProcessed);
                    return Ok(result);
                }
                else
                {
                    _logger.LogWarning("⚠️ Sync completed with {ErrorCount} errors", syncResult.SheetsWithErrors);
                    return Ok(result); // Still return OK but with error details
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error during sync test");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Obtener estado de todas las hojas
        /// </summary>
        [HttpGet("sheets-status")]
        public async Task<IActionResult> GetSheetsStatus()
        {
            try
            {
                _logger.LogInformation("Getting all sheets status...");
                
                var sheetsStatus = await _googleSheetsService.GetAllSheetsStatusAsync();
                
                var result = new
                {
                    totalSheets = sheetsStatus.Count,
                    successfulSheets = sheetsStatus.Count(s => s.Status == Domain.Enums.SyncStatus.Success),
                    failedSheets = sheetsStatus.Count(s => s.Status == Domain.Enums.SyncStatus.Failed),
                    sheets = sheetsStatus.Select(s => new
                    {
                        sheetName = s.SheetName,
                        sponsorName = s.SponsorName,
                        executiveName = s.ExecutiveName,
                        status = s.Status.ToString(),
                        lastSyncDate = s.LastSyncDate,
                        consecutiveFailures = s.ConsecutiveFailures
                    })
                };

                _logger.LogInformation("📊 Status retrieved for {TotalSheets} sheets", sheetsStatus.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting sheets status");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Estadísticas de sincronización
        /// </summary>
        [HttpGet("sync-stats")]
        public async Task<IActionResult> GetSyncStatistics()
        {
            try
            {
                var stats = await _googleSheetsService.GetSyncStatisticsAsync();
                
                var result = new
                {
                    lastSuccessfulSync = stats.LastSuccessfulSync,
                    totalSheets = stats.TotalSheets,
                    successfulSheets = stats.SuccessfulSheets,
                    failedSheets = stats.FailedSheets,
                    averageSyncDuration = stats.AverageSyncDuration.TotalSeconds,
                    totalRecordsSyncedToday = stats.TotalRecordsSyncedToday
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting sync statistics");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Validar configuración de una hoja específica
        /// </summary>
        [HttpPost("validate-sheet")]
        public async Task<IActionResult> ValidateSheetConfiguration([FromBody] ValidateSheetRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.SpreadsheetId) || string.IsNullOrEmpty(request.SheetName))
                {
                    return BadRequest(new { success = false, message = "SpreadsheetId y SheetName son requeridos" });
                }

                var configuration = new Domain.Entities.GoogleSheetConfiguration
                {
                    SpreadsheetId = request.SpreadsheetId,
                    SheetName = request.SheetName
                };

                var validationResult = await _googleSheetsService.ValidateSheetConfigurationAsync(configuration);

                var result = new
                {
                    success = validationResult.IsValid,
                    errors = validationResult.Errors,
                    warnings = validationResult.Warnings,
                    sheetInfo = validationResult.SheetInfo != null ? new
                    {
                        title = validationResult.SheetInfo.Title,
                        totalRows = validationResult.SheetInfo.TotalRows,
                        totalColumns = validationResult.SheetInfo.TotalColumns,
                        headers = validationResult.SheetInfo.Headers
                    } : null
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error validating sheet configuration");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// ✅ CORREGIDO: Endpoint de prueba del dashboard - SIN async
        /// </summary>
        [HttpGet("dashboard")]
        public Task<IActionResult> TestDashboard()
        {
            try
            {
                _logger.LogInformation("Testing dashboard functionality...");
                
                var result = new
                {
                    success = true,
                    message = "Dashboard test endpoint working",
                    timestamp = DateTime.UtcNow,
                    endpoints = new[]
                    {
                        "/api/test/connection",
                        "/api/test/sync-all", 
                        "/api/test/sheets-status",
                        "/api/test/sync-stats",
                        "/api/test/validate-sheet"
                    },
                    systemInfo = new
                    {
                        environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development",
                        dotnetVersion = Environment.Version.ToString(),
                        processId = Environment.ProcessId,
                        machineName = Environment.MachineName
                    }
                };

                // ✅ CORREGIDO: Usar Task.FromResult para evitar error async/await
                return Task.FromResult<IActionResult>(Ok(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error testing dashboard");
                // ✅ CORREGIDO: Usar Task.FromResult para evitar error async/await
                return Task.FromResult<IActionResult>(StatusCode(500, new { success = false, message = ex.Message }));
            }
        }

        /// <summary>
        /// Endpoint de estado del controlador
        /// </summary>
        [HttpGet("status")]
        public IActionResult GetControllerStatus()
        {
            return Ok(new
            {
                controller = "TestController",
                status = "Running",
                timestamp = DateTime.UtcNow,
                version = "1.0.0",
                availableEndpoints = new[]
                {
                    "GET /api/test/connection - Prueba conexión Google Sheets",
                    "POST /api/test/sync-all - Prueba sincronización completa",
                    "GET /api/test/sheets-status - Estado de todas las hojas",
                    "GET /api/test/sync-stats - Estadísticas de sincronización",
                    "POST /api/test/validate-sheet - Validar configuración de hoja",
                    "GET /api/test/dashboard - Prueba funcionalidad dashboard",
                    "GET /api/test/status - Estado del controlador"
                }
            });
        }

        /// <summary>
        /// Health check básico
        /// </summary>
        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new
            {
                status = "Healthy",
                timestamp = DateTime.UtcNow,
                uptime = TimeSpan.FromTicks(Environment.TickCount),
                checks = new
                {
                    googleSheetsService = _googleSheetsService != null ? "Available" : "Not Available",
                    logger = _logger != null ? "Available" : "Not Available"
                }
            });
        }
    }

    // ============================================================================
    // CLASES DE SOPORTE
    // ============================================================================

    /// <summary>
    /// Request para validar configuración de hoja
    /// </summary>
    public class ValidateSheetRequest
    {
        public string SpreadsheetId { get; set; } = string.Empty;
        public string SheetName { get; set; } = string.Empty;
        public string? SponsorName { get; set; }
    }
}