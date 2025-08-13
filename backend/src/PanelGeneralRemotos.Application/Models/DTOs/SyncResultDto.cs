// ============================================================================
// Archivo: SyncResultDto.cs
// Propósito: DTO para resultado de sincronización manual
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/SyncResultDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para resultado de sincronización manual con Google Sheets
/// </summary>
public class SyncResultDto
{
    /// <summary>
    /// Indica si la sincronización fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensaje principal del resultado
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del resultado
    /// </summary>
    public string? DetailedDescription { get; set; }

    /// <summary>
    /// Timestamp de inicio de la sincronización
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Timestamp de finalización de la sincronización
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Duración total de la sincronización
    /// </summary>
    public TimeSpan Duration => EndTime - StartTime;

    /// <summary>
    /// Duración en segundos (para UI)
    /// </summary>
    public double DurationSeconds => Duration.TotalSeconds;

    /// <summary>
    /// Tipo de sincronización realizada
    /// </summary>
    public SyncType SyncType { get; set; }

    /// <summary>
    /// Sponsors sincronizados
    /// </summary>
    public List<SponsorSyncResultDto> SponsorResults { get; set; } = new();

    /// <summary>
    /// Estadísticas generales de la sincronización
    /// </summary>
    public SyncStatisticsDto Statistics { get; set; } = new();

    /// <summary>
    /// Errores ocurridos durante la sincronización
    /// </summary>
    public List<SyncErrorDto> Errors { get; set; } = new();

    /// <summary>
    /// Warnings generados durante la sincronización
    /// </summary>
    public List<SyncWarningDto> Warnings { get; set; } = new();

    /// <summary>
    /// Cambios detectados durante la sincronización
    /// </summary>
    public List<DataChangeDetectedDto> ChangesDetected { get; set; } = new();

    /// <summary>
    /// Métricas de rendimiento de la sincronización
    /// </summary>
    public SyncPerformanceMetricsDto PerformanceMetrics { get; set; } = new();

    /// <summary>
    /// Estado de conectividad con Google Sheets
    /// </summary>
    public GoogleSheetsConnectivityDto GoogleSheetsConnectivity { get; set; } = new();

    /// <summary>
    /// Datos del sistema después de la sincronización
    /// </summary>
    public PostSyncSystemStateDto? PostSyncSystemState { get; set; }

    /// <summary>
    /// Próxima sincronización automática programada
    /// </summary>
    public DateTime? NextAutomaticSync { get; set; }

    /// <summary>
    /// Recomendaciones basadas en el resultado
    /// </summary>
    public List<string> Recommendations { get; set; } = new();

    /// <summary>
    /// Indica si se requiere atención manual
    /// </summary>
    public bool RequiresManualAttention { get; set; }

    /// <summary>
    /// Hash del estado de datos después de la sincronización
    /// </summary>
    public string? PostSyncDataHash { get; set; }
}

/// <summary>
/// Tipo de sincronización
/// </summary>
public enum SyncType
{
    /// <summary>
    /// Sincronización manual completa
    /// </summary>
    ManualFull,

    /// <summary>
    /// Sincronización manual incremental
    /// </summary>
    ManualIncremental,

    /// <summary>
    /// Sincronización automática programada
    /// </summary>
    AutomaticScheduled,

    /// <summary>
    /// Sincronización de un sponsor específico
    /// </summary>
    SingleSponsor,

    /// <summary>
    /// Sincronización forzada (ignora cache)
    /// </summary>
    Forced,

    /// <summary>
    /// Sincronización de recuperación
    /// </summary>
    Recovery
}

/// <summary>
/// Estadísticas generales de sincronización
/// </summary>
public class SyncStatisticsDto
{
    /// <summary>
    /// Total de sponsors procesados
    /// </summary>
    public int TotalSponsorsProcessed { get; set; }

    /// <summary>
    /// Sponsors sincronizados exitosamente
    /// </summary>
    public int SuccessfulSponsors { get; set; }

    /// <summary>
    /// Sponsors con errores
    /// </summary>
    public int FailedSponsors { get; set; }

    /// <summary>
    /// Total de hojas de Google Sheets accedidas
    /// </summary>
    public int GoogleSheetsAccessed { get; set; }

    /// <summary>
    /// Total de registros procesados
    /// </summary>
    public int TotalRecordsProcessed { get; set; }

    /// <summary>
    /// Registros creados
    /// </summary>
    public int RecordsCreated { get; set; }

    /// <summary>
    /// Registros actualizados
    /// </summary>
    public int RecordsUpdated { get; set; }

    /// <summary>
    /// Registros eliminados
    /// </summary>
    public int RecordsDeleted { get; set; }

    /// <summary>
    /// Registros sin cambios
    /// </summary>
    public int RecordsUnchanged { get; set; }

    /// <summary>
    /// Ejecutivos procesados
    /// </summary>
    public int ExecutivesProcessed { get; set; }

    /// <summary>
    /// Ejecutivos activos encontrados
    /// </summary>
    public int ActiveExecutivesFound { get; set; }

    /// <summary>
    /// Total de errores encontrados
    /// </summary>
    public int TotalErrors { get; set; }

    /// <summary>
    /// Total de warnings generados
    /// </summary>
    public int TotalWarnings { get; set; }

    /// <summary>
    /// Velocidad de procesamiento (registros por segundo)
    /// </summary>
    public decimal ProcessingSpeed { get; set; }

    /// <summary>
    /// Porcentaje de éxito general
    /// </summary>
    public decimal SuccessRate { get; set; }

    /// <summary>
    /// Datos transferidos en bytes
    /// </summary>
    public long DataTransferredBytes { get; set; }

    /// <summary>
    /// Número de API calls realizadas
    /// </summary>
    public int ApiCallsMade { get; set; }
}

/// <summary>
/// Error de sincronización
/// </summary>
public class SyncErrorDto
{
    /// <summary>
    /// ID único del error
    /// </summary>
    public string ErrorId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Código del error
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de error
    /// </summary>
    public SyncErrorType ErrorType { get; set; }

    /// <summary>
    /// Mensaje del error
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Detalles técnicos del error
    /// </summary>
    public string? TechnicalDetails { get; set; }

    /// <summary>
    /// Sponsor relacionado (si aplica)
    /// </summary>
    public string? RelatedSponsor { get; set; }

    /// <summary>
    /// Ejecutivo relacionado (si aplica)
    /// </summary>
    public string? RelatedExecutive { get; set; }

    /// <summary>
    /// Hoja de Google Sheets relacionada
    /// </summary>
    public string? RelatedSheet { get; set; }

    /// <summary>
    /// Fila específica con error (si aplica)
    /// </summary>
    public int? RelatedRow { get; set; }

    /// <summary>
    /// Timestamp del error
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Stack trace del error (para debugging)
    /// </summary>
    public string? StackTrace { get; set; }

    /// <summary>
    /// Indica si el error es crítico
    /// </summary>
    public bool IsCritical { get; set; }

    /// <summary>
    /// Número de reintentos realizados
    /// </summary>
    public int RetryCount { get; set; }

    /// <summary>
    /// Indica si el error es recuperable
    /// </summary>
    public bool IsRecoverable { get; set; }

    /// <summary>
    /// Sugerencia para resolver el error
    /// </summary>
    public string? ResolutionSuggestion { get; set; }

    /// <summary>
    /// Contexto adicional del error
    /// </summary>
    public Dictionary<string, object> ErrorContext { get; set; } = new();
}

/// <summary>
/// Tipos de error de sincronización
/// </summary>
public enum SyncErrorType
{
    /// <summary>
    /// Error de conexión con Google Sheets
    /// </summary>
    GoogleSheetsConnection,

    /// <summary>
    /// Error de autenticación
    /// </summary>
    Authentication,

    /// <summary>
    /// Error de autorización/permisos
    /// </summary>
    Authorization,

    /// <summary>
    /// Estructura de datos incorrecta
    /// </summary>
    DataStructure,

    /// <summary>
    /// Error de validación de datos
    /// </summary>
    DataValidation,

    /// <summary>
    /// Error de base de datos
    /// </summary>
    Database,

    /// <summary>
    /// Timeout de operación
    /// </summary>
    Timeout,

    /// <summary>
    /// Límite de API alcanzado
    /// </summary>
    RateLimit,

    /// <summary>
    /// Error de procesamiento
    /// </summary>
    Processing,

    /// <summary>
    /// Error de configuración
    /// </summary>
    Configuration,

    /// <summary>
    /// Error de red
    /// </summary>
    Network,

    /// <summary>
    /// Error desconocido
    /// </summary>
    Unknown
}

/// <summary>
/// Warning de sincronización
/// </summary>
public class SyncWarningDto
{
    /// <summary>
    /// ID único del warning
    /// </summary>
    public string WarningId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tipo de warning
    /// </summary>
    public SyncWarningType WarningType { get; set; }

    /// <summary>
    /// Mensaje del warning
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Recomendación para resolver el warning
    /// </summary>
    public string? Recommendation { get; set; }

    /// <summary>
    /// Sponsor relacionado (si aplica)
    /// </summary>
    public string? RelatedSponsor { get; set; }

    /// <summary>
    /// Ejecutivo relacionado (si aplica)
    /// </summary>
    public string? RelatedExecutive { get; set; }

    /// <summary>
    /// Timestamp del warning
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Severidad del warning
    /// </summary>
    public WarningSeverity Severity { get; set; }

    /// <summary>
    /// Indica si el warning requiere acción
    /// </summary>
    public bool RequiresAction { get; set; }

    /// <summary>
    /// Contexto adicional del warning
    /// </summary>
    public Dictionary<string, object> WarningContext { get; set; } = new();
}

/// <summary>
/// Tipos de warning de sincronización
/// </summary>
public enum SyncWarningType
{
    /// <summary>
    /// Respuesta lenta de API
    /// </summary>
    SlowResponse,

    /// <summary>
    /// Datos parciales obtenidos
    /// </summary>
    PartialData,

    /// <summary>
    /// Inconsistencia en datos
    /// </summary>
    DataInconsistency,

    /// <summary>
    /// Ejecutivo faltante
    /// </summary>
    MissingExecutive,

    /// <summary>
    /// Datos desactualizados
    /// </summary>
    OutdatedData,

    /// <summary>
    /// Rendimiento bajo detectado
    /// </summary>
    LowPerformance,

    /// <summary>
    /// Problema de configuración
    /// </summary>
    ConfigurationIssue,

    /// <summary>
    /// Formato de datos no estándar
    /// </summary>
    NonStandardFormat,

    /// <summary>
    /// Datos duplicados encontrados
    /// </summary>
    DuplicateData,

    /// <summary>
    /// Umbral de calidad bajo
    /// </summary>
    QualityThresholdBreach
}

/// <summary>
/// Severidad de warning
/// </summary>


/// <summary>
/// Cambio detectado durante la sincronización
/// </summary>
public class DataChangeDetectedDto
{
    /// <summary>
    /// Tipo de cambio
    /// </summary>
    public DataChangeType ChangeType { get; set; }

    /// <summary>
    /// Descripción del cambio
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Sponsor afectado
    /// </summary>
    public string? AffectedSponsor { get; set; }

    /// <summary>
    /// Ejecutivo afectado
    /// </summary>
    public string? AffectedExecutive { get; set; }

    /// <summary>
    /// Fecha del cambio
    /// </summary>
    public DateTime ChangeDate { get; set; }

    /// <summary>
    /// Valor anterior
    /// </summary>
    public string? OldValue { get; set; }

    /// <summary>
    /// Nuevo valor
    /// </summary>
    public string? NewValue { get; set; }

    /// <summary>
    /// Impacto del cambio
    /// </summary>
    public ChangeImpact Impact { get; set; }

    /// <summary>
    /// Campo específico que cambió
    /// </summary>
    public string? ChangedField { get; set; }

    /// <summary>
    /// Timestamp de detección
    /// </summary>
    public DateTime DetectedAt { get; set; } = DateTime.UtcNow;
}




/// <summary>
/// Métricas de rendimiento de la sincronización
/// </summary>
public class SyncPerformanceMetricsDto
{
    /// <summary>
    /// Tiempo promedio por sponsor (segundos)
    /// </summary>
    public decimal AverageTimePerSponsor { get; set; }

    /// <summary>
    /// Tiempo promedio por registro (milisegundos)
    /// </summary>
    public decimal AverageTimePerRecord { get; set; }

    /// <summary>
    /// Registros procesados por segundo
    /// </summary>
    public decimal RecordsPerSecond { get; set; }

    /// <summary>
    /// Uso máximo de memoria durante la sincronización (MB)
    /// </summary>
    public decimal PeakMemoryUsageMB { get; set; }

    /// <summary>
    /// Uso promedio de CPU durante la sincronización (%)
    /// </summary>
    public decimal AverageCpuUsage { get; set; }

    /// <summary>
    /// Latencia promedio de Google Sheets API (ms)
    /// </summary>
    public decimal AverageApiLatency { get; set; }

    /// <summary>
    /// Número de conexiones concurrentes utilizadas
    /// </summary>
    public int ConcurrentConnections { get; set; }

    /// <summary>
    /// Eficiencia de la sincronización (0-100)
    /// </summary>
    public decimal SyncEfficiency { get; set; }

    /// <summary>
    /// Overhead de la sincronización (%)
    /// </summary>
    public decimal SyncOverhead { get; set; }
}

/// <summary>
/// Estado del sistema después de la sincronización
/// </summary>
public class PostSyncSystemStateDto
{
    /// <summary>
    /// Total de llamadas en el sistema
    /// </summary>
    public int TotalCallsInSystem { get; set; }

    /// <summary>
    /// Último registro procesado
    /// </summary>
    public DateTime? LastRecordTimestamp { get; set; }

    /// <summary>
    /// Número de alertas activas
    /// </summary>
    public int ActiveAlerts { get; set; }

    /// <summary>
    /// Sponsors con problemas de datos
    /// </summary>
    public int SponsorsWithDataIssues { get; set; }

    /// <summary>
    /// Calidad general de datos (0-100)
    /// </summary>
    public decimal OverallDataQuality { get; set; }

    /// <summary>
    /// Estado de salud del sistema (0-100)
    /// </summary>
    public decimal SystemHealthScore { get; set; }

    /// <summary>
    /// Próxima fecha de datos esperada
    /// </summary>
    public DateTime? NextExpectedDataDate { get; set; }
}