using Microsoft.Extensions.Logging;
using PanelGeneralRemotos.Application.Models.DTOs;
using PanelGeneralRemotos.Application.Services.Interfaces;
using PanelGeneralRemotos.Domain.Entities;
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

                // Obtener datos en paralelo para mejor rendimiento
                var tasks = new
                {
                    GetQuickStatsAsync(cancellationToken),
                    GetSystemAlertsAsync(null, cancellationToken),
                    GetSyncStatusAsync(cancellationToken)
                };

                var results = await Task.WhenAll(tasks);

                summary.QuickStats = results[0];
                summary.SystemAlerts = (List<SystemAlertDto>)results[1];
                summary.SyncStatus = (List<SyncStatusDto>)results[2];

                // Determinar si hay problemas críticos
                summary.HasCriticalIssues = summary.SystemAlerts.Any(a => a.Severity == AlertSeverity.Critical) ||
                                          summary.SyncStatus.Any(s => s.Status == SyncStatus.Failed);

                // Próxima actualización automática (30 minutos)
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

                // Simulación de datos basada en GoogleSheets (hasta tener datos reales)
                var quickStats = new QuickStatsDto
                {
                    TotalCallsToday = syncStats.TotalRecordsSyncedToday,
                    ActiveSponsors = sheetsStatus.Count(s => s.Status == SyncStatus.Success),
                    ProblematicSponsors = sheetsStatus.Count(s => s.Status == SyncStatus.Failed),
                    ContactedPercentage = CalculateContactedPercentage(syncStats.TotalRecordsSyncedToday),
                    GoalProgressPercentage = CalculateGoalProgress(syncStats.TotalRecordsSyncedToday),
                    LastUpdateTime = syncStats.LastSuccessfulSync ?? DateTime.UtcNow,
                    TrendIndicator = DetermineTrendIndicator(syncStats.TotalRecordsSyncedToday)
                };

                _logger.LogDebug("Quick stats calculated: {TotalCalls} calls, {ActiveSponsors} active sponsors", 
                    quickStats.TotalCallsToday, quickStats.ActiveSponsors);

                return quickStats;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating quick stats");
                return new QuickStatsDto
                {
                    LastUpdateTime = DateTime.UtcNow,
                    TrendIndicator = "stable"
                };
            }
        }

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
                        Id = Guid.NewGuid(),
                        Title = $"Sincronización fallida: {failedSheet.SheetName}",
                        Message = $"La hoja de {failedSheet.SponsorName} - {failedSheet.ExecutiveName} no se pudo sincronizar.",
                        Severity = failedSheet.ConsecutiveFailures > 3 ? AlertSeverity.Critical : AlertSeverity.Warning,
                        Category = "Sincronización",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false,
                        ActionRequired = true,
                        ActionUrl = $"/sheets/{failedSheet.ConfigurationId}"
                    });
                }

                // Alerta por baja actividad
                if (syncStats.TotalRecordsSyncedToday < 100)
                {
                    alerts.Add(new SystemAlertDto
                    {
                        Id = Guid.NewGuid(),
                        Title = "Baja actividad de llamadas",
                        Message = $"Solo {syncStats.TotalRecordsSyncedToday} llamadas registradas hoy. Meta esperada: 660 llamadas (11 ejecutivos × 60).",
                        Severity = AlertSeverity.Warning,
                        Category = "Rendimiento",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false,
                        ActionRequired = true
                    });
                }

                // Alerta por sincronización desactualizada
                var lastSync = syncStats.LastSuccessfulSync;
                if (lastSync.HasValue && DateTime.UtcNow.Subtract(lastSync.Value).TotalHours > 2)
                {
                    alerts.Add(new SystemAlertDto
                    {
                        Id = Guid.NewGuid(),
                        Title = "Sincronización desactualizada",
                        Message = $"Última sincronización exitosa: {lastSync.Value:dd/MM/yyyy HH:mm}",
                        Severity = AlertSeverity.Warning,
                        Category = "Sistema",
                        CreatedAt = DateTime.UtcNow,
                        IsRead = false,
                        ActionRequired = true
                    });
                }

                // Filtrar por severidad si se especifica
                if (severityFilter.HasValue)
                {
                    alerts = alerts.Where(a => a.Severity == severityFilter.Value).ToList();
                }

                _logger.LogDebug("Generated {AlertCount} system alerts", alerts.Count);

                return alerts.OrderByDescending(a => a.CreatedAt).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating system alerts");
                return new List<SystemAlertDto>();
            }
        }

        public async Task<DashboardRefreshResultDto> RefreshDashboardDataAsync(bool forceFullRefresh = false, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Refreshing dashboard data (force: {ForceRefresh})...", forceFullRefresh);

            var refreshResult = new DashboardRefreshResultDto
            {
                StartTime = DateTime.UtcNow,
                Success = false
            };

            try
            {
                // Sincronizar todas las hojas de Google Sheets
                var syncResult = await _googleSheetsService.SyncAllSheetsAsync(cancellationToken);
                
                refreshResult.SheetsRefreshed = syncResult.SheetsProcessed;
                refreshResult.SheetsWithErrors = syncResult.SheetsWithErrors;
                refreshResult.RecordsUpdated = syncResult.CallRecordsUpdated;
                refreshResult.Success = syncResult.Success;

                if (syncResult.Errors.Any())
                {
                    refreshResult.Errors = syncResult.Errors.Select(e => e.Message).ToList();
                }

                refreshResult.EndTime = DateTime.UtcNow;
                refreshResult.Duration = refreshResult.EndTime.Value.Subtract(refreshResult.StartTime);

                _logger.LogInformation("Dashboard refresh completed: {Success}, {SheetsRefreshed} sheets, {RecordsUpdated} records", 
                    refreshResult.Success, refreshResult.SheetsRefreshed, refreshResult.RecordsUpdated);

                return refreshResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during dashboard refresh");
                
                refreshResult.Success = false;
                refreshResult.Errors = new List<string> { ex.Message };
                refreshResult.EndTime = DateTime.UtcNow;
                refreshResult.Duration = refreshResult.EndTime.Value.Subtract(refreshResult.StartTime);

                return refreshResult;
            }
        }

        public async Task<CallsSummaryByDateDto> GetCallsSummaryByDateAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting calls summary from {StartDate} to {EndDate}", startDate, endDate);

            try
            {
                // Por ahora retornamos datos de ejemplo hasta tener la integración completa con CallRecords
                var summary = new CallsSummaryByDateDto
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    GeneratedAt = DateTime.UtcNow,
                    TotalCalls = 1250,
                    TotalSponsors = 5,
                    AverageCallsPerDay = 125
                };

                // Datos simulados por sponsor
                var sponsors = new[] { "ACHS", "INTERCLINICA", "BANMEDICA", "Sanatorio Aleman", "INDISA" };
                var random = new Random();

                summary.SponsorSummaries = sponsors.Select(sponsor => new SponsorCallsSummary
                {
                    SponsorName = sponsor,
                    TotalCalls = random.Next(200, 400),
                    DailyCalls = GenerateDailyCallsData(startDate, endDate, random),
                    AveragePerDay = random.Next(20, 50),
                    GoalPercentage = (decimal)(random.NextDouble() * 40 + 60) // 60-100%
                }).ToList();

                return summary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting calls summary by date");
                throw;
            }
        }

        public async Task<CallsDetailDto> GetCallsDetailBySponsorAsync(int sponsorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting calls detail for sponsor {SponsorId} from {StartDate} to {EndDate}", 
                sponsorId, startDate, endDate);

            try
            {
                // Datos simulados hasta tener integración completa
                var detail = new CallsDetailDto
                {
                    SponsorId = sponsorId,
                    SponsorName = $"Sponsor {sponsorId}",
                    StartDate = startDate,
                    EndDate = endDate,
                    GeneratedAt = DateTime.UtcNow
                };

                return detail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting calls detail for sponsor {SponsorId}", sponsorId);
                throw;
            }
        }

        public async Task<PerformanceSummary> GetPerformanceSummaryAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting performance summary from {StartDate} to {EndDate}", startDate, endDate);

            try
            {
                var summary = new PerformanceSummary
                {
                    DateRange = new PanelGeneralRemotos.Application.Models.DTOs.DateRange
                    {
                        StartDate = startDate,
                        EndDate = endDate
                    },
                    GeneratedAt = DateTime.UtcNow
                };

                return summary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting performance summary");
                throw;
            }
        }

        public async Task<PerformanceDetailBySponsor> GetPerformanceDetailBySponsorAsync(int sponsorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Getting performance detail for sponsor {SponsorId}", sponsorId);

            try
            {
                // Implementación pendiente
                return new PerformanceDetailBySponsor();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting performance detail for sponsor {SponsorId}", sponsorId);
                throw;
            }
        }

        public async Task<List<SyncStatusDto>> GetSyncStatusAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var sheetsStatus = await _googleSheetsService.GetAllSheetsStatusAsync();
                
                return sheetsStatus.Select(s => new SyncStatusDto
                {
                    SheetName = s.SheetName,
                    SponsorName = s.SponsorName,
                    ExecutiveName = s.ExecutiveName,
                    Status = s.Status,
                    LastSyncTime = s.LastSyncDate,
                    IsHealthy = s.Status == SyncStatus.Success && s.ConsecutiveFailures == 0,
                    ErrorMessage = s.LastErrorMessage
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sync status");
                return new List<SyncStatusDto>();
            }
        }

        public async Task<RealTimeMetricsDto> GetRealTimeMetricsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var syncStats = await _googleSheetsService.GetSyncStatisticsAsync();
                
                return new RealTimeMetricsDto
                {
                    TotalCallsToday = syncStats.TotalRecordsSyncedToday,
                    ActiveExecutives = syncStats.SuccessfulSheets,
                    SyncStatus = syncStats.FailedSheets == 0 ? SyncStatus.Success : SyncStatus.PartialSuccess,
                    LastUpdateTime = syncStats.LastSuccessfulSync ?? DateTime.UtcNow,
                    Timestamp = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting real-time metrics");
                throw;
            }
        }

        public async Task<bool> HasDataChangesAsync(DateTime lastUpdateTimestamp, CancellationToken cancellationToken = default)
        {
            try
            {
                var syncStats = await _googleSheetsService.GetSyncStatisticsAsync();
                return syncStats.LastSuccessfulSync > lastUpdateTimestamp;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking data changes");
                return false;
            }
        }

        // Métodos helper privados
        private decimal CalculateContactedPercentage(int totalCalls)
        {
            // Simulación: asumiendo ~70% de contactados
            return totalCalls > 0 ? Math.Round((decimal)(totalCalls * 0.7m / totalCalls * 100), 1) : 0;
        }

        private decimal CalculateGoalProgress(int totalCalls)
        {
            // Meta diaria: 660 llamadas (11 ejecutivos × 60 llamadas)
            const int dailyGoal = 660;
            return dailyGoal > 0 ? Math.Round((decimal)totalCalls / dailyGoal * 100, 1) : 0;
        }

        private string DetermineTrendIndicator(int totalCalls)
        {
            // Lógica simple para determinar tendencia
            return totalCalls switch
            {
                > 500 => "up",
                < 200 => "down",
                _ => "stable"
            };
        }

        private List<DailyCallsData> GenerateDailyCallsData(DateTime startDate, DateTime endDate, Random random)
        {
            var dailyData = new List<DailyCallsData>();
            
            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                dailyData.Add(new DailyCallsData
                {
                    Date = date,
                    CallCount = random.Next(40, 80),
                    Goal = 60,
                    CompletionPercentage = random.Next(60, 120)
                });
            }
            
            return dailyData;
        }

        // Métodos de la interfaz que requieren implementación completa
        public async Task<List<BreadcrumbDto>> GetNavigationBreadcrumbsAsync(string currentView, int? sponsorId = null, int? executiveId = null, CancellationToken cancellationToken = default)
        {
            // Implementación básica
            return new List<BreadcrumbDto>();
        }

        public async Task<DataValidationReportDto> ValidateDashboardDataIntegrityAsync(CancellationToken cancellationToken = default)
        {
            // Implementación básica
            return new DataValidationReportDto();
        }

        public async Task<SystemHealthStatus> GetSystemHealthAsync(CancellationToken cancellationToken = default)
        {
            // Implementación básica
            return new SystemHealthStatus();
        }

        public async Task<ReportDataDto> GenerateExportDataAsync(string exportType, string format, DateTime startDate, DateTime endDate, int? sponsorFilter = null, CancellationToken cancellationToken = default)
        {
            // Implementación básica
            return new ReportDataDto();
        }

        public async Task<DashboardConfigurationDto> GetDashboardConfigurationAsync(CancellationToken cancellationToken = default)
        {
            // Implementación básica
            return new DashboardConfigurationDto();
        }

        public async Task<bool> UpdateDashboardConfigurationAsync(DashboardConfigurationDto configuration, CancellationToken cancellationToken = default)
        {
            // Implementación básica
            return true;
        }
    }
}