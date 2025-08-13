// ============================================================================
// Archivo: CallRecordDto.cs
// Propósito: DTO para registros individuales de llamadas
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/CallRecordDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para registro individual de llamada
/// Representa una llamada específica de un ejecutivo en una fecha determinada
/// </summary>
public class CallRecordDto
{
    /// <summary>
    /// ID único del registro de llamada
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// ID del sponsor
    /// </summary>
    public int SponsorId { get; set; }

    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// ID del ejecutivo
    /// </summary>
    public int ExecutiveId { get; set; }

    /// <summary>
    /// Nombre del ejecutivo
    /// </summary>
    public string ExecutiveName { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de la llamada
    /// </summary>
    public DateTime CallDate { get; set; }

    /// <summary>
    /// Número de llamadas realizadas
    /// </summary>
    public int CallCount { get; set; }

    /// <summary>
    /// Meta del ejecutivo para esa fecha
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento para esa fecha
    /// </summary>
    public decimal CompliancePercentage { get; set; }

    /// <summary>
    /// Hora de la primera llamada del día
    /// </summary>
    public TimeSpan? FirstCallTime { get; set; }

    /// <summary>
    /// Hora de la última llamada del día
    /// </summary>
    public TimeSpan? LastCallTime { get; set; }

    /// <summary>
    /// Duración total de trabajo en horas
    /// </summary>
    public decimal? WorkingHours { get; set; }

    /// <summary>
    /// Promedio de llamadas por hora
    /// </summary>
    public decimal? CallsPerHour { get; set; }

    /// <summary>
    /// Calidad de las llamadas (si está disponible)
    /// </summary>
    public CallQualityDto? CallQuality { get; set; }

    /// <summary>
    /// Resultados de las llamadas
    /// </summary>
    public CallResultsDto? CallResults { get; set; }

    /// <summary>
    /// Timestamp de cuando se creó el registro
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp de la última actualización
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Indica si los datos fueron importados desde Google Sheets
    /// </summary>
    public bool IsImported { get; set; }

    /// <summary>
    /// ID de la fila en Google Sheets (si aplica)
    /// </summary>
    public string? GoogleSheetRowId { get; set; }

    /// <summary>
    /// Hash de los datos para detectar cambios
    /// </summary>
    public string? DataHash { get; set; }

    /// <summary>
    /// Estado del registro
    /// </summary>
    public CallRecordStatus Status { get; set; }

    /// <summary>
    /// Notas adicionales sobre el registro
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Alertas relacionadas con este registro
    /// </summary>
    public List<string> Alerts { get; set; } = new();

    /// <summary>
    /// Indicadores de rendimiento
    /// </summary>
    public PerformanceIndicatorsDto PerformanceIndicators { get; set; } = new();

    /// <summary>
    /// Metadata adicional
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Calidad de las llamadas
/// </summary>
public class CallQualityDto
{
    /// <summary>
    /// Score de calidad general (0-100)
    /// </summary>
    public decimal QualityScore { get; set; }

    /// <summary>
    /// Porcentaje de llamadas efectivas
    /// </summary>
    public decimal EffectiveCallsPercentage { get; set; }

    /// <summary>
    /// Tiempo promedio por llamada (minutos)
    /// </summary>
    public decimal AverageCallDuration { get; set; }

    /// <summary>
    /// Tasa de abandono de llamadas
    /// </summary>
    public decimal CallAbandonRate { get; set; }

    /// <summary>
    /// Satisfacción del cliente (si está disponible)
    /// </summary>
    public decimal? CustomerSatisfaction { get; set; }

    /// <summary>
    /// Comentarios sobre la calidad
    /// </summary>
    public string? QualityComments { get; set; }
}

/// <summary>
/// Resultados de las llamadas
/// </summary>
public class CallResultsDto
{
    /// <summary>
    /// Número de contactos exitosos
    /// </summary>
    public int SuccessfulContacts { get; set; }

    /// <summary>
    /// Número de llamadas no contestadas
    /// </summary>
    public int NoAnswers { get; set; }

    /// <summary>
    /// Número de líneas ocupadas
    /// </summary>
    public int BusyLines { get; set; }

    /// <summary>
    /// Número de llamadas con respuesta de contestadora
    /// </summary>
    public int VoicemailResponses { get; set; }

    /// <summary>
    /// Número de números incorrectos o desconectados
    /// </summary>
    public int WrongNumbers { get; set; }

    /// <summary>
    /// Número de personas interesadas
    /// </summary>
    public int InterestedContacts { get; set; }

    /// <summary>
    /// Número de ventas cerradas
    /// </summary>
    public int ClosedSales { get; set; }

    /// <summary>
    /// Número de citas programadas
    /// </summary>
    public int ScheduledAppointments { get; set; }

    /// <summary>
    /// Número de seguimientos programados
    /// </summary>
    public int ScheduledFollowUps { get; set; }

    /// <summary>
    /// Valor total de ventas (si aplica)
    /// </summary>
    public decimal? TotalSalesValue { get; set; }

    /// <summary>
    /// Moneda de las ventas
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// Tasa de conversión (%)
    /// </summary>
    public decimal ConversionRate { get; set; }

    /// <summary>
    /// Tasa de interés (%)
    /// </summary>
    public decimal InterestRate { get; set; }

    /// <summary>
    /// Tasa de cierre (%)
    /// </summary>
    public decimal CloseRate { get; set; }
}

/// <summary>
/// Indicadores de rendimiento del registro
/// </summary>
public class PerformanceIndicatorsDto
{
    /// <summary>
    /// Indica si se cumplió la meta del día
    /// </summary>
    public bool GoalAchieved { get; set; }

    /// <summary>
    /// Indica si fue el mejor día del ejecutivo
    /// </summary>
    public bool IsBestDay { get; set; }

    /// <summary>
    /// Indica si fue un día de bajo rendimiento
    /// </summary>
    public bool IsLowPerformance { get; set; }

    /// <summary>
    /// Ranking del día vs otros días del ejecutivo
    /// </summary>
    public int DayRanking { get; set; }

    /// <summary>
    /// Porcentaje respecto al promedio del ejecutivo
    /// </summary>
    public decimal PercentageVsAverage { get; set; }

    /// <summary>
    /// Porcentaje respecto al mejor día del ejecutivo
    /// </summary>
    public decimal PercentageVsBestDay { get; set; }

    /// <summary>
    /// Nivel de consistencia (qué tan típico es este resultado)
    /// </summary>
    public decimal ConsistencyLevel { get; set; }

    /// <summary>
    /// Tendencia del ejecutivo en esta fecha
    /// </summary>
    public string TrendAtDate { get; set; } = string.Empty;

    /// <summary>
    /// Score de productividad para el día
    /// </summary>
    public decimal ProductivityScore { get; set; }

    /// <summary>
    /// Score de eficiencia para el día
    /// </summary>
    public decimal EfficiencyScore { get; set; }
}

/// <summary>
/// Estado del registro de llamada
/// </summary>
public enum CallRecordStatus
{
    /// <summary>
    /// Registro válido y completo
    /// </summary>
    Valid,

    /// <summary>
    /// Registro pendiente de validación
    /// </summary>
    Pending,

    /// <summary>
    /// Registro con datos incompletos
    /// </summary>
    Incomplete,

    /// <summary>
    /// Registro con errores de validación
    /// </summary>
    Invalid,

    /// <summary>
    /// Registro duplicado
    /// </summary>
    Duplicate,

    /// <summary>
    /// Registro marcado para revisión
    /// </summary>
    UnderReview,

    /// <summary>
    /// Registro obsoleto
    /// </summary>
    Obsolete,

    /// <summary>
    /// Registro eliminado (soft delete)
    /// </summary>
    Deleted
}

/// <summary>
/// Resumen de registros de llamadas
/// </summary>
public class CallRecordsSummaryDto
{
    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Registros válidos
    /// </summary>
    public int ValidRecords { get; set; }

    /// <summary>
    /// Registros con problemas
    /// </summary>
    public int ProblematicRecords { get; set; }

    /// <summary>
    /// Última fecha de datos
    /// </summary>
    public DateTime? LastDataDate { get; set; }

    /// <summary>
    /// Primera fecha de datos
    /// </summary>
    public DateTime? FirstDataDate { get; set; }

    /// <summary>
    /// Total de llamadas en el período
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Sponsors representados
    /// </summary>
    public int UniqueSponsors { get; set; }

    /// <summary>
    /// Ejecutivos representados
    /// </summary>
    public int UniqueExecutives { get; set; }

    /// <summary>
    /// Días con datos
    /// </summary>
    public int DaysWithData { get; set; }

    /// <summary>
    /// Porcentaje de días con meta cumplida
    /// </summary>
    public decimal GoalAchievementRate { get; set; }

    /// <summary>
    /// Distribución por estado
    /// </summary>
    public Dictionary<CallRecordStatus, int> StatusDistribution { get; set; } = new();
}

/// <summary>
/// Filtros para búsqueda de registros de llamadas
/// </summary>
public class CallRecordFiltersDto
{
    /// <summary>
    /// Sponsor específico
    /// </summary>
    public int? SponsorId { get; set; }

    /// <summary>
    /// Ejecutivo específico
    /// </summary>
    public int? ExecutiveId { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Número mínimo de llamadas
    /// </summary>
    public int? MinCalls { get; set; }

    /// <summary>
    /// Número máximo de llamadas
    /// </summary>
    public int? MaxCalls { get; set; }

    /// <summary>
    /// Solo registros que cumplieron meta
    /// </summary>
    public bool? GoalAchievedOnly { get; set; }

    /// <summary>
    /// Estados a incluir
    /// </summary>
    public List<CallRecordStatus>? IncludeStatuses { get; set; }

    /// <summary>
    /// Solo registros con alertas
    /// </summary>
    public bool? WithAlertsOnly { get; set; }

    /// <summary>
    /// Ordenamiento
    /// </summary>
    public string OrderBy { get; set; } = "CallDate";

    /// <summary>
    /// Dirección del ordenamiento
    /// </summary>
    public string OrderDirection { get; set; } = "DESC";

    /// <summary>
    /// Página para paginación
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Tamaño de página
    /// </summary>
    public int PageSize { get; set; } = 50;
}