// ============================================================================
// Archivo: AdditionalDashboardDtos.cs
// Propósito: DTOs adicionales para IDashboardService
// Creado: 11/08/2025 - Fix compilation errors
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/AdditionalDashboardDtos.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para detalle de rendimiento por sponsor (drill-down)
/// Usado en IDashboardService.GetPerformanceDetailBySponsorAsync()
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
/// Resumen de rendimiento de sponsor para vista summary
/// Usado en IDashboardService.GetPerformanceSummaryAsync()
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
    /// Total contactados exitosamente
    /// </summary>
    public int TotalContactados { get; set; }

    /// <summary>
    /// Total interesados
    /// </summary>
    public int TotalInteresados { get; set; }

    /// <summary>
    /// Total cerrados (ventas)
    /// </summary>
    public int TotalCerrados { get; set; }

    /// <summary>
    /// Nivel de rendimiento del sponsor
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Color hexadecimal para la UI
    /// </summary>
    public string? ColorHex { get; set; }
}

/// <summary>
/// Totales consolidados de rendimiento
/// Usado en múltiples lugares del dashboard
/// </summary>
public class PerformanceTotals
{
    /// <summary>
    /// Total gestionado (suma de todos los sponsors)
    /// </summary>
    public int TotalGestionado { get; set; }

    /// <summary>
    /// Meta total global
    /// </summary>
    public int TotalMeta { get; set; }

    /// <summary>
    /// Porcentaje de avance global
    /// </summary>
    public decimal AvancePercentage { get; set; }

    /// <summary>
    /// Total contactados (todos los sponsors)
    /// </summary>
    public int TotalContactados { get; set; }

    /// <summary>
    /// Porcentaje global de contactados
    /// </summary>
    public decimal ContactadosPercentage { get; set; }

    /// <summary>
    /// Total interesados (todos los sponsors)
    /// </summary>
    public int TotalInteresados { get; set; }

    /// <summary>
    /// Porcentaje global de interesados
    /// </summary>
    public decimal InteresadosPercentage { get; set; }

    /// <summary>
    /// Total cerrados (todos los sponsors)
    /// </summary>
    public int TotalCerrados { get; set; }

    /// <summary>
    /// Porcentaje global de cerrados
    /// </summary>
    public decimal CerradosPercentage { get; set; }

    /// <summary>
    /// Número de ejecutivos incluidos
    /// </summary>
    public int ExecutivesCount { get; set; }

    /// <summary>
    /// Número de sponsors incluidos
    /// </summary>
    public int SponsorsCount { get; set; }
}