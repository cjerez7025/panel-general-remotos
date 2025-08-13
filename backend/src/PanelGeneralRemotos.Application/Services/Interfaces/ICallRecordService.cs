// ============================================================================
// Archivo: ICallRecordService.cs
// Propósito: Interface para gestión de registros de llamadas dinámicos
// Creado: 11/08/2025 - Initial creation para manejo de datos de llamadas
// Modificado: 11/08/2025 - Added real-time call data management and aggregation
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Application/Services/Interfaces/ICallRecordService.cs
// ============================================================================

using PanelGeneralRemotos.Domain.Entities;
using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Services.Interfaces;

/// <summary>
/// Servicio para gestión de registros de llamadas con datos dinámicos desde Google Sheets
/// Maneja la lógica de negocio para la pestaña "Llamadas por Fecha" del dashboard
/// </summary>
public interface ICallRecordService
{
    /// <summary>
    /// Obtiene todas las llamadas en un rango de fechas específico
    /// </summary>
    /// <param name="startDate">Fecha inicio del rango</param>
    /// <param name="endDate">Fecha fin del rango</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de registros de llamadas</returns>
    Task<List<CallRecord>> GetCallRecordsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene llamadas agrupadas por sponsor para la vista principal
    /// </summary>
    /// <param name="startDate">Fecha inicio</param>
    /// <param name="endDate">Fecha fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Llamadas agrupadas por sponsor</returns>
    Task<List<SponsorCallsSummary>> GetCallsSummaryBySponsorAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene llamadas detalladas por ejecutivos de un sponsor específico (drill-down)
    /// </summary>
    /// <param name="sponsorId">ID del sponsor</param>
    /// <param name="startDate">Fecha inicio</param>
    /// <param name="endDate">Fecha fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Detalle de llamadas por ejecutivo</returns>
    Task<CallsDetailBySponsor> GetCallsDetailBySponsorAsync(int sponsorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene llamadas de un ejecutivo específico
    /// </summary>
    /// <param name="executiveId">ID del ejecutivo</param>
    /// <param name="startDate">Fecha inicio</param>
    /// <param name="endDate">Fecha fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Llamadas del ejecutivo</returns>
    Task<List<CallRecord>> GetCallRecordsByExecutiveAsync(int executiveId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza o crea registros de llamadas desde datos de Google Sheets
    /// Método principal para la sincronización dinámica
    /// </summary>
    /// <param name="callRecords">Lista de registros a actualizar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la actualización</returns>
    Task<CallRecordsUpdateResult> UpdateCallRecordsAsync(List<CallRecord> callRecords, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene estadísticas de llamadas para un período específico
    /// </summary>
    /// <param name="timePeriod">Período de tiempo</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Estadísticas de llamadas</returns>
    Task<CallStatistics> GetCallStatisticsAsync(TimePeriod timePeriod, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el total de llamadas del día actual
    /// Para las estadísticas rápidas del dashboard
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Total de llamadas de hoy</returns>
    Task<int> GetTodayCallsTotalAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Calcula el cambio porcentual de llamadas respecto al día anterior
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Porcentaje de cambio (positivo o negativo)</returns>
    Task<decimal> GetCallsChangePercentageAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene llamadas que necesitan ser actualizadas (datos obsoletos)
    /// </summary>
    /// <param name="maxAgeMinutes">Edad máxima en minutos antes de considerar obsoleto</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de registros que necesitan actualización</returns>
    Task<List<CallRecord>> GetStaleCallRecordsAsync(int maxAgeMinutes = 30, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marca registros como no actualizados en la última sincronización
    /// Útil para identificar datos que ya no existen en Google Sheets
    /// </summary>
    /// <param name="syncDateTime">Fecha y hora de la sincronización</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Número de registros marcados</returns>
    Task<int> MarkRecordsAsNotUpdatedAsync(DateTime syncDateTime, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina registros obsoletos que no fueron actualizados en múltiples sincronizaciones
    /// </summary>
    /// <param name="daysOld">Días de antigüedad para considerar obsoleto</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Número de registros eliminados</returns>
    Task<int> CleanupObsoleteRecordsAsync(int daysOld = 7, CancellationToken cancellationToken = default);

    /// <summary>
    /// Valida la consistencia de los datos de llamadas
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la validación</returns>
    Task<DataValidationResult> ValidateCallRecordsConsistencyAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Resumen de llamadas por sponsor
/// </summary>
public class SponsorCallsSummary
{
    /// <summary>
    /// ID del sponsor
    /// </summary>
    public int SponsorId { get; set; }

    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Color hexadecimal del sponsor para la UI
    /// </summary>
    public string? ColorHex { get; set; }

    /// <summary>
    /// Estado del sponsor
    /// </summary>
    public SponsorStatus Status { get; set; }

    /// <summary>
    /// Llamadas por fecha (key: fecha, value: total llamadas)
    /// </summary>
    public Dictionary<DateTime, int> CallsByDate { get; set; } = new();

    /// <summary>
    /// Total de llamadas en el período
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta total para el período
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento de meta
    /// </summary>
    public decimal GoalPercentage { get; set; }

    /// <summary>
    /// Nivel de rendimiento del sponsor
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Número de ejecutivos activos
    /// </summary>
    public int ActiveExecutives { get; set; }

    /// <summary>
    /// Última fecha de actualización de datos
    /// </summary>
    public DateTime? LastDataUpdate { get; set; }

    /// <summary>
    /// Indica si hay problemas con los datos del sponsor
    /// </summary>
    public bool HasDataIssues { get; set; }

    /// <summary>
    /// Lista de problemas si los hay
    /// </summary>
    public List<string> DataIssues { get; set; } = new();
}

/// <summary>
/// Detalle de llamadas por sponsor (para drill-down)
/// </summary>
public class CallsDetailBySponsor
{
    /// <summary>
    /// Información del sponsor
    /// </summary>
    public SponsorInfo Sponsor { get; set; } = new();

    /// <summary>
    /// Rango de fechas de los datos
    /// </summary>
    public DateRange DateRange { get; set; } = new();

    /// <summary>
    /// Llamadas detalladas por ejecutivo
    /// </summary>
    public List<ExecutiveCallsDetail> ExecutiveDetails { get; set; } = new();

    /// <summary>
    /// Totales consolidados del sponsor
    /// </summary>
    public CallsTotals Totals { get; set; } = new();

    /// <summary>
    /// Fechas incluidas en el análisis
    /// </summary>
    public List<DateTime> IncludedDates { get; set; } = new();
}

/// <summary>
/// Detalle de llamadas por ejecutivo
/// </summary>
public class ExecutiveCallsDetail
{
    /// <summary>
    /// ID del ejecutivo
    /// </summary>
    public int ExecutiveId { get; set; }

    /// <summary>
    /// Nombre completo del ejecutivo
    /// </summary>
    public string ExecutiveName { get; set; } = string.Empty;

    /// <summary>
    /// Nombre corto/identificador
    /// </summary>
    public string ShortName { get; set; } = string.Empty;

    /// <summary>
    /// Indica si el ejecutivo está activo
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Llamadas por fecha
    /// </summary>
    public Dictionary<DateTime, int> CallsByDate { get; set; } = new();

    /// <summary>
    /// Total de llamadas del ejecutivo
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta diaria del ejecutivo
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Meta total para el período
    /// </summary>
    public int PeriodGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento
    /// </summary>
    public decimal GoalPercentage { get; set; }

    /// <summary>
    /// Nivel de rendimiento del ejecutivo
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Mejor día (más llamadas)
    /// </summary>
    public DateTime? BestDay { get; set; }

    /// <summary>
    /// Máximo de llamadas en un día
    /// </summary>
    public int MaxCallsInDay { get; set; }
}

/// <summary>
/// Totales consolidados de llamadas
/// </summary>
public class CallsTotals
{
    /// <summary>
    /// Total de llamadas
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta total
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento global
    /// </summary>
    public decimal GoalPercentage { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Día con más llamadas
    /// </summary>
    public DateTime? BestDay { get; set; }

    /// <summary>
    /// Máximo de llamadas en un día
    /// </summary>
    public int MaxCallsInDay { get; set; }

    /// <summary>
    /// Número de ejecutivos incluidos
    /// </summary>
    public int ExecutivesCount { get; set; }

    /// <summary>
    /// Número de días con datos
    /// </summary>
    public int DaysWithData { get; set; }
}

/// <summary>
/// Estadísticas de llamadas
/// </summary>
public class CallStatistics
{
    /// <summary>
    /// Período de las estadísticas
    /// </summary>
    public TimePeriod TimePeriod { get; set; }

    /// <summary>
    /// Fecha de inicio del período
    /// </summary>
    public DateTime PeriodStart { get; set; }

    /// <summary>
    /// Fecha de fin del período
    /// </summary>
    public DateTime PeriodEnd { get; set; }

    /// <summary>
    /// Total de llamadas en el período
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Tendencia (creciente, decreciente, estable)
    /// </summary>
    public string Trend { get; set; } = string.Empty;

    /// <summary>
    /// Comparación con período anterior
    /// </summary>
    public decimal ChangeFromPreviousPeriod { get; set; }

    /// <summary>
    /// Distribución por sponsors
    /// </summary>
    public List<SponsorCallsDistribution> SponsorDistribution { get; set; } = new();

    /// <summary>
    /// Días con mejor rendimiento
    /// </summary>
    public List<DateTime> TopPerformanceDays { get; set; } = new();
}

/// <summary>
/// Resultado de actualización de registros de llamadas
/// </summary>
public class CallRecordsUpdateResult
{
    /// <summary>
    /// Indica si la actualización fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Número de registros creados
    /// </summary>
    public int RecordsCreated { get; set; }

    /// <summary>
    /// Número de registros actualizados
    /// </summary>
    public int RecordsUpdated { get; set; }

    /// <summary>
    /// Número de registros sin cambios
    /// </summary>
    public int RecordsUnchanged { get; set; }

    /// <summary>
    /// Errores ocurridos durante la actualización
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Warnings generados
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Duración de la operación
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Fecha y hora de la actualización
    /// </summary>
    public DateTime UpdateDateTime { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Información básica del sponsor
/// </summary>
public class SponsorInfo
{
    /// <summary>
    /// ID del sponsor
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del sponsor
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Color hexadecimal para la UI
    /// </summary>
    public string? ColorHex { get; set; }

    /// <summary>
    /// Meta mensual del sponsor
    /// </summary>
    public int MonthlyGoal { get; set; }

    /// <summary>
    /// Meta diaria promedio
    /// </summary>
    public int DailyGoal { get; set; }
}

/// <summary>
/// Distribución de llamadas por sponsor
/// </summary>
public class SponsorCallsDistribution
{
    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Número de llamadas
    /// </summary>
    public int CallsCount { get; set; }

    /// <summary>
    /// Porcentaje del total
    /// </summary>
    public decimal Percentage { get; set; }
}

/// <summary>
/// Resultado de validación de datos
/// </summary>
public class DataValidationResult
{
    /// <summary>
    /// Indica si los datos son consistentes
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Errores de validación encontrados
    /// </summary>
    public List<string> ValidationErrors { get; set; } = new();

    /// <summary>
    /// Warnings de validación
    /// </summary>
    public List<string> ValidationWarnings { get; set; } = new();

    /// <summary>
    /// Estadísticas de validación
    /// </summary>
    public ValidationStatistics Statistics { get; set; } = new();
}

/// <summary>
/// Estadísticas de validación
/// </summary>
public class ValidationStatistics
{
    /// <summary>
    /// Total de registros validados
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Registros válidos
    /// </summary>
    public int ValidRecords { get; set; }

    /// <summary>
    /// Registros con errores
    /// </summary>
    public int InvalidRecords { get; set; }

    /// <summary>
    /// Registros duplicados
    /// </summary>
    public int DuplicateRecords { get; set; }

    /// <summary>
    /// Registros obsoletos
    /// </summary>
    public int ObsoleteRecords { get; set; }
}