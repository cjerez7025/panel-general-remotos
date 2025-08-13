// ============================================================================
// Archivo: ReportDataDto.cs
// Propósito: DTO para datos de reportes generados
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/ReportDataDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para datos de reportes generados
/// </summary>
public class ReportDataDto
{
    /// <summary>
    /// ID único del reporte
    /// </summary>
    public string ReportId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tipo de reporte (summary, detailed, executive, comparison)
    /// </summary>
    public string ReportType { get; set; } = string.Empty;

    /// <summary>
    /// Título del reporte
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Subtítulo del reporte
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// Formato del reporte (json, csv, excel, pdf)
    /// </summary>
    public string Format { get; set; } = string.Empty;

    /// <summary>
    /// Datos del reporte (puede ser JSON, XML, etc.)
    /// </summary>
    public object Data { get; set; } = new();

    /// <summary>
    /// Metadatos del reporte
    /// </summary>
    public ReportMetadataDto Metadata { get; set; } = new();

    /// <summary>
    /// Configuración utilizada para generar el reporte
    /// </summary>
    public ReportConfigurationDto Configuration { get; set; } = new();

    /// <summary>
    /// Fecha de generación
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Duración de generación del reporte
    /// </summary>
    public TimeSpan GenerationDuration { get; set; }

    /// <summary>
    /// Tamaño del reporte en bytes
    /// </summary>
    public long SizeInBytes { get; set; }

    /// <summary>
    /// URL de descarga (si aplica)
    /// </summary>
    public string? DownloadUrl { get; set; }

    /// <summary>
    /// Token de acceso para descarga
    /// </summary>
    public string? DownloadToken { get; set; }

    /// <summary>
    /// Fecha de expiración del reporte
    /// </summary>
    public DateTime? ExpiresAt { get; set; }

    /// <summary>
    /// Estado del reporte
    /// </summary>
    public ReportStatus Status { get; set; }

    /// <summary>
    /// Mensaje de estado
    /// </summary>
    public string? StatusMessage { get; set; }

    /// <summary>
    /// Errores ocurridos durante la generación
    /// </summary>
    public List<string> GenerationErrors { get; set; } = new();

    /// <summary>
    /// Warnings durante la generación
    /// </summary>
    public List<string> GenerationWarnings { get; set; } = new();
}

/// <summary>
/// Metadatos del reporte
/// </summary>
public class ReportMetadataDto
{
    /// <summary>
    /// Autor del reporte
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Organización
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Versión del reporte
    /// </summary>
    public string Version { get; set; } = "1.0";

    /// <summary>
    /// Período cubierto por el reporte
    /// </summary>
    public DateRange Period { get; set; } = new();

    /// <summary>
    /// Sponsors incluidos en el reporte
    /// </summary>
    public List<string> IncludedSponsors { get; set; } = new();

    /// <summary>
    /// Ejecutivos incluidos
    /// </summary>
    public List<string> IncludedExecutives { get; set; } = new();

    /// <summary>
    /// Total de registros incluidos
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Total de llamadas incluidas
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Filtros aplicados
    /// </summary>
    public Dictionary<string, object> AppliedFilters { get; set; } = new();

    /// <summary>
    /// Parámetros usados para generar el reporte
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new();

    /// <summary>
    /// Fuentes de datos utilizadas
    /// </summary>
    public List<string> DataSources { get; set; } = new();

    /// <summary>
    /// Última sincronización de datos incluida
    /// </summary>
    public DateTime? LastDataSync { get; set; }

    /// <summary>
    /// Calidad de datos incluidos (0-100)
    /// </summary>
    public decimal DataQuality { get; set; }

    /// <summary>
    /// Notas adicionales sobre el reporte
    /// </summary>
    public List<string> Notes { get; set; } = new();
}

/// <summary>
/// Configuración del reporte
/// </summary>
public class ReportConfigurationDto
{
    /// <summary>
    /// Incluir gráficos en el reporte
    /// </summary>
    public bool IncludeCharts { get; set; } = true;

    /// <summary>
    /// Incluir tablas detalladas
    /// </summary>
    public bool IncludeDetailedTables { get; set; } = true;

    /// <summary>
    /// Incluir resumen ejecutivo
    /// </summary>
    public bool IncludeExecutiveSummary { get; set; } = true;

    /// <summary>
    /// Incluir comparaciones con períodos anteriores
    /// </summary>
    public bool IncludePeriodComparisons { get; set; } = false;

    /// <summary>
    /// Incluir análisis de tendencias
    /// </summary>
    public bool IncludeTrendAnalysis { get; set; } = false;

    /// <summary>
    /// Incluir recomendaciones
    /// </summary>
    public bool IncludeRecommendations { get; set; } = false;

    /// <summary>
    /// Incluir datos en bruto
    /// </summary>
    public bool IncludeRawData { get; set; } = false;

    /// <summary>
    /// Agrupar datos por sponsor
    /// </summary>
    public bool GroupBySponsor { get; set; } = true;

    /// <summary>
    /// Agrupar datos por ejecutivo
    /// </summary>
    public bool GroupByExecutive { get; set; } = false;

    /// <summary>
    /// Agrupar datos por fecha
    /// </summary>
    public bool GroupByDate { get; set; } = false;

    /// <summary>
    /// Nivel de detalle (summary, detailed, comprehensive)
    /// </summary>
    public string DetailLevel { get; set; } = "summary";

    /// <summary>
    /// Orientación de página (portrait, landscape)
    /// </summary>
    public string PageOrientation { get; set; } = "portrait";

    /// <summary>
    /// Tamaño de página (A4, Letter, Legal)
    /// </summary>
    public string PageSize { get; set; } = "A4";

    /// <summary>
    /// Idioma del reporte
    /// </summary>
    public string Language { get; set; } = "es-CL";

    /// <summary>
    /// Zona horaria para fechas
    /// </summary>
    public string TimeZone { get; set; } = "America/Santiago";

    /// <summary>
    /// Formato de fecha
    /// </summary>
    public string DateFormat { get; set; } = "dd/MM/yyyy";

    /// <summary>
    /// Formato de números
    /// </summary>
    public string NumberFormat { get; set; } = "N0";

    /// <summary>
    /// Moneda para valores monetarios
    /// </summary>
    public string Currency { get; set; } = "CLP";

    /// <summary>
    /// Template personalizado (si aplica)
    /// </summary>
    public string? CustomTemplate { get; set; }

    /// <summary>
    /// Logo de la empresa
    /// </summary>
    public string? CompanyLogo { get; set; }

    /// <summary>
    /// Colores personalizados
    /// </summary>
    public Dictionary<string, string>? CustomColors { get; set; }
}

/// <summary>
/// Estado del reporte
/// </summary>
public enum ReportStatus
{
    /// <summary>
    /// Generación pendiente
    /// </summary>
    Pending,

    /// <summary>
    /// Generándose
    /// </summary>
    Generating,

    /// <summary>
    /// Completado exitosamente
    /// </summary>
    Completed,

    /// <summary>
    /// Error en la generación
    /// </summary>
    Failed,

    /// <summary>
    /// Completado con advertencias
    /// </summary>
    CompletedWithWarnings,

    /// <summary>
    /// Cancelado por el usuario
    /// </summary>
    Cancelled,

    /// <summary>
    /// Expirado
    /// </summary>
    Expired
}

/// <summary>
/// Solicitud de generación de reporte
/// </summary>
public class ReportRequestDto
{
    /// <summary>
    /// Tipo de reporte solicitado
    /// </summary>
    public string ReportType { get; set; } = string.Empty;

    /// <summary>
    /// Formato deseado
    /// </summary>
    public string Format { get; set; } = "json";

    /// <summary>
    /// Sponsor específico (opcional)
    /// </summary>
    public string? SponsorName { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Configuración del reporte
    /// </summary>
    public ReportConfigurationDto Configuration { get; set; } = new();

    /// <summary>
    /// Parámetros adicionales
    /// </summary>
    public Dictionary<string, object> AdditionalParameters { get; set; } = new();

    /// <summary>
    /// Email para notificación cuando esté listo
    /// </summary>
    public string? NotificationEmail { get; set; }
}