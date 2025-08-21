// ============================================================================
// Archivo: QuickStatsDto.cs
// Propósito: DTO para las estadísticas rápidas del dashboard (4 cards superiores)
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/QuickStatsDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para las estadísticas rápidas mostradas en los cards superiores del dashboard
/// Corresponde a la sección "Quick Stats" del mockup
/// </summary>
public class QuickStatsDto
{
    /// <summary>
    /// Total de llamadas realizadas en el día actual
    /// Card: "Total Llamadas del Día"
    /// </summary>
    public int TotalCallsToday { get; set; }

    /// <summary>
    /// Meta diaria total (suma de todos los sponsors)
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje general de cumplimiento
    /// </summary>
    public decimal OverallPercentage { get; set; }

    /// <summary>
    /// Porcentaje de cambio respecto al día anterior
    /// Positivo = incremento, Negativo = disminución
    /// </summary>
    public decimal CallsChangePercentage { get; set; }

    /// <summary>
    /// Número de sponsors activos (con llamadas hoy)
    /// Card: "Sponsors Activos"
    /// </summary>
    public int ActiveSponsors { get; set; }

    /// <summary>
    /// Número de sponsors con problemas (sin llamadas o bajo rendimiento)
    /// Card: "Sponsors Problemáticos"
    /// </summary>
    public int ProblematicSponsors { get; set; }

    /// <summary>
    /// Porcentaje de contactados exitosamente del total de llamadas
    /// Card: "% Contactados"
    /// </summary>
    public decimal ContactedPercentage { get; set; }

    /// <summary>
    /// Porcentaje de avance hacia la meta diaria total
    /// Card: "% Avance hacia Meta"
    /// </summary>
    public decimal GoalProgressPercentage { get; set; }

    /// <summary>
    /// Número total de ejecutivos activos
    /// </summary>
    public int TotalActiveExecutives { get; set; }

    /// <summary>
    /// Promedio de llamadas por ejecutivo hoy
    /// </summary>
    public decimal AverageCallsPerExecutive { get; set; }

    /// <summary>
    /// Timestamp de la última actualización de datos
    /// Para mostrar en el header "Última sincronización"
    /// </summary>
    public DateTime LastRefresh { get; set; }

    /// <summary>
    /// Timestamp de generación de este reporte
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indica si hay algún problema de sincronización
    /// </summary>
    public bool HasSyncIssues { get; set; }

    /// <summary>
    /// Tiempo transcurrido desde la última sincronización en minutos
    /// </summary>
    public int MinutesSinceLastSync { get; set; }

    /// <summary>
    /// Estado general del sistema
    /// </summary>
    public SystemHealthStatus SystemStatus { get; set; }

    /// <summary>
    /// Estado de la fuente de datos
    /// </summary>
    public string DataSourceStatus { get; set; } = "Connected";

    /// <summary>
    /// Información adicional sobre el estado
    /// </summary>
    public string? StatusMessage { get; set; }

    /// <summary>
    /// Breakdown por sponsor para análisis rápido
    /// </summary>
    public List<SponsorQuickStatsDto> SponsorStats { get; set; } = new();

    /// <summary>
    /// Indicador de tendencia con valor por defecto
    /// </summary>
    public string TrendIndicator { get; set; } = "stable";
}

/// <summary>
/// Quick stats específicas por sponsor
/// </summary>
public class SponsorQuickStatsDto
{
    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Número de llamadas hoy
    /// </summary>
    public int CallsToday { get; set; }

    /// <summary>
    /// Meta diaria del sponsor
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento de meta
    /// </summary>
    public decimal GoalPercentage { get; set; }

    /// <summary>
    /// Estado del sponsor (usando el enum de MissingDTOs.cs)
    /// </summary>
    public string Status { get; set; } = "Unknown";

    /// <summary>
    /// Color hexadecimal para mostrar en la UI
    /// </summary>
    public string? ColorHex { get; set; }

    /// <summary>
    /// Número de ejecutivos activos del sponsor
    /// </summary>
    public int ActiveExecutives { get; set; }

    /// <summary>
    /// Icono de tendencia para mostrar en la UI
    /// </summary>
    public string? TrendIcon { get; set; }

    /// <summary>
    /// Última actualización de datos del sponsor
    /// </summary>
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Estado de salud del sistema general
/// </summary>
public enum SystemHealthStatus
{
    /// <summary>
    /// Todo funcionando correctamente
    /// </summary>
    Healthy,

    /// <summary>
    /// Algunos problemas menores
    /// </summary>
    Warning,

    /// <summary>
    /// Problemas significativos
    /// </summary>
    Critical,

    /// <summary>
    /// Sistema no disponible
    /// </summary>
    Down
}