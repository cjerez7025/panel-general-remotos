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
                    _logger.LogWarning("❌ Google Sheets connection failed: {Message}", connectionStatus.Message);
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error testing Google Sheets connection");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Prueba de sincronización de todas las hojas
        /// </summary>
        [HttpPost("sync-all")]
        public async Task<IActionResult> TestSyncAll()
        {
            try
            {
                _logger.LogInformation("Testing sync of all Google Sheets...");
                
                var syncResult = await _googleSheetsService.SyncAllSheetsAsync();
                
                var result = new
                {
                    success = syncResult.Success,
                    sheetsProcessed = syncResult.SheetsProcessed,
                    sheetsWithErrors = syncResult.SheetsWithErrors,
                    callRecordsUpdated = syncResult.CallRecordsUpdated,
                    duration = syncResult.Duration.TotalSeconds,
                    errors = syncResult.Errors.Select(e => new { 
                        sheetName = e.SheetName, 
                        message = e.Message,
                        errorType = e.ErrorType.ToString()
                    }),
                    syncDateTime = syncResult.SyncDateTime
                };

                if (syncResult.Success)
                {
                    _logger.LogInformation("✅ Sync completed successfully! Processed {SheetsProcessed} sheets", syncResult.SheetsProcessed);
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
                _logger.LogError(ex, "Error during sync test");
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
                _logger.LogError(ex, "Error getting sheets status");
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
                _logger.LogError(ex, "Error getting sync statistics");
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}