// ============================================================================
// Archivo: Executive.cs
// Propósito: Entidad que representa un ejecutivo remoto asociado a un sponsor
// Creado: 11/08/2025 - Initial creation con datos extraídos de Google Sheets
// Modificado: 11/08/2025 - Added relationship with GoogleSheetConfiguration
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Domain/Entities/Executive.cs
// ============================================================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PanelGeneralRemotos.Domain.Enums;
using PanelGeneralRemotos.Domain.Entities;
namespace PanelGeneralRemotos.Domain.Entities;

/// <summary>
/// Entidad que representa un ejecutivo remoto
/// Cada ejecutivo está asociado a un sponsor y tiene su propia hoja de Google Sheets
/// </summary>
public class Executive
{
    /// <summary>
    /// Identificador único del ejecutivo
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo del ejecutivo
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Nombre corto o identificador del ejecutivo (ej: "Ejecutivo ACHS 8")
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string ShortName { get; set; } = string.Empty;

    /// <summary>
    /// Email del ejecutivo
    /// </summary>
    [MaxLength(255)]
    public string? Email { get; set; }

    /// <summary>
    /// Teléfono de contacto
    /// </summary>
    [MaxLength(50)]
    public string? Phone { get; set; }

    /// <summary>
    /// Meta diaria individual del ejecutivo
    /// </summary>
    public int DailyCallGoal { get; set; } = 60;

    /// <summary>
    /// Meta mensual individual del ejecutivo
    /// </summary>
    public int MonthlyCallGoal { get; set; }

    /// <summary>
    /// Indica si el ejecutivo está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Fecha de ingreso del ejecutivo
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de creación del registro
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// ID del sponsor al que pertenece este ejecutivo
    /// </summary>
    [Required]
    public int SponsorId { get; set; }

    /// <summary>
    /// Sponsor al que pertenece este ejecutivo
    /// </summary>
    [ForeignKey(nameof(SponsorId))]
    public virtual Sponsor Sponsor { get; set; } = null!;

    /// <summary>
    /// Configuración de Google Sheets asociada a este ejecutivo
    /// </summary>
    public virtual GoogleSheetConfiguration? GoogleSheetConfiguration { get; set; }

    /// <summary>
    /// Registros de llamadas realizadas por este ejecutivo
    /// </summary>
    public virtual ICollection<CallRecord> CallRecords { get; set; } = new List<CallRecord>();

    /// <summary>
    /// Métricas de rendimiento del ejecutivo
    /// </summary>
    public virtual ICollection<PerformanceMetric> PerformanceMetrics { get; set; } = new List<PerformanceMetric>();

    /// <summary>
    /// Calcula el porcentaje de cumplimiento de meta diaria
    /// </summary>
    /// <param name="callsToday">Número de llamadas realizadas hoy</param>
    /// <returns>Porcentaje de cumplimiento (0-100)</returns>
    public decimal CalculateDailyGoalPercentage(int callsToday)
    {
        if (DailyCallGoal <= 0) return 0;
        return Math.Round((decimal)callsToday / DailyCallGoal * 100, 2);
    }

    /// <summary>
    /// Determina el nivel de rendimiento basado en el porcentaje de cumplimiento
    /// </summary>
    /// <param name="percentage">Porcentaje de cumplimiento</param>
    /// <returns>Nivel de rendimiento</returns>
    public PerformanceLevel GetPerformanceLevel(decimal percentage)
    {
        return percentage switch
        {
            >= 90 => PerformanceLevel.Excellent,
            >= 70 => PerformanceLevel.Good,
            >= 50 => PerformanceLevel.Average,
            _ => PerformanceLevel.Poor
        };
    }
}