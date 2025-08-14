using Microsoft.AspNetCore.Mvc;
using PanelGeneralRemotos.Application.Services.Interfaces;
using PanelGeneralRemotos.Application.Models.DTOs;

namespace PanelGeneralRemotos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
        {
            _dashboardService = dashboardService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el resumen completo del dashboard
        /// </summary>
        [HttpGet("summary")]
        public async Task<ActionResult<DashboardSummary>> GetDashboardSummary(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Getting dashboard summary...");
                
                var summary = await _dashboardService.GetDashboardSummaryAsync(cancellationToken);
                
                _logger.LogInformation("✅ Dashboard summary retrieved successfully");
                return Ok(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting dashboard summary");
                return StatusCode(500, new { error = "Error interno del servidor", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene las estadísticas rápidas (Quick Stats)
        /// </summary>
        [HttpGet("quick-stats")]
        public async Task<ActionResult<QuickStatsDto>> GetQuickStats(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Getting quick stats...");
                
                var quickStats = await _dashboardService.GetQuickStatsAsync(cancellationToken);
                
                _logger.LogDebug("✅ Quick stats retrieved successfully");
                return Ok(quickStats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting quick stats");
                return StatusCode(500, new { error = "Error obteniendo estadísticas rápidas", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene las alertas del sistema
        /// </summary>
        [HttpGet("alerts")]
        public async Task<ActionResult<List<SystemAlertDto>>> GetSystemAlerts(
            [FromQuery] string? severity = null, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Getting system alerts with severity filter: {Severity}", severity ?? "ALL");
                
                // Convertir string a enum si se proporciona
                AlertSeverity? severityFilter = null;
                if (!string.IsNullOrEmpty(severity) && Enum.TryParse<AlertSeverity>(severity, true, out var parsedSeverity))
                {
                    severityFilter = parsedSeverity;
                }
                
                var alerts = await _dashboardService.GetSystemAlertsAsync(severityFilter, cancellationToken);
                
                _logger.LogDebug("✅ Retrieved {AlertCount} system alerts", alerts.Count);
                return Ok(alerts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting system alerts");
                return StatusCode(500, new { error = "Error obteniendo alertas del sistema", details = ex.Message });
            }
        }

        /// <summary>
        /// Fuerza la actualización del dashboard (botón "ACTUALIZAR")
        /// </summary>
        [HttpPost("refresh")]
        public async Task<ActionResult<DashboardRefreshResultDto>> RefreshDashboard(
            [FromQuery] bool forceFullRefresh = false,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("🔄 Starting dashboard refresh (forceFullRefresh: {ForceFullRefresh})", forceFullRefresh);
                
                var result = await _dashboardService.RefreshDashboardDataAsync(forceFullRefresh, cancellationToken);
                
                if (result.Success)
                {
                    _logger.LogInformation("✅ Dashboard refresh completed successfully in {Duration:F2} seconds", 
                        result.DurationSeconds);
                }
                else
                {
                    _logger.LogWarning("⚠️ Dashboard refresh completed with errors");
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error during dashboard refresh");
                return StatusCode(500, new { error = "Error actualizando dashboard", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene resumen de llamadas por fecha
        /// </summary>
        [HttpGet("calls-summary")]
        public async Task<ActionResult<CallsSummaryByDateDto>> GetCallsSummaryByDate(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Getting calls summary from {StartDate} to {EndDate}", startDate, endDate);
                
                if (startDate == default || endDate == default)
                {
                    return BadRequest(new { error = "startDate y endDate son requeridos" });
                }
                
                if (startDate > endDate)
                {
                    return BadRequest(new { error = "startDate no puede ser mayor que endDate" });
                }
                
                var summary = await _dashboardService.GetCallsSummaryByDateAsync(startDate, endDate, cancellationToken);
                
                _logger.LogDebug("✅ Calls summary retrieved successfully");
                return Ok(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting calls summary");
                return StatusCode(500, new { error = "Error obteniendo resumen de llamadas", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene detalle de llamadas por sponsor (drill-down)
        /// </summary>
        [HttpGet("calls-detail/{sponsorId}")]
        public async Task<ActionResult<CallsDetailDto>> GetCallsDetailBySponsor(
            int sponsorId,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Getting calls detail for sponsor {SponsorId} from {StartDate} to {EndDate}", 
                    sponsorId, startDate, endDate);
                
                if (sponsorId <= 0)
                {
                    return BadRequest(new { error = "sponsorId debe ser mayor que 0" });
                }
                
                if (startDate == default || endDate == default)
                {
                    return BadRequest(new { error = "startDate y endDate son requeridos" });
                }
                
                var detail = await _dashboardService.GetCallsDetailBySponsorAsync(sponsorId, startDate, endDate, cancellationToken);
                
                _logger.LogDebug("✅ Calls detail retrieved successfully for sponsor {SponsorId}", sponsorId);
                return Ok(detail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting calls detail for sponsor {SponsorId}", sponsorId);
                return StatusCode(500, new { error = "Error obteniendo detalle de llamadas", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene estado de sincronización
        /// </summary>
        [HttpGet("sync-status")]
        public async Task<ActionResult<List<SyncStatusDto>>> GetSyncStatus(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Getting sync status...");
                
                var status = await _dashboardService.GetSyncStatusAsync(cancellationToken);
                
                _logger.LogDebug("✅ Sync status retrieved successfully");
                return Ok(status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting sync status");
                return StatusCode(500, new { error = "Error obteniendo estado de sincronización", details = ex.Message });
            }
        }

        /// <summary>
        /// Verifica si hay cambios desde la última actualización
        /// </summary>
        [HttpGet("has-changes")]
        public async Task<ActionResult<bool>> HasDataChanges(
            [FromQuery] DateTime lastUpdateTimestamp,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Checking for data changes since {LastUpdateTimestamp}", lastUpdateTimestamp);
                
                var hasChanges = await _dashboardService.HasDataChangesAsync(lastUpdateTimestamp, cancellationToken);
                
                _logger.LogDebug("✅ Data changes check completed: {HasChanges}", hasChanges);
                return Ok(new { hasChanges, checkTime = DateTime.UtcNow });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error checking for data changes");
                return StatusCode(500, new { error = "Error verificando cambios", details = ex.Message });
            }
        }

        /// <summary>
        /// Endpoint de estado para verificar que el controlador funciona
        /// </summary>
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
                service = "Dashboard Controller",
                status = "Running",
                timestamp = DateTime.UtcNow,
                version = "1.0.0",
                endpoints = new[]
                {
                    "GET /api/dashboard/summary",
                    "GET /api/dashboard/quick-stats",
                    "GET /api/dashboard/alerts",
                    "POST /api/dashboard/refresh",
                    "GET /api/dashboard/calls-summary",
                    "GET /api/dashboard/calls-detail/{sponsorId}",
                    "GET /api/dashboard/sync-status",
                    "GET /api/dashboard/has-changes"
                }
            });
        }
    }
}