// ============================================================================
// Archivo: DashboardRefreshResultDto.cs
// Propósito: DTO para el resultado del botón "ACTUALIZAR" del dashboard
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/DashboardRefreshResultDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para el resultado de la actualización manual del dashboard
/// Respuesta del botón "ACTUALIZAR" principal
/// </summary>

public class DashboardRefreshResultDto
{
    /// <summary>
    /// Indica si la actualización fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Timestamp de cuando se inició la actualización
    /// </summary>
    public DateTime RefreshStartTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp de cuando finalizó la actualización
    /// </summary>
    public DateTime RefreshEndTime { get; set; }

    /// <summary>
    /// Duración total de la actualización
    /// </summary>
    public TimeSpan Duration => RefreshEndTime - RefreshStartTime;

    /// <summary>
    /// Duración en segundos para mostrar en UI
    /// </summary>
    public double DurationSeconds => Duration.TotalSeconds;

    /// <summary>
    /// Mensaje principal del resultado
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Detalles adicionales del proceso
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Resultados de sincronización por sponsor
    /// </summary>
    public List<SponsorSyncResultDto> SponsorResults { get; set; } = new();

    /// <summary>
    /// Estadísticas de la sincronización
    /// </summary>
    public RefreshStatisticsDto Statistics { get; set; } = new();

    /// <summary>
    /// Errores ocurridos durante la actualización
    /// </summary>
    public List<RefreshErrorDto> Errors { get; set; } = new();

    /// <summary>
    /// Warnings generados durante la actualización
    /// </summary>
    public List<RefreshWarningDto> Warnings { get; set; } = new();

    /// <summary>
    /// Nuevas alertas generadas durante la sincronización
    /// </summary>
    public List<SystemAlertDto> NewAlerts { get; set; } = new();

    /// <summary>
    /// Alertas resueltas durante la sincronización
    /// </summary>
    public List<string> ResolvedAlerts { get; set; } = new();

    /// <summary>
    /// Datos actualizados para el dashboard
    /// </summary>
    public DashboardDataDto? UpdatedDashboardData { get; set; }

    /// <summary>
    /// Indica si se requiere refrescar la página
    /// </summary>
    public bool RequiresPageRefresh { get; set; } = false;

    /// <summary>
    /// Próxima sincronización automática programada
    /// </summary>
    public DateTime? NextAutomaticSync { get; set; }

    /// <summary>
    /// Estado de conectividad con Google Sheets
    /// </summary>
    public GoogleSheetsConnectivityDto GoogleSheetsStatus { get; set; } = new();

    /// <summary>
    /// Cambios detectados desde la última sincronización
    /// </summary>
    public List<DataChangeDto> DetectedChanges { get; set; } = new();
}

/// <summary>
/// Resultado de sincronización específico por sponsor
/// </summary>
public class SponsorSyncResultDto
{
    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// ID del sponsor
    /// </summary>
    public int SponsorId { get; set; }

    /// <summary>
    /// Indica si la sincronización del sponsor fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensaje específico del sponsor
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Tiempo que tomó sincronizar este sponsor
    /// </summary>
    public TimeSpan SyncDuration { get; set; }

    /// <summary>
    /// ID de la hoja de Google Sheets
    /// </summary>
    public string? GoogleSheetId { get; set; }

    /// <summary>
    /// Nombre de la hoja sincronizada
    /// </summary>
    public string? SheetName { get; set; }

    /// <summary>
    /// Número de registros procesados
    /// </summary>
    public int RecordsProcessed { get; set; }

    /// <summary>
    /// Número de registros creados
    /// </summary>
    public int RecordsCreated { get; set; }

    /// <summary>
    /// Número de registros actualizados
    /// </summary>
    public int RecordsUpdated { get; set; }

    /// <summary>
    /// Número de registros eliminados
    /// </summary>
    public int RecordsDeleted { get; set; }

    /// <summary>
    /// Errores específicos del sponsor
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Warnings específicos del sponsor
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Última fila procesada en Google Sheets
    /// </summary>
    public int? LastRowProcessed { get; set; }

    /// <summary>
    /// Ejecutivos sincronizados
    /// </summary>
    public List<ExecutiveSyncStatusDto> ExecutivesSync { get; set; } = new();

    /// <summary>
    /// Estado de salud del sponsor después de la sync
    /// </summary>
    public SponsorHealthStatus HealthStatus { get; set; }

    /// <summary>
    /// Cambios detectados en este sponsor
    /// </summary>
    public int ChangesDetected { get; set; }
}

/// <summary>
/// Estado de sincronización de un ejecutivo
/// </summary>
public class ExecutiveSyncStatusDto
{
    /// <summary>
    /// Nombre del ejecutivo
    /// </summary>
    public string ExecutiveName { get; set; } = string.Empty;

    /// <summary>
    /// ID del ejecutivo
    /// </summary>
    public int ExecutiveId { get; set; }

    /// <summary>
    /// Indica si la sincronización fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Número de llamadas sincronizadas
    /// </summary>
    public int CallsSynced { get; set; }

    /// <summary>
    /// Última fecha de datos sincronizada
    /// </summary>
    public DateTime? LastDataDate { get; set; }

    /// <summary>
    /// Errores específicos del ejecutivo
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Indica si el ejecutivo está activo
    /// </summary>
    public bool IsActive { get; set; }
}

/// <summary>
/// Estadísticas generales de la actualización
/// </summary>
public class RefreshStatisticsDto
{
    /// <summary>
    /// Total de sponsors procesados
    /// </summary>
    public int TotalSponsors { get; set; }

    /// <summary>
    /// Sponsors sincronizados exitosamente
    /// </summary>
    public int SuccessfulSponsors { get; set; }

    /// <summary>
    /// Sponsors con errores
    /// </summary>
    public int FailedSponsors { get; set; }

    /// <summary>
    /// Total de hojas de Google Sheets consultadas
    /// </summary>
    public int GoogleSheetsAccessed { get; set; }

    /// <summary>
    /// Total de registros procesados
    /// </summary>
    public int TotalRecordsProcessed { get; set; }

    /// <summary>
    /// Total de registros creados
    /// </summary>
    public int TotalRecordsCreated { get; set; }

    /// <summary>
    /// Total de registros actualizados
    /// </summary>
    public int TotalRecordsUpdated { get; set; }

    /// <summary>
    /// Total de registros eliminados
    /// </summary>
    public int TotalRecordsDeleted { get; set; }

    /// <summary>
    /// Ejecutivos procesados
    /// </summary>
    public int ExecutivesProcessed { get; set; }

    /// <summary>
    /// Ejecutivos activos encontrados
    /// </summary>
    public int ActiveExecutives { get; set; }

    /// <summary>
    /// Total de errores generados
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
    /// Memoria utilizada durante el proceso (MB)
    /// </summary>
    public decimal MemoryUsageMB { get; set; }
}

/// <summary>
/// Error ocurrido durante la actualización
/// </summary>
public class RefreshErrorDto
{
    /// <summary>
    /// ID único del error
    /// </summary>
    public string ErrorId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tipo de error
    /// </summary>
    public RefreshErrorType ErrorType { get; set; }

    /// <summary>
    /// Mensaje del error
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Detalles técnicos del error
    /// </summary>
    public string? Details { get; set; }

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
}

/// <summary>
/// Warning generado durante la actualización
/// </summary>
public class RefreshWarningDto
{
    /// <summary>
    /// ID único del warning
    /// </summary>
    public string WarningId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tipo de warning
    /// </summary>
    public RefreshWarningType WarningType { get; set; }

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
}

/// <summary>
/// Estado de conectividad con Google Sheets
/// </summary>
public class GoogleSheetsConnectivityDto
{
    /// <summary>
    /// Indica si hay conectividad general
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// Tiempo de respuesta promedio (ms)
    /// </summary>
    public int AverageResponseTimeMs { get; set; }

    /// <summary>
    /// Hojas accesibles
    /// </summary>
    public int AccessibleSheets { get; set; }

    /// <summary>
    /// Hojas con problemas
    /// </summary>
    public int ProblematicSheets { get; set; }

    /// <summary>
    /// API calls realizadas
    /// </summary>
    public int ApiCallsMade { get; set; }

    /// <summary>
    /// Límite de API calls restante
    /// </summary>
    public int RemainingApiCalls { get; set; }

    /// <summary>
    /// Último error de conectividad
    /// </summary>
    public string? LastConnectionError { get; set; }

    /// <summary>
    /// Timestamp del último test de conectividad
    /// </summary>
    public DateTime LastConnectivityTest { get; set; }
}

/// <summary>
/// Cambio detectado en los datos
/// </summary>
public class DataChangeDto
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
}

/// <summary>
/// Datos completos del dashboard después de la actualización
/// </summary>
public class DashboardDataDto
{
    /// <summary>
    /// Estadísticas rápidas actualizadas
    /// </summary>
    public QuickStatsDto QuickStats { get; set; } = new();

    /// <summary>
    /// Alertas del sistema actualizadas
    /// </summary>
    public List<SystemAlertDto> SystemAlerts { get; set; } = new();

    /// <summary>
    /// Resumen de llamadas por fecha
    /// </summary>
    public object? CallsSummaryData { get; set; }

    /// <summary>
    /// Datos de rendimiento y métricas
    /// </summary>
    public object? PerformanceData { get; set; }

    /// <summary>
    /// Timestamp de los datos
    /// </summary>
    public DateTime DataTimestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Tipos de error de actualización
/// </summary>
public enum RefreshErrorType
{
    GoogleSheetsConnection,
    GoogleSheetsAuthentication,
    GoogleSheetsDataStructure,
    DatabaseConnection,
    DatabaseTimeout,
    DataValidation,
    DataProcessing,
    MemoryLimits,
    Unknown
}

/// <summary>
/// Tipos de warning de actualización
/// </summary>
public enum RefreshWarningType
{
    SlowResponse,
    PartialData,
    DataInconsistency,
    MissingExecutive,
    OutdatedData,
    LowPerformance,
    ConfigurationIssue
}

/// <summary>
/// Severidad de warning
/// </summary>
public enum WarningSeverity
{
    Low,
    Medium,
    High
}

/// <summary>
/// Tipos de cambio en datos
/// </summary>
public enum DataChangeType
{
    NewExecutive,
    RemovedExecutive,
    UpdatedCalls,
    NewData,
    ModifiedGoals,
    StructureChange
}

/// <summary>
/// Impacto del cambio
/// </summary>
public enum ChangeImpact
{
    Low,
    Medium,
    High,
    Critical
}
