// ============================================================================
// Archivo: SponsorPerformanceDto.cs
// Propósito: DTO para métricas de rendimiento de sponsors
// Creado: 11/08/2025 - Initial creation
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
    public int DailyGoalAverage { get; set; }

    /// <summary>
    /// Meta mensual total
    /// </summary>
    public int MonthlyGoal { get; set; }

    /// <summary>
    /// Días transcurridos en el período
    /// </summary>
    public int DaysElapsed { get; set; }

    /// <summary>
    /// Días restantes en el período
    /// </summary>
    public int DaysRemaining { get; set; }

    /// <summary>
    /// Progreso esperado a la fecha (%)
    /// </summary>
    public decimal ExpectedProgress { get; set; }

    /// <summary>
    /// Progreso real a la fecha (%)
    /// </summary>
    public decimal ActualProgress { get; set; }

    /// <summary>
    /// Diferencia entre progreso real y esperado
    /// </summary>
    public decimal ProgressDifference { get; set; }

    /// <summary>
    /// Proyección de cumplimiento de meta mensual
    /// </summary>
    public decimal MonthlyGoalProjection { get; set; }

    /// <summary>
    /// Días con meta cumplida
    /// </summary>
    public int DaysGoalMet { get; set; }

    /// <summary>
    /// Días con meta no cumplida
    /// </summary>
    public int DaysGoalNotMet { get; set; }

    /// <summary>
    /// Porcentaje de días con meta cumplida
    /// </summary>
    public decimal GoalMetDaysPercentage { get; set; }

    /// <summary>
    /// Mejor día del período
    /// </summary>
    public DateTime? BestDay { get; set; }

    /// <summary>
    /// Resultado del mejor día
    /// </summary>
    public int BestDayResult { get; set; }

    /// <summary>
    /// Peor día del período
    /// </summary>
    public DateTime? WorstDay { get; set; }

    /// <summary>
    /// Resultado del peor día
    /// </summary>
    public int WorstDayResult { get; set; }
}

/// <summary>
/// Rendimiento de un ejecutivo específico
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
    /// Nombre corto/identificador
    /// </summary>
    public string ShortName { get; set; } = string.Empty;

    /// <summary>
    /// Indica si está activo
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// KPIs individuales del ejecutivo
    /// </summary>
    public ExecutiveKpisDto KPIs { get; set; } = new();

    /// <summary>
    /// Nivel de rendimiento individual
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Ranking entre ejecutivos del sponsor (1 = mejor)
    /// </summary>
    public int Ranking { get; set; }

    /// <summary>
    /// Porcentaje de contribución al sponsor
    /// </summary>
    public decimal ContributionPercentage { get; set; }

    /// <summary>
    /// Tendencia del ejecutivo (mejorando, empeorando, estable)
    /// </summary>
    public string Trend { get; set; } = string.Empty;

    /// <summary>
    /// Días trabajados en el período
    /// </summary>
    public int WorkingDays { get; set; }

    /// <summary>
    /// Última actividad registrada
    /// </summary>
    public DateTime? LastActivity { get; set; }

    /// <summary>
    /// Alertas específicas del ejecutivo
    /// </summary>
    public List<string> Alerts { get; set; } = new();
}

/// <summary>
/// KPIs específicos de un ejecutivo
/// </summary>
public class ExecutiveKpisDto
{
    /// <summary>
    /// Total gestionado por el ejecutivo
    /// </summary>
    public int Gestionado { get; set; }

    /// <summary>
    /// Meta individual para el período
    /// </summary>
    public int Meta { get; set; }

    /// <summary>
    /// Porcentaje de avance individual
    /// </summary>
    public decimal AvancePercentage { get; set; }

    /// <summary>
    /// Total contactados
    /// </summary>
    public int Contactados { get; set; }

    /// <summary>
    /// Porcentaje de contactados
    /// </summary>
    public decimal ContactadosPercentage { get; set; }

    /// <summary>
    /// Total interesados
    /// </summary>
    public int Interesados { get; set; }

    /// <summary>
    /// Porcentaje de interesados
    /// </summary>
    public decimal InteresadosPercentage { get; set; }

    /// <summary>
    /// Total cerrados
    /// </summary>
    public int Cerrados { get; set; }

    /// <summary>
    /// Porcentaje de cerrados
    /// </summary>
    public decimal CerradosPercentage { get; set; }

    /// <summary>
    /// Llamadas promedio por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Mejor día del ejecutivo
    /// </summary>
    public DateTime? BestDay { get; set; }

    /// <summary>
    /// Llamadas en el mejor día
    /// </summary>
    public int BestDayCount { get; set; }
}

/// <summary>
/// Tendencias de rendimiento
/// </summary>
public class PerformanceTrendsDto
{
    /// <summary>
    /// Tendencia general (improving, declining, stable)
    /// </summary>
    public string OverallTrend { get; set; } = string.Empty;

    /// <summary>
    /// Datos para gráfico de tendencia (últimos 30 días)
    /// </summary>
    public List<TrendDataPointDto> TrendData { get; set; } = new();

    /// <summary>
    /// Cambio porcentual vs período anterior
    /// </summary>
    public decimal ChangeFromPreviousPeriod { get; set; }

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

/// <summary>
/// Rango de fechas
/// </summary>
public class DateRange
{
    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Número de días en el rango
    /// </summary>
    public int TotalDays => (EndDate - StartDate).Days + 1;

    /// <summary>
    /// Indica si incluye el día actual
    /// </summary>
    public bool IncludesToday => EndDate.Date >= DateTime.Today;

    /// <summary>
    /// Descripción del rango
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

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