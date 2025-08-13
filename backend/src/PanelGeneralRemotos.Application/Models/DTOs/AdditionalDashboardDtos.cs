// ============================================================================
// Archivo: MissingDashboardDtos.cs
// Propósito: DTOs faltantes para IDashboardService
// Agregar estos al final de algún archivo DTO existente o crear archivo separado
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para detalle de rendimiento por sponsor (drill-down)
/// </summary>
public class PerformanceDetailBySponsor
{
    /// <summary>
    /// Información del sponsor
    /// </summary>
    public SponsorInfoDto Sponsor { get; set; } = new();

    /// <summary>
    /// Rango de fechas del análisis
    /// </summary>
    public DateRange DateRange { get; set; } = new();

    /// <summary>
    /// Detalle por ejecutivos
    /// </summary>
    public List<ExecutivePerformanceDto> ExecutiveDetails { get; set; } = new();

    /// <summary>
    /// Totales del sponsor
    /// </summary>
    public PerformanceTotals Totals { get; set; } = new();

    /// <summary>
    /// Timestamp de generación
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Resumen de rendimiento de sponsor
/// </summary>
public class SponsorPerformanceSummary
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
    /// Total gestionado
    /// </summary>
    public int TotalGestionado { get; set; }

    /// <summary>
    /// Meta total
    /// </summary>
    public int TotalMeta { get; set; }

    /// <summary>
    /// Porcentaje de avance
    /// </summary>
    public decimal AvancePercentage { get; set; }

    /// <summary>
    /// Total contactados
    /// </summary>
    public int TotalContactados { get; set; }

    /// <summary>
    /// Total interesados
    /// </summary>
    public int TotalInteresados { get; set; }

    /// <summary>
    /// Total cerrados
    /// </summary>
    public int TotalCerrados { get; set; }

    /// <summary>
    /// Nivel de rendimiento
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Color hexadecimal
    /// </summary>
    public string? ColorHex { get; set; }
}

/// <summary>
/// Totales de rendimiento
/// </summary>
public class PerformanceTotals
{
    /// <summary>
    /// Total gestionado
    /// </summary>
    public int TotalGestionado { get; set; }

    /// <summary>
    /// Meta total
    /// </summary>
    public int TotalMeta { get; set; }

    /// <summary>
    /// Porcentaje de avance global
    /// </summary>
    public decimal AvancePercentage { get; set; }

    /// <summary>
    /// Total contactados
    /// </summary>
    public int TotalContactados { get; set; }

    /// <summary>
    /// Porcentaje de contactados
    /// </summary>
    public decimal ContactadosPercentage { get; set; }

    /// <summary>
    /// Total interesados
    /// </summary>
    public int TotalInteresados { get; set; }

    /// <summary>
    /// Porcentaje de interesados
    /// </summary>
    public decimal InteresadosPercentage { get; set; }

    /// <summary>
    /// Total cerrados
    /// </summary>
    public int TotalCerrados { get; set; }

    /// <summary>
    /// Porcentaje de cerrados
    /// </summary>
    public decimal CerradosPercentage { get; set; }

    /// <summary>
    /// Ejecutivos incluidos
    /// </summary>
    public int ExecutivesCount { get; set; }

    /// <summary>
    /// Sponsors incluidos
    /// </summary>
    public int SponsorsCount { get; set; }
}