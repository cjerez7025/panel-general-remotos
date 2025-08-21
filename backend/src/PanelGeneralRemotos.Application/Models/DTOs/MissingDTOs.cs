// ============================================================================
// Archivo: MissingDTOs.cs
// Propósito: TODOS los DTOs que faltan para corregir errores de compilación
// Creado: 20/08/2025 - Corrección de errores CS0246
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/MissingDTOs.cs
// ============================================================================

using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Models.DTOs;

// ============================================================================
// DTOS FALTANTES PARA CALLRECORDSERVICE
// ============================================================================

/// <summary>
/// Estado de llamadas para reportes
/// </summary>
public enum CallsStatus
{
    Excellent,
    Good,
    Average,
    Poor,
    NoActivity,
    Unknown
}

// ============================================================================
// DTOS FALTANTES PARA DASHBOARDSERVICE
// ============================================================================

/// <summary>
/// Resultado de sincronización completa para GoogleSheetsService
/// </summary>
public class SyncAllResult
{
    /// <summary>
    /// Indica si la sincronización fue exitosa
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Timestamp de la sincronización
    /// </summary>
    public DateTime SyncTimestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Duración total de la sincronización
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Estadísticas de la sincronización
    /// </summary>
    public SyncStatistics Statistics { get; set; } = new();

    /// <summary>
    /// Lista de errores ocurridos
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Lista de warnings generados
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Detalles por sponsor sincronizado
    /// </summary>
    public List<SponsorSyncDetail> SponsorDetails { get; set; } = new();

    /// <summary>
    /// Indica si hay errores críticos
    /// </summary>
    public bool HasCriticalErrors => Errors.Any(e => e.Contains("Critical") || e.Contains("Error"));

    /// <summary>
    /// Número total de hojas procesadas
    /// </summary>
    public int TotalSheetsProcessed { get; set; }

    /// <summary>
    /// Número de hojas sincronizadas exitosamente
    /// </summary>
    public int SuccessfulSheets { get; set; }

    /// <summary>
    /// Número de hojas con errores
    /// </summary>
    public int FailedSheets { get; set; }
}

/// <summary>
/// Detalle de sincronización por sponsor
/// </summary>
public class SponsorSyncDetail
{
    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Indica si la sincronización fue exitosa
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Registros procesados para este sponsor
    /// </summary>
    public int RecordsProcessed { get; set; }

    /// <summary>
    /// Errores específicos del sponsor
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Ejecutivos sincronizados
    /// </summary>
    public List<ExecutiveSyncDetail> ExecutiveDetails { get; set; } = new();
}

/// <summary>
/// Detalle de sincronización por ejecutivo
/// </summary>
public class ExecutiveSyncDetail
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
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Registros procesados para este ejecutivo
    /// </summary>
    public int RecordsProcessed { get; set; }

    /// <summary>
    /// Última fecha de datos procesada
    /// </summary>
    public DateTime? LastDataDate { get; set; }
}

/// <summary>
/// DTO para detalle de actualización por sponsor
/// </summary>
public class SponsorRefreshDetailDto
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
    /// Indica si la actualización fue exitosa
    /// </summary>
    public bool IsSuccessful { get; set; }

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
    /// Última fecha de datos procesada
    /// </summary>
    public DateTime? LastDataDate { get; set; }

    /// <summary>
    /// Duración del proceso para este sponsor
    /// </summary>
    public TimeSpan ProcessingDuration { get; set; }

    /// <summary>
    /// Errores específicos del sponsor
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Warnings específicos del sponsor
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Detalles por ejecutivo
    /// </summary>
    public List<ExecutiveRefreshDetailDto> ExecutiveDetails { get; set; } = new();

    /// <summary>
    /// Estado de salud después de la actualización
    /// </summary>
    public SponsorHealthStatus HealthStatus { get; set; }

    /// <summary>
    /// Número de hojas de Google Sheets procesadas
    /// </summary>
    public int SheetsProcessed { get; set; }

    /// <summary>
    /// Timestamp de la actualización
    /// </summary>
    public DateTime UpdateTimestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// DTO para detalle de actualización por ejecutivo
/// </summary>
public class ExecutiveRefreshDetailDto
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
    /// Indica si la actualización fue exitosa
    /// </summary>
    public bool IsSuccessful { get; set; }

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
    /// Última fecha de datos procesada
    /// </summary>
    public DateTime? LastDataDate { get; set; }

    /// <summary>
    /// Duración del proceso para este ejecutivo
    /// </summary>
    public TimeSpan ProcessingDuration { get; set; }

    /// <summary>
    /// Errores específicos del ejecutivo
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Warnings específicos del ejecutivo
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Indica si el ejecutivo está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Configuración de Google Sheets utilizada
    /// </summary>
    public string? SheetConfiguration { get; set; }

    /// <summary>
    /// URL de la hoja de Google Sheets
    /// </summary>
    public string? SheetUrl { get; set; }

    /// <summary>
    /// Timestamp de la actualización
    /// </summary>
    public DateTime UpdateTimestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Estado de salud de un sponsor
/// </summary>
public enum SponsorHealthStatus
{
    Excellent,
    Good,
    Warning,
    Critical,
    Offline,
    Unknown
}

/// <summary>
/// Estadísticas de sincronización detalladas
/// </summary>
public class SyncStatistics
{
    /// <summary>
    /// Última sincronización exitosa
    /// </summary>
    public DateTime? LastSuccessfulSync { get; set; }

    /// <summary>
    /// Total de hojas configuradas
    /// </summary>
    public int TotalSheets { get; set; }

    /// <summary>
    /// Hojas sincronizadas exitosamente
    /// </summary>
    public int SuccessfulSheets { get; set; }

    /// <summary>
    /// Hojas con errores de sincronización
    /// </summary>
    public int FailedSheets { get; set; }

    /// <summary>
    /// Total de registros sincronizados hoy
    /// </summary>
    public int TotalRecordsSyncedToday { get; set; }

    /// <summary>
    /// Total de registros procesados en la última sincronización
    /// </summary>
    public int TotalRecordsProcessed { get; set; }

    /// <summary>
    /// Total de registros creados en la última sincronización
    /// </summary>
    public int TotalRecordsCreated { get; set; }

    /// <summary>
    /// Total de registros actualizados en la última sincronización
    /// </summary>
    public int TotalRecordsUpdated { get; set; }

    /// <summary>
    /// Total de registros eliminados en la última sincronización
    /// </summary>
    public int TotalRecordsDeleted { get; set; }

    /// <summary>
    /// Número de ejecutivos procesados
    /// </summary>
    public int ExecutivesProcessed { get; set; }

    /// <summary>
    /// Número de ejecutivos activos
    /// </summary>
    public int ActiveExecutives { get; set; }

    /// <summary>
    /// Duración promedio de sincronización
    /// </summary>
    public TimeSpan AverageSyncDuration { get; set; }

    /// <summary>
    /// Total de errores en la última sincronización
    /// </summary>
    public int TotalErrors { get; set; }

    /// <summary>
    /// Total de warnings en la última sincronización
    /// </summary>
    public int TotalWarnings { get; set; }

    /// <summary>
    /// Velocidad de procesamiento (registros por segundo)
    /// </summary>
    public decimal ProcessingSpeed { get; set; }

    /// <summary>
    /// Uso de memoria durante la sincronización (MB)
    /// </summary>
    public decimal MemoryUsageMB { get; set; }

    /// <summary>
    /// Porcentaje de éxito de la sincronización
    /// </summary>
    public decimal SuccessPercentage => TotalSheets > 0 ? (decimal)SuccessfulSheets / TotalSheets * 100 : 0;

    /// <summary>
    /// Indica si la sincronización fue completamente exitosa
    /// </summary>
    public bool IsHealthy => FailedSheets == 0 && TotalErrors == 0;
}

/// <summary>
/// Estado de salud de sincronización
/// </summary>
public enum SyncHealthStatus
{
    Healthy,
    Warning,
    Critical,
    Unknown
}

/// <summary>
/// DTO para estado de sincronización individual
/// </summary>
public class SyncStatusDto
{
    /// <summary>
    /// Nombre del componente o fuente de datos
    /// </summary>
    public string ComponentName { get; set; } = string.Empty;

    /// <summary>
    /// Estado de salud de la sincronización
    /// </summary>
    public SyncHealthStatus Status { get; set; }

    /// <summary>
    /// Última vez que se sincronizó exitosamente
    /// </summary>
    public DateTime LastSyncTime { get; set; }

    /// <summary>
    /// Próxima sincronización programada
    /// </summary>
    public DateTime? NextSyncTime { get; set; }

    /// <summary>
    /// Número de registros sincronizados en la última operación
    /// </summary>
    public int RecordsSynced { get; set; }

    /// <summary>
    /// Mensaje de error (si aplica)
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Duración de la última sincronización
    /// </summary>
    public TimeSpan? LastSyncDuration { get; set; }

    /// <summary>
    /// Número de intentos fallidos consecutivos
    /// </summary>
    public int ConsecutiveFailures { get; set; }

    /// <summary>
    /// Configuración utilizada para la sincronización
    /// </summary>
    public string? Configuration { get; set; }

    /// <summary>
    /// URL o identificador de la fuente de datos
    /// </summary>
    public string? DataSource { get; set; }

    /// <summary>
    /// Indica si el componente está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Timestamp de esta verificación de estado
    /// </summary>
    public DateTime CheckTimestamp { get; set; } = DateTime.UtcNow;
}