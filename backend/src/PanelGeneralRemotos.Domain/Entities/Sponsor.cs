// ============================================================================
// Archivo: Sponsor.cs
// Propósito: Entidad principal que representa un sponsor (ACHS, INTERCLINICA, etc.)
// Creado: 11/08/2025 - Initial creation con datos reales de Google Sheets
// Modificado: 11/08/2025 - Added Google Sheets configuration properties
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Domain/Entities/Sponsor.cs
// ============================================================================


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PanelGeneralRemotos.Domain.Enums;
using PanelGeneralRemotos.Domain.Entities;
namespace PanelGeneralRemotos.Domain.Entities;

/// <summary>
/// Entidad que representa un sponsor del sistema
/// Contiene la configuración de Google Sheets y ejecutivos asociados
/// </summary>
public class Sponsor
{
    /// <summary>
    /// Identificador único del sponsor
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Nombre del sponsor (ACHS, INTERCLINICA, BANMEDICA, SANATORIO)
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del sponsor
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Meta mensual total del sponsor
    /// </summary>
    public int MonthlyGoal { get; set; }

    /// <summary>
    /// Meta diaria promedio del sponsor
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Indica si el sponsor está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Color hexadecimal para la UI del sponsor
    /// </summary>
    [MaxLength(7)]
    public string? ColorHex { get; set; }

    /// <summary>
    /// Fecha de creación del registro
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Ejecutivos asociados a este sponsor
    /// </summary>
    public virtual ICollection<Executive> Executives { get; set; } = new List<Executive>();

    /// <summary>
    /// Configuraciones de Google Sheets para este sponsor
    /// </summary>
    public virtual ICollection<GoogleSheetConfiguration> GoogleSheetConfigurations { get; set; } = new List<GoogleSheetConfiguration>();

    /// <summary>
    /// Métricas de rendimiento asociadas al sponsor
    /// </summary>
    public virtual ICollection<PerformanceMetric> PerformanceMetrics { get; set; } = new List<PerformanceMetric>();

    /// <summary>
    /// Llamadas realizadas por los ejecutivos de este sponsor
    /// </summary>
    public virtual ICollection<CallRecord> CallRecords { get; set; } = new List<CallRecord>();
}