// ============================================================================
// Archivo: SponsorPerformanceDto.cs
// Propósito: DTO para métricas de rendimiento de sponsors
// Creado: 11/08/2025 - Initial creation
// Modificado: 21/08/2025 - CORREGIDO: Eliminada definición duplicada de DateRange
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/SponsorPerformanceDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para métricas de rendimiento de sponsors - Pestaña "Rendimiento y Métricas"
/// </summary>
public class SponsorPerformanceDto
{
    /// <summary>
    /// ID del sponsor
    /// </summary>
    public int SponsorId { get; set; }

    /// <summary>
    /// Nombre del sponsor (INTERCLINICA, ACHS, BANMEDICA)
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Color hexadecimal del sponsor para la UI
    /// </summary>
    public string? ColorHex { get; set; }

    /// <summary>
    /// Descripción del sponsor
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Rango de fechas del análisis
    /// CORRECCIÓN: Esta referencia ahora apunta a DateRange en MissingCallRecordDTOs.cs
    /// </summary>
    public DateRange DateRange { get; set; } = new();

    /// <summary>
    /// KPIs principales del sponsor
    /// </summary>
    public SponsorKpisDto KPIs { get; set; } = new();

    /// <summary>
    /// Métricas de cumplimiento de metas
    /// </summary>
    public GoalMetricsDto GoalMetrics { get; set; } = new();

    /// <summary>
    /// Métricas de rendimiento por ejecutivo
    /// </summary>
    public List<ExecutivePerformanceDto> ExecutivePerformance { get; set; } = new();

    /// <summary>
    /// Tendencias históricas
    /// </summary>
    public PerformanceTrendsDto Trends { get; set; } = new();

    /// <summary>
    /// Comparación con otros sponsors
    /// </summary>
    public SponsorComparisonDto? Comparison { get; set; }

    /// <summary>
    /// Estado general del sponsor
    /// </summary>
    public SponsorStatus Status { get; set; }

    /// <summary>
    /// Nivel de rendimiento general
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Última actualización de datos
    /// </summary>
    public DateTime LastDataUpdate { get; set; }

    /// <summary>
    /// Indica si hay problemas con los datos
    /// </summary>
    public bool HasDataIssues { get; set; }

    /// <summary>
    /// Lista de problemas identificados
    /// </summary>
    public List<string> DataIssues { get; set; } = new();

    /// <summary>
    /// Alertas específicas del sponsor
    /// </summary>
    public List<SystemAlertDto> Alerts { get; set; } = new();
}

/// <summary>
/// KPIs principales de un sponsor
/// </summary>
public class SponsorKpisDto
{
    /// <summary>
    /// Total gestionado en el período
    /// </summary>
    public int TotalGestionado { get; set; }

    /// <summary>
    /// Meta total para el período
    /// </summary>
    public int TotalMeta { get; set; }

    /// <summary>
    /// Porcentaje de avance hacia la meta
    /// </summary>
    public decimal AvancePercentage { get; set; }

    /// <summary>
    /// Total de contactados exitosamente
    /// </summary>
    public int TotalContactados { get; set; }

    /// <summary>
    /// Porcentaje de contactados del total gestionado
    /// </summary>
    public decimal ContactadosPercentage { get; set; }

    /// <summary>
    /// Total de interesados
    /// </summary>
    public int TotalInteresados { get; set; }

    /// <summary>
    /// Porcentaje de interesados del total contactados
    /// </summary>
    public decimal InteresadosPercentage { get; set; }

    /// <summary>
    /// Total de cerrados (ventas exitosas)
    /// </summary>
    public int TotalCerrados { get; set; }

    /// <summary>
    /// Porcentaje de cerrados del total interesados
    /// </summary>
    public decimal CerradosPercentage { get; set; }

    /// <summary>
    /// Tasa de conversión general (cerrados / gestionado)
    /// </summary>
    public decimal ConversionRate { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Promedio de llamadas por ejecutivo
    /// </summary>
    public decimal AverageCallsPerExecutive { get; set; }

    /// <summary>
    /// Eficiencia general (0-100)
    /// </summary>
    public decimal OverallEfficiency { get; set; }
}

/// <summary>
/// Métricas de cumplimiento de metas
/// </summary>
public class GoalMetricsDto
{
    /// <summary>
    /// Meta diaria promedio
    /// </summary>
    public decimal DailyGoalAverage { get; set; }

    /// <summary>
    /// Meta total del período
    /// </summary>
    public int TotalPeriodGoal { get; set; }

    /// <summary>
    /// Progreso actual hacia la meta
    /// </summary>
    public decimal GoalProgress { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento de meta
    /// </summary>
    public decimal GoalAchievementPercentage { get; set; }

    /// <summary>
    /// Días restantes para alcanzar la meta
    /// </summary>
    public int DaysToReachGoal { get; set; }

    /// <summary>
    /// Llamadas diarias necesarias para alcanzar la meta
    /// </summary>
    public decimal RequiredDailyCallsToMeetGoal { get; set; }

    /// <summary>
    /// Indica si la meta es alcanzable
    /// </summary>
    public bool IsGoalAchievable { get; set; }

    /// <summary>
    /// Proyección de cumplimiento basada en tendencia actual
    /// </summary>
    public decimal ProjectedGoalCompletion { get; set; }

    /// <summary>
    /// Diferencia entre progreso actual y progreso esperado
    /// </summary>
    public decimal GoalVariance { get; set; }

    /// <summary>
    /// Estado del cumplimiento de la meta
    /// </summary>
    public GoalStatus GoalStatus { get; set; }
}

/// <summary>
/// Métricas de rendimiento por ejecutivo
/// </summary>
public class ExecutivePerformanceDto
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
    /// Nombre corto o identificador
    /// </summary>
    public string ShortName { get; set; } = string.Empty;

    /// <summary>
    /// Indica si el ejecutivo está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

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
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento de meta
    /// </summary>
    public decimal GoalAchievementPercentage { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Nivel de rendimiento del ejecutivo
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Total contactados por el ejecutivo
    /// </summary>
    public int TotalContactados { get; set; }

    /// <summary>
    /// Total interesados generados
    /// </summary>
    public int TotalInteresados { get; set; }

    /// <summary>
    /// Total cerrados (ventas)
    /// </summary>
    public int TotalCerrados { get; set; }

    /// <summary>
    /// Tasa de contactabilidad
    /// </summary>
    public decimal ContactabilityRate { get; set; }

    /// <summary>
    /// Tasa de conversión a interesado
    /// </summary>
    public decimal InterestConversionRate { get; set; }

    /// <summary>
    /// Tasa de cierre
    /// </summary>
    public decimal ClosingRate { get; set; }

    /// <summary>
    /// Eficiencia general del ejecutivo
    /// </summary>
    public decimal OverallEfficiency { get; set; }

    /// <summary>
    /// Ranking del ejecutivo en el sponsor
    /// </summary>
    public int Ranking { get; set; }

    /// <summary>
    /// Mejor día (más llamadas)
    /// </summary>
    public DateTime? BestPerformanceDay { get; set; }

    /// <summary>
    /// Peor día (menos llamadas)
    /// </summary>
    public DateTime? WorstPerformanceDay { get; set; }

    /// <summary>
    /// Tendencia de rendimiento (mejorando, estable, empeorando)
    /// </summary>
    public PerformanceTrend Trend { get; set; }

    /// <summary>
    /// Llamadas por fecha del ejecutivo
    /// </summary>
    public Dictionary<DateTime, int> CallsByDate { get; set; } = new();

    /// <summary>
    /// Alertas específicas del ejecutivo
    /// </summary>
    public List<string> Alerts { get; set; } = new();
}

/// <summary>
/// Tendencias históricas de rendimiento
/// </summary>
public class PerformanceTrendsDto
{
    /// <summary>
    /// Datos de tendencia diaria
    /// </summary>
    public List<TrendDataPointDto> DailyTrends { get; set; } = new();

    /// <summary>
    /// Datos de tendencia semanal
    /// </summary>
    public List<TrendDataPointDto> WeeklyTrends { get; set; } = new();

    /// <summary>
    /// Datos de tendencia mensual
    /// </summary>
    public List<TrendDataPointDto> MonthlyTrends { get; set; } = new();

    /// <summary>
    /// Tendencia general (mejorando, estable, empeorando)
    /// </summary>
    public PerformanceTrend OverallTrend { get; set; }

    /// <summary>
    /// Porcentaje de cambio en el período
    /// </summary>
    public decimal PercentageChange { get; set; }

    /// <summary>
    /// Proyección para el próximo período
    /// </summary>
    public ProjectionDto? NextPeriodProjection { get; set; }
}

/// <summary>
/// Punto de datos para gráfico de tendencias
/// </summary>
public class TrendDataPointDto
{
    /// <summary>
    /// Fecha del punto de datos
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Valor de la métrica
    /// </summary>
    public decimal Value { get; set; }

    /// <summary>
    /// Tipo de métrica
    /// </summary>
    public string MetricType { get; set; } = string.Empty;

    /// <summary>
    /// Meta para esa fecha
    /// </summary>
    public decimal? Goal { get; set; }
}

/// <summary>
/// Proyección de rendimiento
/// </summary>
public class ProjectionDto
{
    /// <summary>
    /// Período de la proyección
    /// </summary>
    public DateRange ProjectionPeriod { get; set; } = new();

    /// <summary>
    /// Valor proyectado
    /// </summary>
    public decimal ProjectedValue { get; set; }

    /// <summary>
    /// Nivel de confianza de la proyección (0-100)
    /// </summary>
    public decimal ConfidenceLevel { get; set; }

    /// <summary>
    /// Metodología usada para la proyección
    /// </summary>
    public string ProjectionMethod { get; set; } = string.Empty;

    /// <summary>
    /// Factores considerados en la proyección
    /// </summary>
    public List<string> ProjectionFactors { get; set; } = new();
}

/// <summary>
/// Comparación entre sponsors
/// </summary>
public class SponsorComparisonDto
{
    /// <summary>
    /// Ranking del sponsor (1 = mejor)
    /// </summary>
    public int Ranking { get; set; }

    /// <summary>
    /// Total de sponsors en la comparación
    /// </summary>
    public int TotalSponsors { get; set; }

    /// <summary>
    /// Percentil de rendimiento (0-100)
    /// </summary>
    public decimal PerformancePercentile { get; set; }

    /// <summary>
    /// Diferencia con el mejor sponsor
    /// </summary>
    public decimal DifferenceFromBest { get; set; }

    /// <summary>
    /// Diferencia con el promedio
    /// </summary>
    public decimal DifferenceFromAverage { get; set; }

    /// <summary>
    /// Métricas comparativas por categoría
    /// </summary>
    public Dictionary<string, SponsorComparisonMetricDto> ComparisonMetrics { get; set; } = new();
}

/// <summary>
/// Métrica comparativa específica
/// </summary>
public class SponsorComparisonMetricDto
{
    /// <summary>
    /// Nombre de la métrica
    /// </summary>
    public string MetricName { get; set; } = string.Empty;

    /// <summary>
    /// Valor del sponsor actual
    /// </summary>
    public decimal CurrentValue { get; set; }

    /// <summary>
    /// Valor promedio de todos los sponsors
    /// </summary>
    public decimal AverageValue { get; set; }

    /// <summary>
    /// Mejor valor entre todos los sponsors
    /// </summary>
    public decimal BestValue { get; set; }

    /// <summary>
    /// Ranking en esta métrica específica
    /// </summary>
    public int MetricRanking { get; set; }

    /// <summary>
    /// Percentil en esta métrica (0-100)
    /// </summary>
    public decimal MetricPercentile { get; set; }
}

// ============================================================================
// ENUMS AUXILIARES
// ============================================================================

/// <summary>
/// Estado de un sponsor
/// </summary>
public enum SponsorStatus
{
    /// <summary>
    /// Activo y funcionando normalmente
    /// </summary>
    Active,

    /// <summary>
    /// Activo pero con advertencias
    /// </summary>
    ActiveWithWarnings,

    /// <summary>
    /// Inactivo temporalmente
    /// </summary>
    Inactive,

    /// <summary>
    /// Suspendido
    /// </summary>
    Suspended,

    /// <summary>
    /// En configuración
    /// </summary>
    Setup
}

/// <summary>
/// Nivel de rendimiento
/// </summary>
public enum PerformanceLevel
{
    /// <summary>
    /// Excelente (90-100% de meta)
    /// </summary>
    Excellent,

    /// <summary>
    /// Bueno (75-89% de meta)
    /// </summary>
    Good,

    /// <summary>
    /// Regular (60-74% de meta)
    /// </summary>
    Average,

    /// <summary>
    /// Malo (40-59% de meta)
    /// </summary>
    Poor,

    /// <summary>
    /// Crítico (<40% de meta)
    /// </summary>
    Critical,

    /// <summary>
    /// Sin datos suficientes
    /// </summary>
    NoData
}

/// <summary>
/// Estado del cumplimiento de meta
/// </summary>
public enum GoalStatus
{
    /// <summary>
    /// Por encima de la meta
    /// </summary>
    AboveGoal,

    /// <summary>
    /// En línea con la meta
    /// </summary>
    OnTrack,

    /// <summary>
    /// Ligeramente por debajo
    /// </summary>
    SlightlyBehind,

    /// <summary>
    /// Significativamente por debajo
    /// </summary>
    SignificantlyBehind,

    /// <summary>
    /// Muy por debajo de la meta
    /// </summary>
    FarBehind,

    /// <summary>
    /// Sin actividad
    /// </summary>
    NoActivity
}

/// <summary>
/// Tendencia de rendimiento
/// </summary>
public enum PerformanceTrend
{
    /// <summary>
    /// Mejorando significativamente
    /// </summary>
    ImprovingSignificantly,

    /// <summary>
    /// Mejorando
    /// </summary>
    Improving,

    /// <summary>
    /// Estable
    /// </summary>
    Stable,

    /// <summary>
    /// Empeorando
    /// </summary>
    Declining,

    /// <summary>
    /// Empeorando significativamente
    /// </summary>
    DecliningSignificantly,

    /// <summary>
    /// Datos insuficientes
    /// </summary>
    InsufficientData
}

// ============================================================================
// CORRECCIÓN APLICADA:
// ❌ ELIMINADA la definición duplicada de DateRange (que estaba en línea ~539)
// ✅ Ahora DateRange se referencia desde MissingCallRecordDTOs.cs
// ✅ Archivo listo para compilación sin errores CS0101
// ============================================================================