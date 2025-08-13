// ============================================================================
// Archivo: GoalComplianceDto.cs
// Propósito: DTO para cumplimiento de metas
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/GoalComplianceDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para cumplimiento de metas
/// </summary>
public class GoalComplianceDto
{
    /// <summary>
    /// Sponsor relacionado
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Período de la meta (daily, weekly, monthly)
    /// </summary>
    public string Period { get; set; } = string.Empty;

    /// <summary>
    /// Meta establecida
    /// </summary>
    public int Goal { get; set; }

    /// <summary>
    /// Logro actual
    /// </summary>
    public int Achievement { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento
    /// </summary>
    public decimal CompliancePercentage { get; set; }

    /// <summary>
    /// Fecha de la medición
    /// </summary>
    public DateTime MeasurementDate { get; set; }

    /// <summary>
    /// Estado del cumplimiento
    /// </summary>
    public GoalComplianceStatus Status { get; set; }

    /// <summary>
    /// Diferencia entre meta y logro
    /// </summary>
    public int Difference { get; set; }

    /// <summary>
    /// Días restantes en el período
    /// </summary>
    public int DaysRemaining { get; set; }

    /// <summary>
    /// Proyección de cumplimiento
    /// </summary>
    public decimal ProjectedCompletion { get; set; }

    /// <summary>
    /// Indica si es probable cumplir la meta
    /// </summary>
    public bool LikelyToAchieve { get; set; }

    /// <summary>
    /// Número de ejecutivos que cumplen meta
    /// </summary>
    public int ExecutivesMeetingGoal { get; set; }

    /// <summary>
    /// Total de ejecutivos
    /// </summary>
    public int TotalExecutives { get; set; }
}

/// <summary>
/// Estado de cumplimiento de meta
/// </summary>
public enum GoalComplianceStatus
{
    /// <summary>
    /// Meta cumplida exitosamente
    /// </summary>
    Achieved,

    /// <summary>
    /// En progreso hacia la meta
    /// </summary>
    InProgress,

    /// <summary>
    /// Por debajo de lo esperado
    /// </summary>
    BelowExpected,

    /// <summary>
    /// Meta no cumplida
    /// </summary>
    NotAchieved,

    /// <summary>
    /// Meta superada
    /// </summary>
    Exceeded,

    /// <summary>
    /// En riesgo de no cumplir
    /// </summary>
    AtRisk
}