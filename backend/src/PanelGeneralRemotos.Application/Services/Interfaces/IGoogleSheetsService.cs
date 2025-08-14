// ============================================================================
// Archivo: IGoogleSheetsService.cs
// Propósito: Interface principal para lectura de datos desde Google Sheets
// Creado: 11/08/2025 - Initial creation con métodos para actualización dinámica
// Modificado: 11/08/2025 - Added real-time sync methods for dashboard refresh
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Application/Services/Interfaces/IGoogleSheetsService.cs
// ============================================================================

using PanelGeneralRemotos.Domain.Entities;
using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Services.Interfaces;

/// <summary>
/// Servicio principal para la integración con Google Sheets API
/// Maneja la lectura dinámica de datos cuando el usuario presiona "Actualizar"
/// </summary>
public interface IGoogleSheetsService
{
    /// <summary>
    /// Sincroniza TODOS los datos desde Google Sheets
    /// Este método se ejecuta cuando el usuario presiona "Actualizar" en el dashboard
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la sincronización con estadísticas</returns>
    Task<SyncResult> SyncAllSheetsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sincroniza datos de un sponsor específico
    /// </summary>
    /// <param name="sponsorId">ID del sponsor a sincronizar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la sincronización</returns>
    Task<SyncResult> SyncSponsorDataAsync(int sponsorId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sincroniza datos de un ejecutivo específico
    /// </summary>
    /// <param name="executiveId">ID del ejecutivo</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la sincronización</returns>
    Task<SyncResult> SyncExecutiveDataAsync(int executiveId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lee datos en tiempo real de una hoja específica de Google Sheets
    /// </summary>
    /// <param name="configuration">Configuración de la hoja a leer</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Datos procesados de la hoja</returns>
    Task<SheetDataResult> ReadSheetDataAsync(GoogleSheetConfiguration configuration, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica el estado de conexión con Google Sheets API
    /// MÉTODO FALTANTE - usado en TestController
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Estado de la conexión</returns>
    Task<ConnectionStatus> CheckConnectionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene la fecha/hora de la última sincronización exitosa
    /// </summary>
    /// <returns>Fecha de última sincronización o null si nunca se sincronizó</returns>
    Task<DateTime?> GetLastSyncDateAsync();

    /// <summary>
    /// Obtiene el estado actual de todas las configuraciones de Google Sheets
    /// MÉTODO FALTANTE - usado en DashboardService
    /// </summary>
    /// <returns>Lista con el estado de cada configuración</returns>
    Task<List<SheetStatusInfo>> GetAllSheetsStatusAsync();

    /// <summary>
    /// Valida que una configuración de Google Sheets sea accesible
    /// </summary>
    /// <param name="configuration">Configuración a validar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la validación</returns>
    Task<ValidationResult> ValidateSheetConfigurationAsync(GoogleSheetConfiguration configuration, CancellationToken cancellationToken = default);

    /// <summary>
    /// Procesa datos crudos de Google Sheets y los convierte en entidades del dominio
    /// </summary>
    /// <param name="rawData">Datos crudos de la hoja</param>
    /// <param name="configuration">Configuración de la hoja</param>
    /// <returns>Entidades procesadas</returns>
    Task<ProcessedSheetData> ProcessSheetDataAsync(object[][] rawData, GoogleSheetConfiguration configuration);

    /// <summary>
    /// Obtiene estadísticas de sincronización para el dashboard
    /// MÉTODO FALTANTE - usado en DashboardService  
    /// </summary>
    /// <returns>Estadísticas de la última sincronización</returns>
    Task<SyncStatistics> GetSyncStatisticsAsync();
}

/// <summary>
/// Resultado de una operación de sincronización
/// </summary>
public class SyncResult
{
    /// <summary>
    /// Indica si la sincronización fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Número total de hojas procesadas
    /// </summary>
    public int SheetsProcessed { get; set; }

    /// <summary>
    /// Número de hojas con errores
    /// </summary>
    public int SheetsWithErrors { get; set; }

    /// <summary>
    /// Número de registros de llamadas actualizados
    /// </summary>
    public int CallRecordsUpdated { get; set; }

    /// <summary>
    /// Número de métricas de rendimiento actualizadas
    /// </summary>
    public int PerformanceMetricsUpdated { get; set; }

    /// <summary>
    /// Duración de la sincronización
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Errores ocurridos durante la sincronización
    /// </summary>
    public List<SyncError> Errors { get; set; } = new();

    /// <summary>
    /// Warnings ocurridos durante la sincronización
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Fecha y hora de la sincronización
    /// </summary>
    public DateTime SyncDateTime { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Resultado de lectura de una hoja específica
/// </summary>
public class SheetDataResult
{
    /// <summary>
    /// Datos leídos exitosamente
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Datos crudos de la hoja
    /// </summary>
    public object[][]? RawData { get; set; }

    /// <summary>
    /// Número de filas leídas
    /// </summary>
    public int RowsRead { get; set; }

    /// <summary>
    /// Mensaje de error si no fue exitoso
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Tipo de error ocurrido
    /// </summary>
    public SheetErrorType? ErrorType { get; set; }

    /// <summary>
    /// Metadatos de la hoja
    /// </summary>
    public SheetMetadata? Metadata { get; set; }
}

/// <summary>
/// Estado de conexión con Google Sheets
/// </summary>
public class ConnectionStatus
{
    /// <summary>
    /// Indica si la conexión es exitosa
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// Tiempo de respuesta de la API
    /// </summary>
    public TimeSpan ResponseTime { get; set; }

    /// <summary>
    /// Mensaje de estado
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Fecha y hora de la verificación
    /// </summary>
    public DateTime CheckDateTime { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Información de estado de una hoja específica
/// </summary>
public class SheetStatusInfo
{
    /// <summary>
    /// ID de la configuración
    /// </summary>
    public int ConfigurationId { get; set; }

    /// <summary>
    /// Nombre de la hoja
    /// </summary>
    public string SheetName { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del ejecutivo
    /// </summary>
    public string ExecutiveName { get; set; } = string.Empty;

    /// <summary>
    /// Estado actual de la sincronización
    /// </summary>
    public SyncStatus Status { get; set; }

    /// <summary>
    /// Última fecha de sincronización exitosa
    /// </summary>
    public DateTime? LastSyncDate { get; set; }

    /// <summary>
    /// Número de fallos consecutivos
    /// </summary>
    public int ConsecutiveFailures { get; set; }

    /// <summary>
    /// Último mensaje de error
    /// </summary>
    public string? LastErrorMessage { get; set; }
}

/// <summary>
/// Resultado de validación de configuración
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// Indica si la configuración es válida
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Lista de errores de validación
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Lista de warnings
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Información adicional sobre la hoja
    /// </summary>
    public SheetMetadata? SheetInfo { get; set; }
}

/// <summary>
/// Datos procesados de una hoja
/// </summary>
public class ProcessedSheetData
{
    /// <summary>
    /// Registros de llamadas extraídos
    /// </summary>
    public List<CallRecord> CallRecords { get; set; } = new();

    /// <summary>
    /// Métricas de rendimiento extraídas
    /// </summary>
    public List<PerformanceMetric> PerformanceMetrics { get; set; } = new();

    /// <summary>
    /// Número de filas procesadas
    /// </summary>
    public int ProcessedRows { get; set; }

    /// <summary>
    /// Número de filas con errores
    /// </summary>
    public int ErrorRows { get; set; }

    /// <summary>
    /// Errores de procesamiento
    /// </summary>
    public List<string> ProcessingErrors { get; set; } = new();
}

/// <summary>
/// Metadatos de una hoja de Google Sheets
/// </summary>
public class SheetMetadata
{
    /// <summary>
    /// Título de la hoja
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Número total de filas
    /// </summary>
    public int TotalRows { get; set; }

    /// <summary>
    /// Número total de columnas
    /// </summary>
    public int TotalColumns { get; set; }

    /// <summary>
    /// Fecha de última modificación
    /// </summary>
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// Headers detectados en la hoja
    /// </summary>
    public List<string> Headers { get; set; } = new();
}

/// <summary>
/// Error específico de sincronización
/// </summary>
public class SyncError
{
    /// <summary>
    /// ID de la configuración que causó el error
    /// </summary>
    public int ConfigurationId { get; set; }

    /// <summary>
    /// Nombre de la hoja
    /// </summary>
    public string SheetName { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de error
    /// </summary>
    public SheetErrorType ErrorType { get; set; }

    /// <summary>
    /// Mensaje descriptivo del error
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Excepción original si está disponible
    /// </summary>
    public string? Exception { get; set; }

    /// <summary>
    /// Fecha y hora del error
    /// </summary>
    public DateTime ErrorDateTime { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Estadísticas de sincronización
/// </summary>
public class SyncStatistics
{
    /// <summary>
    /// Fecha de última sincronización exitosa
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
    /// Hojas con errores
    /// </summary>
    public int FailedSheets { get; set; }

    /// <summary>
    /// Duración promedio de sincronización
    /// </summary>
    public TimeSpan AverageSyncDuration { get; set; }

    /// <summary>
    /// Número total de registros sincronizados hoy
    /// </summary>
    public int TotalRecordsSyncedToday { get; set; }
}