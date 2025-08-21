// ============================================================================
// Archivo: ConsolidatedMissingDTOs.cs
// Propósito: TODOS los DTOs faltantes consolidados para resolver errores CS0234 y CS0246
// Creado: 21/08/2025 - Corrección final de errores de compilación
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/ConsolidatedMissingDTOs.cs
// ============================================================================

using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Models.DTOs;

// ============================================================================
// SOLUCIÓN: Crear este archivo nuevo y ELIMINAR archivos duplicados:
// - MissingDTOs.cs (eliminar)
// - MissingDTOsComplement.cs (eliminar) 
// - Cualquier otro archivo que tenga estas definiciones
// ============================================================================

// ============================================================================
// DTOs FALTANTES IDENTIFICADOS EN LOS ERRORES
// ============================================================================

/// <summary>
/// Estadísticas de sincronización - Usado en ICallRecordService
/// CS0234: 'SyncStatistics' no existe en el namespace
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
    public decimal SuccessPercentage => TotalSheets > 0 ? 
        (decimal)SuccessfulSheets / TotalSheets * 100 : 0;
}

/// <summary>
/// DTO para estado de sincronización individual
/// CS0246: 'SyncStatusDto' no se encontró
/// </summary>
public class SyncStatusDto
{
    /// <summary>
    /// Nombre del componente
    /// </summary>
    public string ComponentName { get; set; } = string.Empty;

    /// <summary>
    /// Estado de salud de sincronización
    /// </summary>
    public SyncHealthStatus Status { get; set; }

    /// <summary>
    /// Última vez que se sincronizó
    /// </summary>
    public DateTime LastSyncTime { get; set; }

    /// <summary>
    /// Registros sincronizados
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
    /// Indica si hay problemas
    /// </summary>
    public bool HasIssues { get; set; }

    /// <summary>
    /// Próxima sincronización programada
    /// </summary>
    public DateTime? NextSyncScheduled { get; set; }
}

/// <summary>
/// Resultado de sincronización completa para GoogleSheetsService
/// CS0246: 'SyncAllResult' no se encontró
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
/// DTO para detalle de actualización por sponsor
/// CS0246: 'SponsorRefreshDetailDto' no se encontró
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
/// CS0246: 'ExecutiveRefreshDetailDto' no se encontró
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
/// CS0246: 'SponsorHealthStatus' no se encontró
/// </summary>
public enum SponsorHealthStatus
{
    /// <summary>
    /// Excelente estado
    /// </summary>
    Excellent,

    /// <summary>
    /// Buen estado
    /// </summary>
    Good,

    /// <summary>
    /// Estado con advertencias
    /// </summary>
    Warning,

    /// <summary>
    /// Estado crítico
    /// </summary>
    Critical,

    /// <summary>
    /// Fuera de línea
    /// </summary>
    Offline,

    /// <summary>
    /// Estado desconocido
    /// </summary>
    Unknown
}

/// <summary>
/// Estado de salud de sincronización
/// </summary>
public enum SyncHealthStatus
{
    /// <summary>
    /// Saludable
    /// </summary>
    Healthy,

    /// <summary>
    /// Con advertencias
    /// </summary>
    Warning,

    /// <summary>
    /// Crítico
    /// </summary>
    Critical,

    /// <summary>
    /// Desconocido
    /// </summary>
    Unknown
}

// ============================================================================
// DTOs AUXILIARES PARA COMPLETAR LAS DEFINICIONES
// ============================================================================

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

// ============================================================================
// INSTRUCCIONES DE IMPLEMENTACIÓN
// ============================================================================

/*
PASOS PARA RESOLVER LOS ERRORES:

1. CREAR este archivo: ConsolidatedMissingDTOs.cs

2. ELIMINAR archivos duplicados:
   - MissingDTOs.cs
   - MissingDTOsComplement.cs
   - OnlyMissingDTOs.cs (si aún existe)

3. VERIFICAR que no hay otras definiciones duplicadas en:
   - SyncResultDto.cs
   - RealTimeMetricsDto.cs
   - DashboardRefreshResultDto.cs

4. COMPILAR:
   cd D:\panel-general-remotos\backend\
   dotnet clean
   dotnet build

RESULTADO ESPERADO:
✅ Todos los errores CS0234 y CS0246 resueltos
✅ Compilación exitosa
✅ Una sola definición de cada DTO
*/