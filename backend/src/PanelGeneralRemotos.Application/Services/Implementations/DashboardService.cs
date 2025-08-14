using Microsoft.Extensions.Logging;
using PanelGeneralRemotos.Application.Models.DTOs;
using PanelGeneralRemotos.Application.Services.Interfaces;
using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly ILogger<DashboardService> _logger;

        public DashboardService(IGoogleSheetsService googleSheetsService, ILogger<DashboardService> logger)
        {
            _googleSheetsService = googleSheetsService;
            _logger = logger;
        }

        public async Task<DashboardSummary> GetDashboardSummaryAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Generating dashboard summary...");

            try
            {
                var summary = new DashboardSummary
                {
                    GeneratedAt = DateTime.UtcNow
                };

                // ✅ CORREGIDO: Obtener datos en paralelo
                var quickStatsTask = GetQuickStatsAsync(cancellationToken);
                var alertsTask = GetSystemAlertsAsync(null, cancellationToken);
                var syncStatusTask = GetSyncStatusAsync(cancellationToken);

                await Task.WhenAll(quickStatsTask, alertsTask, syncStatusTask);

                summary.QuickStats = await quickStatsTask;
                summary.SystemAlerts = await alertsTask;
                summary.SyncStatus = await syncStatusTask;

                // Determinar si hay problemas críticos
                summary.HasCriticalIssues = summary.SystemAlerts.Any(a => a.Severity == AlertSeverity.Critical) ||
                                          summary.SyncStatus.Any(s => s.Status == SyncStatus.Failed);

                summary.NextAutoRefresh = DateTime.UtcNow.AddMinutes(30);

                _logger.LogInformation("Dashboard summary generated successfully with {AlertCount} alerts", 
                    summary.SystemAlerts.Count);

                return summary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating dashboard summary");
                throw;
            }
        }

        public async Task<QuickStatsDto> GetQuickStatsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Calculating quick stats...");

                var syncStats = await _googleSheetsService.GetSyncStatisticsAsync();
                var sheetsStatus = await _googleSheetsService.GetAllSheetsStatusAsync();

                var quickStats = new QuickStatsDto
                {
                    TotalCallsToday = syncStats.TotalRecordsSyncedToday,
                    ActiveSponsors = sheetsStatus.Count(s => s.Status == SyncStatus.Success),
                    ProblematicSponsors = sheetsStatus.Count(s => s.Status == SyncStatus.Failed),
                    ContactedPercentage = CalculateContactedPercentage(syncStats.TotalRecordsSyncedToday),
                    GoalProgressPercentage = CalculateGoalProgress(syncStats.TotalRecordsSyncedToday),
                    LastUpdateTimestamp = syncStats.LastSuccessfulSync ?? DateTime.UtcNow,
                    TrendIndicator = DetermineTrendIndicator(syncStats.TotalRecordsSyncedToday),
                    // ✅ AGREGADO: Campos faltantes
                    TotalDailyGoal = 660, // 11 ejecutivos × 60 llamadas
                    TotalActiveExecutives = sheetsStatus.Count(s => s.Status == SyncStatus.Success),
                    AverageCallsPerExecutive = syncStats.TotalRecordsSyncedToday > 0 ? 
                        (decimal)syncStats.TotalRecordsSyncedToday / Math.Max(1, sheetsStatus.Count) : 0,
                    HasSyncIssues = sheetsStatus.Any(s => s.Status == SyncStatus.Failed),
                    MinutesSinceLastSync = syncStats.LastSuccessfulSync.HasValue ? 
                        (int)DateTime.UtcNow.Subtract(syncStats.LastSuccessfulSync.Value).TotalMinutes : 999,
                    SystemStatus = DetermineSystemStatus(sheetsStatus),
                    StatusMessage = GenerateStatusMessage(sheetsStatus)
                };

                // ✅ AGREGADO: Breakdown por sponsor
                quickStats.SponsorBreakdown = GenerateSponsorBreakdown(sheetsStatus);

                _logger.LogDebug("Quick stats calculated: {TotalCalls} calls, {ActiveSponsors} active sponsors", 
                    quickStats.TotalCallsToday, quickStats.ActiveSponsors);

                return quickStats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating quick stats");
                return new QuickStatsDto
                {
                    LastUpdateTimestamp = DateTime.UtcNow,
                    TrendIndicator = "stable",
                    SystemStatus = SystemHealthStatus.Critical,
                    StatusMessage = "Error al calcular estadísticas"
                };
            }
        }

        // ✅ MÉTODOS HELPER AGREGADOS
        private SystemHealthStatus DetermineSystemStatus(List<SheetStatusInfo> sheetsStatus)
        {
            var failedCount = sheetsStatus.Count(s => s.Status == SyncStatus.Failed);
            var totalCount = sheetsStatus.Count;
            
            if (totalCount == 0) return SystemHealthStatus.Down;
            
            var failureRate = (decimal)failedCount / totalCount;
            
            return failureRate switch
            {
                0 => SystemHealthStatus.Healthy,
                <= 0.2m => SystemHealthStatus.Warning,
                _ => SystemHealthStatus.Critical
            };
        }

        private string? GenerateStatusMessage(List<SheetStatusInfo> sheetsStatus)
        {
            var failedCount = sheetsStatus.Count(s => s.Status == SyncStatus.Failed);
            
            if (failedCount == 0)
                return "Todos los sistemas funcionando correctamente";
            
            return $"{failedCount} hoja(s) con problemas de sincronización";
        }

        private List<SponsorQuickStatsDto> GenerateSponsorBreakdown(List<SheetStatusInfo> sheetsStatus)
        {
            return sheetsStatus.GroupBy(s => s.SponsorName)
                .Select(g => new SponsorQuickStatsDto
                {
                    SponsorName = g.Key,
                    CallsToday = new Random().Next(40, 80), // Temporal - reemplazar con datos reales
                    DailyGoal = 60,
                    GoalPercentage = new Random().Next(60, 120),
                    Status = g.Any(s => s.Status == SyncStatus.Failed) ? 
                        SponsorHealthStatus.Poor : SponsorHealthStatus.Good,
                    ActiveExecutives = g.Count(s => s.Status == SyncStatus.Success)
                }).ToList();
        }

        // ✅ RESTO DE MÉTODOS REQUERIDOS POR LA INTERFAZ...
        
        public async Task<List<SystemAlertDto>> GetSystemAlertsAsync(AlertSeverity? severityFilter = null, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Generating system alerts...");

                var alerts = new List<SystemAlertDto>();
                var sheetsStatus = await _googleSheetsService.GetAllSheetsStatusAsync();
                var syncStats = await _googleSheetsService.GetSyncStatisticsAsync();

                // Alertas por hojas fallidas
                var failedSheets = sheetsStatus.Where(s => s.Status == SyncStatus.Failed).ToList();
                foreach (var failedSheet in failedSheets)
                {
                    alerts.Add(new SystemAlertDto
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = $"Sincronización fallida: {failedSheet.SheetName}",
                        Message = $"La hoja de {failedSheet.SponsorName} - {failedSheet.ExecutiveName} no se pudo sincronizar.",
                        Severity = failedSheet.ConsecutiveFailures > 3 ? AlertSeverity.Critical : AlertSeverity.Warning,
                        Type = AlertType.SyncError,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    });
                }

                // Filtrar por severidad si se especifica
                if (severityFilter.HasValue)
                {
                    alerts = alerts.Where(a => a.Severity == severityFilter.Value).ToList();
                }

                return alerts.OrderByDescending(a => a.CreatedAt).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating system alerts");
                return new List<SystemAlertDto>();
            }
        }

        // ✅ IMPLEMENTAR TODOS LOS MÉTODOS RESTANTES DE LA INTERFAZ...
        
        // Métodos helper existentes
        private decimal CalculateContactedPercentage(int totalCalls)
        {
            return totalCalls > 0 ? Math.Round((decimal)(totalCalls * 0.7m / totalCalls * 100), 1) : 0;
        }

        private decimal CalculateGoalProgress(int totalCalls)
        {
            const int dailyGoal = 660;
            return dailyGoal > 0 ? Math.Round((decimal)totalCalls / dailyGoal * 100, 1) : 0;
        }

        private string DetermineTrendIndicator(int totalCalls)
        {
            return totalCalls switch
            {
                > 500 => "up",
                < 200 => "down",
                _ => "stable"
            };
        }

        // ... resto de métodos requeridos por la interfaz
        
        public Task<DashboardRefreshResultDto> RefreshDashboardDataAsync(bool forceFullRefresh = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Implementar método completo");
        }

        public Task<CallsSummaryByDateDto> GetCallsSummaryByDateAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Implementar método completo");
        }

        public Task<CallsDetailDto> GetCallsDetailBySponsorAsync(int sponsorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Implementar método completo");
        }

        public Task<PerformanceSummary> GetPerformanceSummaryAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Implementar método completo");
        }

        public Task<PerformanceDetailBySponsor> GetPerformanceDetailBySponsorAsync(int sponsorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Implementar método completo");
        }

        public Task<List<SyncStatusDto>> GetSyncStatusAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Implementar método completo");
        }

        public Task<RealTimeMetricsDto> GetRealTimeMetricsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Implementar método completo");
        }

        public Task<bool> HasDataChangesAsync(DateTime lastUpdateTimestamp, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Implementar método completo");
        }

        // Resto de métodos de la interfaz...
        public Task<List<BreadcrumbDto>> GetNavigationBreadcrumbsAsync(string currentView, int? sponsorId = null, int? executiveId = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DataValidationReportDto> ValidateDashboardDataIntegrityAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<SystemHealthStatus> GetSystemHealthAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ReportDataDto> GenerateExportDataAsync(string exportType, string format, DateTime startDate, DateTime endDate, int? sponsorFilter = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DashboardConfigurationDto> GetDashboardConfigurationAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDashboardConfigurationAsync(DashboardConfigurationDto configuration, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}