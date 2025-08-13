// ============================================================================
// Archivo: SystemKpisDto.cs
// Propósito: DTO para KPIs del sistema
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/SystemKpisDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para KPIs principales del sistema
/// </summary>
public class SystemKpisDto
{
    /// <summary>
    /// KPIs de llamadas
    /// </summary>
    public CallKpisDto CallKpis { get; set; } = new();

    /// <summary>
    /// KPIs de rendimiento
    /// </summary>
    public PerformanceKpisDto PerformanceKpis { get; set; } = new();

    /// <summary>
    /// KPIs de cumplimiento de metas
    /// </summary>
    public GoalKpisDto GoalKpis { get; set; } = new();

    /// <summary>
    /// KPIs de productividad
    /// </summary>
    public ProductivityKpisDto ProductivityKpis { get; set; } = new();

    /// <summary>
    /// KPIs de calidad
    /// </summary>
    public QualityKpisDto QualityKpis { get; set; } = new();

    /// <summary>
    /// Período de los KPIs
    /// </summary>
    public DateRange Period { get; set; } = new();

    /// <summary>
    /// Timestamp de generación
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Comparación con período anterior
    /// </summary>
    public KpisComparisonDto? PreviousPeriodComparison { get; set; }
}

/// <summary>
/// KPIs relacionados con llamadas
/// </summary>
public class CallKpisDto
{
    /// <summary>
    /// Total de llamadas en el período
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Promedio de llamadas por ejecutivo
    /// </summary>
    public decimal AverageCallsPerExecutive { get; set; }

    /// <summary>
    /// Pico máximo de llamadas en un día
    /// </summary>
    public int PeakCallsInDay { get; set; }

    /// <summary>
    /// Fecha del pico máximo
    /// </summary>
    public DateTime? PeakCallsDate { get; set; }

    /// <summary>
    /// Mínimo de llamadas en un día
    /// </summary>
    public int MinCallsInDay { get; set; }

    /// <summary>
    /// Fecha del mínimo
    /// </summary>
    public DateTime? MinCallsDate { get; set; }

    /// <summary>
    /// Variabilidad en llamadas diarias (desviación estándar)
    /// </summary>
    public decimal DailyVariability { get; set; }

    /// <summary>
    /// Tendencia de llamadas (creciente, decreciente, estable)
    /// </summary>
    public string CallsTrend { get; set; } = string.Empty;

    /// <summary>
    /// Tasa de crecimiento de llamadas (%)
    /// </summary>
    public decimal CallsGrowthRate { get; set; }
}

/// <summary>
/// KPIs de rendimiento general
/// </summary>
public class PerformanceKpisDto
{
    /// <summary>
    /// Score de rendimiento general del sistema (0-100)
    /// </summary>
    public decimal OverallPerformanceScore { get; set; }

    /// <summary>
    /// Eficiencia promedio del sistema (%)
    /// </summary>
    public decimal AverageEfficiency { get; set; }

    /// <summary>
    /// Productividad promedio (llamadas por hora trabajada)
    /// </summary>
    public decimal AverageProductivity { get; set; }

    /// <summary>
    /// Consistencia del sistema (0-100)
    /// </summary>
    public decimal SystemConsistency { get; set; }

    /// <summary>
    /// Número de sponsors con rendimiento excelente
    /// </summary>
    public int ExcellentPerformanceSponsors { get; set; }

    /// <summary>
    /// Número de sponsors con rendimiento bajo
    /// </summary>
    public int PoorPerformanceSponsors { get; set; }

    /// <summary>
    /// Porcentaje de ejecutivos que superan expectativas
    /// </summary>
    public decimal ExecutivesExceedingExpectations { get; set; }

    /// <summary>
    /// Porcentaje de ejecutivos bajo rendimiento
    /// </summary>
    public decimal ExecutivesUnderPerforming { get; set; }
}

/// <summary>
/// KPIs de cumplimiento de metas
/// </summary>
public class GoalKpisDto
{
    /// <summary>
    /// Meta total del sistema para el período
    /// </summary>
    public int TotalSystemGoal { get; set; }

    /// <summary>
    /// Logro total del sistema
    /// </summary>
    public int TotalSystemAchievement { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento general
    /// </summary>
    public decimal OverallCompliancePercentage { get; set; }

    /// <summary>
    /// Número de días donde se cumplió la meta global
    /// </summary>
    public int DaysGlobalGoalMet { get; set; }

    /// <summary>
    /// Porcentaje de días con meta cumplida
    /// </summary>
    public decimal DaysGoalMetPercentage { get; set; }

    /// <summary>
    /// Número de sponsors que cumplen meta mensual
    /// </summary>
    public int SponsorsMeetingMonthlyGoal { get; set; }

    /// <summary>
    /// Porcentaje de ejecutivos que cumplen meta diaria promedio
    /// </summary>
    public decimal ExecutivesDailyGoalCompliance { get; set; }

    /// <summary>
    /// Brecha promedio entre meta y logro (%)
    /// </summary>
    public decimal AverageGoalGap { get; set; }

    /// <summary>
    /// Proyección de cumplimiento de meta mensual (%)
    /// </summary>
    public decimal MonthlyGoalProjection { get; set; }
}

/// <summary>
/// KPIs de productividad
/// </summary>
public class ProductivityKpisDto
{
    /// <summary>
    /// Productividad promedio por ejecutivo (llamadas/día)
    /// </summary>
    public decimal AverageExecutiveProductivity { get; set; }

    /// <summary>
    /// Productividad promedio por sponsor (llamadas/día/ejecutivo)
    /// </summary>
    public decimal AverageSponsorProductivity { get; set; }

    /// <summary>
    /// Ejecutivo más productivo del período
    /// </summary>
    public string? TopExecutiveName { get; set; }

    /// <summary>
    /// Productividad del ejecutivo más productivo
    /// </summary>
    public decimal TopExecutiveProductivity { get; set; }

    /// <summary>
    /// Sponsor más productivo del período
    /// </summary>
    public string? TopSponsorName { get; set; }

    /// <summary>
    /// Productividad del sponsor más productivo
    /// </summary>
    public decimal TopSponsorProductivity { get; set; }

    /// <summary>
    /// Horas trabajadas promedio por día
    /// </summary>
    public decimal AverageWorkingHoursPerDay { get; set; }

    /// <summary>
    /// Llamadas por hora promedio
    /// </summary>
    public decimal AverageCallsPerHour { get; set; }

    /// <summary>
    /// Utilización de tiempo promedio (%)
    /// </summary>
    public decimal AverageTimeUtilization { get; set; }
}

/// <summary>
/// KPIs de calidad
/// </summary>
public class QualityKpisDto
{
    /// <summary>
    /// Score de calidad promedio del sistema (0-100)
    /// </summary>
    public decimal AverageQualityScore { get; set; }

    /// <summary>
    /// Tasa de contacto exitoso promedio (%)
    /// </summary>
    public decimal AverageContactSuccessRate { get; set; }

    /// <summary>
    /// Tasa de conversión promedio (%)
    /// </summary>
    public decimal AverageConversionRate { get; set; }

    /// <summary>
    /// Tasa de interés promedio (%)
    /// </summary>
    public decimal AverageInterestRate { get; set; }

    /// <summary>
    /// Tasa de cierre promedio (%)
    /// </summary>
    public decimal AverageCloseRate { get; set; }

    /// <summary>
    /// Duración promedio por llamada (minutos)
    /// </summary>
    public decimal AverageCallDuration { get; set; }

    /// <summary>
    /// Tasa de abandono promedio (%)
    /// </summary>
    public decimal AverageAbandonRate { get; set; }

    /// <summary>
    /// Satisfacción del cliente promedio (si disponible)
    /// </summary>
    public decimal? AverageCustomerSatisfaction { get; set; }

    /// <summary>
    /// Número de quejas o problemas reportados
    /// </summary>
    public int QualityIssuesReported { get; set; }

    /// <summary>
    /// Porcentaje de llamadas de alta calidad
    /// </summary>
    public decimal HighQualityCallsPercentage { get; set; }
}

/// <summary>
/// Comparación de KPIs con período anterior
/// </summary>
public class KpisComparisonDto
{
    /// <summary>
    /// Período anterior utilizado para comparación
    /// </summary>
    public DateRange PreviousPeriod { get; set; } = new();

    /// <summary>
    /// Cambio en total de llamadas (%)
    /// </summary>
    public decimal CallsChange { get; set; }

    /// <summary>
    /// Cambio en rendimiento general (%)
    /// </summary>
    public decimal PerformanceChange { get; set; }

    /// <summary>
    /// Cambio en cumplimiento de metas (%)
    /// </summary>
    public decimal GoalComplianceChange { get; set; }

    /// <summary>
    /// Cambio en productividad (%)
    /// </summary>
    public decimal ProductivityChange { get; set; }

    /// <summary>
    /// Cambio en calidad (%)
    /// </summary>
    public decimal QualityChange { get; set; }

    /// <summary>
    /// Cambio en eficiencia (%)
    /// </summary>
    public decimal EfficiencyChange { get; set; }

    /// <summary>
    /// Tendencia general (improving, declining, stable)
    /// </summary>
    public string OverallTrend { get; set; } = string.Empty;

    /// <summary>
    /// KPIs que mejoraron
    /// </summary>
    public List<string> ImprovedKpis { get; set; } = new();

    /// <summary>
    /// KPIs que empeoraron
    /// </summary>
    public List<string> DeclinedKpis { get; set; } = new();

    /// <summary>
    /// KPIs que se mantuvieron estables
    /// </summary>
    public List<string> StableKpis { get; set; } = new();
}