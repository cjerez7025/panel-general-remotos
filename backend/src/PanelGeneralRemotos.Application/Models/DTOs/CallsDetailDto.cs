// ============================================================================
// Archivo: CallsDetailDto.cs
// Propósito: DTO para detalle de llamadas (drill-down de sponsors)
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/CallsDetailDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para el detalle completo de llamadas de un sponsor específico
/// Usado cuando se hace drill-down en un sponsor desde la vista summary
/// </summary>
public class CallsDetailDto
{
    /// <summary>
    /// Información del sponsor seleccionado
    /// </summary>
    public SponsorInfoDto Sponsor { get; set; } = new();

    /// <summary>
    /// Rango de fechas del análisis
    /// </summary>
    public DateRange DateRange { get; set; } = new();

    /// <summary>
    /// Detalle de llamadas por ejecutivo
    /// </summary>
    public List<ExecutiveCallsDetailDto> ExecutiveDetails { get; set; } = new();

    /// <summary>
    /// Totales consolidados del sponsor
    /// </summary>
    public SponsorTotalsDto Totals { get; set; } = new();

    /// <summary>
    /// Métricas de rendimiento del sponsor
    /// </summary>
    public SponsorPerformanceMetricsDto PerformanceMetrics { get; set; } = new();

    /// <summary>
    /// Análisis por días de la semana
    /// </summary>
    public Dictionary<DayOfWeek, DayOfWeekAnalysisDto> DayOfWeekAnalysis { get; set; } = new();

    /// <summary>
    /// Tendencias del sponsor
    /// </summary>
    public SponsorTrendsDto Trends { get; set; } = new();

    /// <summary>
    /// Alertas específicas del sponsor
    /// </summary>
    public List<SystemAlertDto> SponsorAlerts { get; set; } = new();

    /// <summary>
    /// Navegación breadcrumbs
    /// </summary>
    public List<BreadcrumbDto> Breadcrumbs { get; set; } = new();

    /// <summary>
    /// Datos para exportación
    /// </summary>
    public ExportDataDto? ExportData { get; set; }

    /// <summary>
    /// Timestamp de generación
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Configuración aplicada
    /// </summary>
    public DetailConfigurationDto Configuration { get; set; } = new();
}

/// <summary>
/// Información básica del sponsor
/// </summary>
public class SponsorInfoDto
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

    /// <summary>
    /// Estado actual del sponsor
    /// </summary>
    public SponsorStatus Status { get; set; }

    /// <summary>
    /// Número de ejecutivos asignados
    /// </summary>
    public int TotalExecutives { get; set; }

    /// <summary>
    /// Número de ejecutivos activos
    /// </summary>
    public int ActiveExecutives { get; set; }

    /// <summary>
    /// Fecha de creación del sponsor
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Última modificación
    /// </summary>
    public DateTime? LastModified { get; set; }
}

/// <summary>
/// Detalle de llamadas por ejecutivo
/// </summary>
public class ExecutiveCallsDetailDto
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
    /// Llamadas por fecha
    /// </summary>
    public Dictionary<DateTime, int> CallsByDate { get; set; } = new();

    /// <summary>
    /// Metas por fecha
    /// </summary>
    public Dictionary<DateTime, int> GoalsByDate { get; set; } = new();

    /// <summary>
    /// Cumplimiento por fecha (%)
    /// </summary>
    public Dictionary<DateTime, decimal> ComplianceByDate { get; set; } = new();

    /// <summary>
    /// Total de llamadas del ejecutivo
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta total del ejecutivo
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento general
    /// </summary>
    public decimal OverallCompliance { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
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

    /// <summary>
    /// Días trabajados en el período
    /// </summary>
    public int WorkingDays { get; set; }

    /// <summary>
    /// Días con meta cumplida
    /// </summary>
    public int DaysGoalMet { get; set; }

    /// <summary>
    /// Porcentaje de días con meta cumplida
    /// </summary>
    public decimal GoalMetDaysPercentage { get; set; }

    /// <summary>
    /// Ranking del ejecutivo en el sponsor (1 = mejor)
    /// </summary>
    public int Ranking { get; set; }

    /// <summary>
    /// Porcentaje de contribución al sponsor
    /// </summary>
    public decimal ContributionPercentage { get; set; }

    /// <summary>
    /// Nivel de rendimiento del ejecutivo
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Tendencia del ejecutivo
    /// </summary>
    public string Trend { get; set; } = string.Empty;

    /// <summary>
    /// Última actividad registrada
    /// </summary>
    public DateTime? LastActivity { get; set; }

    /// <summary>
    /// Alertas específicas del ejecutivo
    /// </summary>
    public List<string> ExecutiveAlerts { get; set; } = new();

    /// <summary>
    /// Notas adicionales
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Totales consolidados del sponsor
/// </summary>
public class SponsorTotalsDto
{
    /// <summary>
    /// Total de llamadas del sponsor
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta total del sponsor
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento global
    /// </summary>
    public decimal OverallCompliance { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Promedio de llamadas por ejecutivo
    /// </summary>
    public decimal AverageCallsPerExecutive { get; set; }

    /// <summary>
    /// Mejor día del sponsor
    /// </summary>
    public DateTime? BestDay { get; set; }

    /// <summary>
    /// Llamadas en el mejor día
    /// </summary>
    public int BestDayCount { get; set; }

    /// <summary>
    /// Peor día del sponsor
    /// </summary>
    public DateTime? WorstDay { get; set; }

    /// <summary>
    /// Llamadas en el peor día
    /// </summary>
    public int WorstDayCount { get; set; }

    /// <summary>
    /// Días con datos disponibles
    /// </summary>
    public int DaysWithData { get; set; }

    /// <summary>
    /// Días donde se cumplió la meta
    /// </summary>
    public int DaysGoalMet { get; set; }

    /// <summary>
    /// Porcentaje de días con meta cumplida
    /// </summary>
    public decimal GoalMetDaysPercentage { get; set; }

    /// <summary>
    /// Variancia en las llamadas diarias
    /// </summary>
    public decimal DailyVariance { get; set; }

    /// <summary>
    /// Consistencia del sponsor (0-100)
    /// </summary>
    public decimal ConsistencyScore { get; set; }
}

/// <summary>
/// Métricas de rendimiento del sponsor
/// </summary>
public class SponsorPerformanceMetricsDto
{
    /// <summary>
    /// Eficiencia general (0-100)
    /// </summary>
    public decimal OverallEfficiency { get; set; }

    /// <summary>
    /// Productividad (llamadas por ejecutivo por día)
    /// </summary>
    public decimal Productivity { get; set; }

    /// <summary>
    /// Estabilidad (consistencia en el tiempo)
    /// </summary>
    public decimal Stability { get; set; }

    /// <summary>
    /// Crecimiento (tendencia de mejora)
    /// </summary>
    public decimal Growth { get; set; }

    /// <summary>
    /// Calidad (cumplimiento de metas)
    /// </summary>
    public decimal Quality { get; set; }

    /// <summary>
    /// Score compuesto de rendimiento
    /// </summary>
    public decimal CompositeScore { get; set; }

    /// <summary>
    /// Ranking vs otros sponsors
    /// </summary>
    public int SponsorRanking { get; set; }

    /// <summary>
    /// Percentil de rendimiento
    /// </summary>
    public decimal PerformancePercentile { get; set; }
}

/// <summary>
/// Análisis por día de la semana
/// </summary>
public class DayOfWeekAnalysisDto
{
    /// <summary>
    /// Día de la semana
    /// </summary>
    public DayOfWeek DayOfWeek { get; set; }

    /// <summary>
    /// Promedio de llamadas en este día
    /// </summary>
    public decimal AverageCalls { get; set; }

    /// <summary>
    /// Meta promedio para este día
    /// </summary>
    public decimal AverageGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento promedio
    /// </summary>
    public decimal AverageCompliance { get; set; }

    /// <summary>
    /// Mejor resultado en este día
    /// </summary>
    public int BestResult { get; set; }

    /// <summary>
    /// Peor resultado en este día
    /// </summary>
    public int WorstResult { get; set; }

    /// <summary>
    /// Número de ocurrencias de este día
    /// </summary>
    public int Occurrences { get; set; }

    /// <summary>
    /// Tendencia para este día específico
    /// </summary>
    public string DayTrend { get; set; } = string.Empty;
}

/// <summary>
/// Tendencias del sponsor
/// </summary>
public class SponsorTrendsDto
{
    /// <summary>
    /// Tendencia general (improving, declining, stable)
    /// </summary>
    public string OverallTrend { get; set; } = string.Empty;

    /// <summary>
    /// Datos para gráfico de tendencia
    /// </summary>
    public List<TrendDataPointDto> TrendData { get; set; } = new();

    /// <summary>
    /// Tendencia de cumplimiento de metas
    /// </summary>
    public string GoalComplianceTrend { get; set; } = string.Empty;

    /// <summary>
    /// Tendencia de productividad
    /// </summary>
    public string ProductivityTrend { get; set; } = string.Empty;

    /// <summary>
    /// Predicción para próximos días
    /// </summary>
    public List<PredictionDataPointDto> Predictions { get; set; } = new();

    /// <summary>
    /// Cambio porcentual vs período anterior
    /// </summary>
    public decimal ChangeFromPreviousPeriod { get; set; }

    /// <summary>
    /// Velocidad de cambio (llamadas por día de cambio)
    /// </summary>
    public decimal ChangeVelocity { get; set; }
}

/// <summary>
/// Elemento de navegación breadcrumb
/// </summary>
public class BreadcrumbDto
{
    /// <summary>
    /// Texto a mostrar
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// URL o ruta de navegación
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Indica si es el elemento actual (no clickeable)
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Icono opcional
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Orden en la navegación
    /// </summary>
    public int Order { get; set; }
}

/// <summary>
/// Datos para exportación
/// </summary>
public class ExportDataDto
{
    /// <summary>
    /// Formatos disponibles para exportar
    /// </summary>
    public List<string> AvailableFormats { get; set; } = new() { "CSV", "Excel", "PDF" };

    /// <summary>
    /// URL base para descargas
    /// </summary>
    public string? ExportBaseUrl { get; set; }

    /// <summary>
    /// Parámetros para la exportación
    /// </summary>
    public Dictionary<string, object> ExportParameters { get; set; } = new();

    /// <summary>
    /// Tamaño estimado del archivo en MB
    /// </summary>
    public decimal EstimatedSizeMB { get; set; }

    /// <summary>
    /// Tiempo estimado de generación en segundos
    /// </summary>
    public int EstimatedGenerationTimeSeconds { get; set; }
}

/// <summary>
/// Configuración para el detalle
/// </summary>
public class DetailConfigurationDto
{
    /// <summary>
    /// Incluir ejecutivos inactivos
    /// </summary>
    public bool IncludeInactiveExecutives { get; set; }

    /// <summary>
    /// Incluir análisis de tendencias
    /// </summary>
    public bool IncludeTrendAnalysis { get; set; } = true;

    /// <summary>
    /// Incluir métricas de rendimiento
    /// </summary>
    public bool IncludePerformanceMetrics { get; set; } = true;

    /// <summary>
    /// Incluir análisis por día de semana
    /// </summary>
    public bool IncludeDayOfWeekAnalysis { get; set; } = true;

    /// <summary>
    /// Incluir predicciones
    /// </summary>
    public bool IncludePredictions { get; set; } = false;

    /// <summary>
    /// Número máximo de ejecutivos a mostrar
    /// </summary>
    public int MaxExecutivesToShow { get; set; } = 50;

    /// <summary>
    /// Umbral mínimo de llamadas para incluir ejecutivo
    /// </summary>
    public int MinimumCallsThreshold { get; set; } = 1;
}

/// <summary>
/// Punto de datos para predicciones
/// </summary>
public class PredictionDataPointDto
{
    /// <summary>
    /// Fecha de la predicción
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Valor predicho
    /// </summary>
    public decimal PredictedValue { get; set; }

    /// <summary>
    /// Nivel de confianza (0-100)
    /// </summary>
    public decimal ConfidenceLevel { get; set; }

    /// <summary>
    /// Rango mínimo de la predicción
    /// </summary>
    public decimal MinRange { get; set; }

    /// <summary>
    /// Rango máximo de la predicción
    /// </summary>
    public decimal MaxRange { get; set; }

    /// <summary>
    /// Método utilizado para la predicción
    /// </summary>
    public string PredictionMethod { get; set; } = string.Empty;
}