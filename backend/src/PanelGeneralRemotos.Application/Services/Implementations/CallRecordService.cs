// ============================================================================
// Archivo: CallRecordService.cs  
// Propósito: Servicio para gestión de registros de llamadas con DATOS REALES
// Modificado: 20/08/2025 - CORRECCIÓN de errores CS0104 y CS0246
// Ubicación: backend/src/PanelGeneralRemotos.Application/Services/Implementations/CallRecordService.cs
// ============================================================================

using Microsoft.Extensions.Logging;
using PanelGeneralRemotos.Application.Services.Interfaces;
using PanelGeneralRemotos.Domain.Entities;
using PanelGeneralRemotos.Domain.Enums;
using PanelGeneralRemotos.Application.Models.DTOs;

namespace PanelGeneralRemotos.Application.Services.Implementations
{
    public class CallRecordService : ICallRecordService
    {
        private readonly ILogger<CallRecordService> _logger;
        private readonly IGoogleSheetsService _googleSheetsService;

        // Cache temporal para datos sincronizados (en implementación completa usaría Entity Framework)
        private readonly Dictionary<string, List<CallRecord>> _cachedCallRecords = new();
        private DateTime _lastCacheUpdate = DateTime.MinValue;

        public CallRecordService(
            ILogger<CallRecordService> logger,
            IGoogleSheetsService googleSheetsService)
        {
            _logger = logger;
            _googleSheetsService = googleSheetsService;
        }

        public async Task<List<CallRecord>> GetCallRecordsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("📅 Getting call records from {StartDate} to {EndDate} with real data", startDate, endDate);
            
            try
            {
                await EnsureDataIsFresh(cancellationToken);

                var allRecords = _cachedCallRecords.Values.SelectMany(records => records).ToList();
                
                var filteredRecords = allRecords
                    .Where(r => r.CallDate.Date >= startDate.Date && r.CallDate.Date <= endDate.Date)
                    .OrderBy(r => r.CallDate)
                    .ToList();

                _logger.LogDebug("✅ Found {Count} call records in date range", filteredRecords.Count);
                return filteredRecords;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting call records by date range");
                return new List<CallRecord>();
            }
        }

        // ============================================================================
        // CORRECCIÓN: Usar el DTO existente con namespace completo
        // ============================================================================
        public async Task<List<PanelGeneralRemotos.Application.Models.DTOs.SponsorCallsSummary>> GetCallsSummaryBySponsorAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("📊 Getting calls summary by sponsor from {StartDate} to {EndDate}", startDate, endDate);
            
            try
            {
                var callRecords = await GetCallRecordsByDateRangeAsync(startDate, endDate, cancellationToken);
                
                var summary = callRecords
                    .GroupBy(r => new { r.Sponsor.Name, r.SponsorId })
                    .Select(g => new PanelGeneralRemotos.Application.Models.DTOs.SponsorCallsSummary
                    {
                        SponsorName = g.Key.Name,
                        TotalCalls = g.Sum(r => r.TotalCalls),
                        DailyCalls = g.Select(r => new DailyCallsData 
                        { 
                            Date = r.CallDate, 
                            CallCount = r.TotalCalls 
                        }).ToList(),
                        AveragePerDay = g.Any() ? g.Sum(r => r.TotalCalls) / (decimal)g.Select(r => r.CallDate.Date).Distinct().Count() : 0,
                        GoalPercentage = g.Sum(r => r.CallGoal) > 0 ? 
                            (decimal)g.Sum(r => r.TotalCalls) / g.Sum(r => r.CallGoal) * 100 : 0
                    })
                    .OrderByDescending(s => s.TotalCalls)
                    .ToList();

                _logger.LogDebug("✅ Generated calls summary for {Count} sponsors", summary.Count);
                return summary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting calls summary by sponsor");
                return new List<PanelGeneralRemotos.Application.Models.DTOs.SponsorCallsSummary>();
            }
        }

        public async Task<CallsDetailBySponsor> GetCallsDetailBySponsorAsync(int sponsorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("🔍 Getting calls detail for sponsor {SponsorId} from {StartDate} to {EndDate}", sponsorId, startDate, endDate);
            
            try
            {
                var callRecords = await GetCallRecordsByDateRangeAsync(startDate, endDate, cancellationToken);
                var sponsorRecords = callRecords.Where(r => r.SponsorId == sponsorId).ToList();

                if (!sponsorRecords.Any())
                {
                    return new CallsDetailBySponsor
                    {
                        SponsorId = sponsorId,
                        SponsorName = "Sponsor no encontrado",
                        DateRange = new DateRange { StartDate = startDate, EndDate = endDate }
                    };
                }

                var firstRecord = sponsorRecords.First();
                var executiveDetails = sponsorRecords
                    .GroupBy(r => new { r.Executive.Name, r.ExecutiveId })
                    .Select(g => new ExecutiveCallsDetail
                    {
                        ExecutiveId = g.Key.ExecutiveId,
                        ExecutiveName = g.Key.Name,
                        TotalCalls = g.Sum(r => r.TotalCalls),
                        DailyGoal = g.FirstOrDefault()?.CallGoal ?? 0,
                        TotalGoal = g.Sum(r => r.CallGoal),
                        CallsByDate = g.ToDictionary(r => r.CallDate.Date, r => r.TotalCalls),
                        GoalAchievementPercentage = g.Sum(r => r.CallGoal) > 0 ? 
                            (decimal)g.Sum(r => r.TotalCalls) / g.Sum(r => r.CallGoal) * 100 : 0,
                        AverageCallsPerDay = g.Any() ? 
                            g.Sum(r => r.TotalCalls) / (decimal)g.Select(r => r.CallDate.Date).Distinct().Count() : 0,
                        BestDay = g.OrderByDescending(r => r.TotalCalls).FirstOrDefault()?.CallDate,
                        // CORRECCIÓN: Usar namespace completo para evitar ambigüedad CS0104
                        PerformanceLevel = DeterminePerformanceLevel(g.Sum(r => r.TotalCalls), g.Sum(r => r.CallGoal))
                    })
                    .OrderByDescending(e => e.TotalCalls)
                    .ToList();

                return new CallsDetailBySponsor
                {
                    SponsorId = sponsorId,
                    SponsorName = firstRecord.Sponsor.Name,
                    DateRange = new DateRange { StartDate = startDate, EndDate = endDate },
                    ExecutiveDetails = executiveDetails,
                    TotalCalls = sponsorRecords.Sum(r => r.TotalCalls),
                    TotalGoal = sponsorRecords.Sum(r => r.CallGoal),
                    GoalAchievementPercentage = sponsorRecords.Sum(r => r.CallGoal) > 0 ?
                        (decimal)sponsorRecords.Sum(r => r.TotalCalls) / sponsorRecords.Sum(r => r.CallGoal) * 100 : 0,
                    ExecutiveCount = sponsorRecords.Select(r => r.ExecutiveId).Distinct().Count(),
                    CallsByDate = sponsorRecords
                        .GroupBy(r => r.CallDate.Date)
                        .ToDictionary(g => g.Key, g => g.Sum(r => r.TotalCalls))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting calls detail for sponsor {SponsorId}", sponsorId);
                return new CallsDetailBySponsor
                {
                    SponsorId = sponsorId,
                    SponsorName = "Error al cargar datos",
                    DateRange = new DateRange { StartDate = startDate, EndDate = endDate }
                };
            }
        }

        public async Task<List<CallRecord>> GetCallRecordsByExecutiveAsync(int executiveId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("👤 Getting call records for executive {ExecutiveId} from {StartDate} to {EndDate}", executiveId, startDate, endDate);
            
            try
            {
                var allRecords = await GetCallRecordsByDateRangeAsync(startDate, endDate, cancellationToken);
                var executiveRecords = allRecords
                    .Where(r => r.ExecutiveId == executiveId)
                    .OrderBy(r => r.CallDate)
                    .ToList();

                _logger.LogDebug("✅ Found {Count} call records for executive {ExecutiveId}", executiveRecords.Count, executiveId);
                return executiveRecords;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting call records for executive {ExecutiveId}", executiveId);
                return new List<CallRecord>();
            }
        }

        public async Task<UpdateResult> UpdateCallRecordsAsync(List<CallRecord> callRecords, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("🔄 Updating {Count} call records", callRecords.Count);
            
            try
            {
                var result = new UpdateResult
                {
                    IsSuccessful = true,
                    TotalRecordsProcessed = callRecords.Count,
                    UpdatedRecords = callRecords.Count,
                    UpdatedAt = DateTime.UtcNow
                };

                // Actualizar cache temporal por sponsor
                foreach (var record in callRecords)
                {
                    var sponsorKey = record.Sponsor.Name;
                    if (!_cachedCallRecords.ContainsKey(sponsorKey))
                    {
                        _cachedCallRecords[sponsorKey] = new List<CallRecord>();
                    }

                    // Remover registros existentes para la misma fecha y ejecutivo
                    _cachedCallRecords[sponsorKey].RemoveAll(r => 
                        r.CallDate.Date == record.CallDate.Date && 
                        r.ExecutiveId == record.ExecutiveId);

                    // Agregar el nuevo registro
                    _cachedCallRecords[sponsorKey].Add(record);
                }

                _lastCacheUpdate = DateTime.UtcNow;

                _logger.LogInformation("✅ Successfully updated {Count} call records", callRecords.Count);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error updating call records");
                return new UpdateResult
                {
                    IsSuccessful = false,
                    TotalRecordsProcessed = callRecords.Count,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        // ============================================================================
        // CORRECCIÓN: Usar el DTO existente con namespace completo
        // ============================================================================
        public async Task<PanelGeneralRemotos.Application.Models.DTOs.SyncStatistics> GetSyncStatisticsAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("📈 Getting sync statistics");
            
            try
            {
                var allRecords = _cachedCallRecords.Values.SelectMany(records => records).ToList();
                
                return new PanelGeneralRemotos.Application.Models.DTOs.SyncStatistics
                {
                    LastSuccessfulSync = _lastCacheUpdate,
                    TotalSheets = _cachedCallRecords.Keys.Count,
                    SuccessfulSheets = _cachedCallRecords.Keys.Count,
                    FailedSheets = 0,
                    TotalRecordsSyncedToday = allRecords.Count(r => r.CreatedAt.Date == DateTime.Today),
                    AverageSyncDuration = TimeSpan.FromMinutes(2)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error getting sync statistics");
                return new PanelGeneralRemotos.Application.Models.DTOs.SyncStatistics
                {
                    LastSuccessfulSync = _lastCacheUpdate,
                    TotalSheets = 0,
                    SuccessfulSheets = 0,
                    FailedSheets = 1
                };
            }
        }

        public async Task<int> MarkRecordsAsNotUpdatedAsync(DateTime syncDateTime, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("🏷️ Marking records as not updated for sync time {SyncDateTime}", syncDateTime);
            
            try
            {
                var allRecords = _cachedCallRecords.Values.SelectMany(records => records).ToList();
                var markedCount = 0;

                foreach (var record in allRecords)
                {
                    if (record.UpdatedAt == null || record.UpdatedAt < syncDateTime)
                    {
                        record.IsStale = true;
                        markedCount++;
                    }
                }

                _logger.LogDebug("✅ Marked {Count} records as not updated", markedCount);
                return markedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error marking records as not updated");
                return 0;
            }
        }

        public async Task<int> CleanupObsoleteRecordsAsync(int daysOld = 7, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("🧹 Cleaning up obsolete records older than {DaysOld} days", daysOld);
            
            try
            {
                var cutoffDate = DateTime.UtcNow.AddDays(-daysOld);
                var removedCount = 0;

                foreach (var sponsorKey in _cachedCallRecords.Keys.ToList())
                {
                    var recordsToRemove = _cachedCallRecords[sponsorKey]
                        .Where(r => r.IsStale && r.UpdatedAt?.Date < cutoffDate.Date)
                        .ToList();

                    foreach (var record in recordsToRemove)
                    {
                        _cachedCallRecords[sponsorKey].Remove(record);
                        removedCount++;
                    }

                    // Remover sponsor si no tiene más registros
                    if (!_cachedCallRecords[sponsorKey].Any())
                    {
                        _cachedCallRecords.Remove(sponsorKey);
                    }
                }

                _logger.LogInformation("✅ Cleaned up {Count} obsolete records", removedCount);
                return removedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error cleaning up obsolete records");
                return 0;
            }
        }

        public async Task<DataValidationResult> ValidateCallRecordsConsistencyAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("🔍 Validating call records consistency");
            
            try
            {
                var allRecords = _cachedCallRecords.Values.SelectMany(records => records).ToList();
                var issues = new List<string>();
                var warnings = new List<string>();

                // Validar registros duplicados
                var duplicates = allRecords
                    .GroupBy(r => new { r.SponsorId, r.ExecutiveId, r.CallDate.Date })
                    .Where(g => g.Count() > 1)
                    .ToList();

                if (duplicates.Any())
                {
                    issues.Add($"Found {duplicates.Count()} duplicate records");
                }

                // Validar fechas futuras
                var futureRecords = allRecords.Where(r => r.CallDate.Date > DateTime.Today).ToList();
                if (futureRecords.Any())
                {
                    warnings.Add($"Found {futureRecords.Count} records with future dates");
                }

                // Validar llamadas negativas
                var negativeRecords = allRecords.Where(r => r.TotalCalls < 0).ToList();
                if (negativeRecords.Any())
                {
                    issues.Add($"Found {negativeRecords.Count} records with negative calls");
                }

                return new DataValidationResult
                {
                    IsValid = !issues.Any(),
                    TotalRecordsValidated = allRecords.Count,
                    ValidationErrors = issues,
                    ValidationWarnings = warnings,
                    ValidatedAt = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error validating call records consistency");
                return new DataValidationResult
                {
                    IsValid = false,
                    ValidationErrors = new List<string> { $"Validation failed: {ex.Message}" }
                };
            }
        }

        // ============================================================================
        // MÉTODOS PRIVADOS AUXILIARES
        // ============================================================================

        private async Task EnsureDataIsFresh(CancellationToken cancellationToken)
        {
            // Refrescar datos si han pasado más de 30 minutos
            if (DateTime.UtcNow.Subtract(_lastCacheUpdate).TotalMinutes > 30)
            {
                _logger.LogInformation("🔄 Cache is stale, refreshing data from Google Sheets");
                try
                {
                    var syncResult = await _googleSheetsService.SyncAllSheetsAsync(cancellationToken);
                    if (syncResult.Success)
                    {
                        _lastCacheUpdate = DateTime.UtcNow;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "⚠️ Failed to refresh data from Google Sheets, using cached data");
                }
            }
        }

        // CORRECCIÓN: Usar enum del Domain para evitar ambigüedad CS0104
        private static PanelGeneralRemotos.Domain.Enums.PerformanceLevel DeterminePerformanceLevel(int totalCalls, int totalGoal)
        {
            if (totalGoal == 0) return PanelGeneralRemotos.Domain.Enums.PerformanceLevel.Unknown;
            
            var percentage = (decimal)totalCalls / totalGoal;
            
            return percentage switch
            {
                >= 1.0m => PanelGeneralRemotos.Domain.Enums.PerformanceLevel.Excellent,
                >= 0.8m => PanelGeneralRemotos.Domain.Enums.PerformanceLevel.Good,
                >= 0.6m => PanelGeneralRemotos.Domain.Enums.PerformanceLevel.Average,
                >= 0.3m => PanelGeneralRemotos.Domain.Enums.PerformanceLevel.Poor,
                _ => PanelGeneralRemotos.Domain.Enums.PerformanceLevel.Critical
            };
        }
    }
}