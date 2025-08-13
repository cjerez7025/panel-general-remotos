// ============================================================================
// Archivo: PeriodComparisonDto.cs
// Propósito: DTO para comparación entre períodos
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/PeriodComparisonDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para comparación entre períodos
/// </summary>
public class PeriodComparisonDto
{
    /// <summary>
    /// Período actual
    /// </summary>
    public DateRange CurrentPeriod { get; set; } = new();

    /// <summary>
    /// Período anterior
    /// </summary>
    public DateRange PreviousPeriod { get; set; } = new();

    /// <summary>
    /// Comparación de llamadas
    /// </summary>
    public MetricComparisonDto CallsComparison { get; set; } = new();

    /// <summary>
    /// Comparación de rendimiento
    /// </summary>
    public MetricComparisonDto PerformanceComparison { get; set; } = new();

    /// <summary>
    /// Comparación de cumplimiento de metas
    /// </summary>
    public MetricComparisonDto GoalComplianceComparison { get; set; } = new();

    /// <summary>
    /// Comparación de productividad
    /// </summary>
    public MetricComparisonDto ProductivityComparison { get; set; } = new();

    /// <summary>
    /// Comparación de eficiencia
    /// </summary>
    public MetricComparisonDto EfficiencyComparison { get; set; } = new();

    /// <summary>
    /// Tendencia general
    /// </summary>
    public string OverallTrend { get; set; } = string.Empty;

    /// <summary>
    /// Score de mejora general (0-100)
    /// </summary>
    public decimal ImprovementScore { get; set; }

    /// <summary>
    /// Resumen de cambios principales
    /// </summary>
    public List<string> KeyChanges { get; set; } = new();

    /// <summary>
    /// Sponsors que mejoraron
    /// </summary>
    public List<string> ImprovedSponsors { get; set; } = new();

    /// <summary>
    /// Sponsors que empeoraron
    /// </summary>
    public List<string> DeclinedSponsors { get; set; } = new();

    /// <summary>
    /// Timestamp de la comparación
    /// </summary>
    public DateTime ComparisonDate { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Comparación de una métrica específica
/// </summary>
public class MetricComparisonDto
{
    /// <summary>
    /// Nombre de la métrica
    /// </summary>
    public string MetricName { get; set; } = string.Empty;

    /// <summary>
    /// Valor del período actual
    /// </summary>
    public decimal CurrentValue { get; set; }

    /// <summary>
    /// Valor del período anterior
    /// </summary>
    public decimal PreviousValue { get; set; }

    /// <summary>
    /// Diferencia absoluta
    /// </summary>
    public decimal AbsoluteDifference { get; set; }

    /// <summary>
    /// Diferencia porcentual
    /// </summary>
    public decimal PercentageDifference { get; set; }

    /// <summary>
    /// Tendencia (improving, declining, stable)
    /// </summary>
    public string Trend { get; set; } = string.Empty;

    /// <summary>
    /// Indica si es una mejora
    /// </summary>
    public bool IsImprovement { get; set; }

    /// <summary>
    /// Significancia del cambio (low, medium, high)
    /// </summary>
    public string ChangeSignificance { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del cambio
    /// </summary>
    public string? ChangeDescription { get; set; }
}