// ============================================================================
// Archivo: Enums.cs
// Propósito: Enumeraciones del dominio para estados y tipos del sistema
// Creado: 11/08/2025 - Initial creation con estados específicos del proyecto
// Modificado: 11/08/2025 - Added comprehensive enums for all system states
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Domain/Enums/Enums.cs
// ============================================================================

namespace PanelGeneralRemotos.Domain.Enums;

/// <summary>
/// Estado de sincronización de Google Sheets
/// </summary>
public enum SyncStatus
{
    /// <summary>
    /// Sincronización pendiente - aún no se ha intentado
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Sincronización en progreso
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Sincronización exitosa
    /// </summary>
    Success = 2,

    /// <summary>
    /// Sincronización fallida
    /// </summary>
    Failed = 3,

    /// <summary>
    /// Sincronización parcialmente exitosa (algunos datos con warnings)
    /// </summary>
    PartialSuccess = 4,

    /// <summary>
    /// Sincronización cancelada por el usuario o sistema
    /// </summary>
    Cancelled = 5
}

/// <summary>
/// Nivel de rendimiento del ejecutivo basado en métricas
/// </summary>
public enum PerformanceLevel
{
    /// <summary>
    /// Rendimiento pobre - menos del 50% de la meta
    /// </summary>
    Poor = 0,

    /// <summary>
    /// Rendimiento promedio - 50% a 69% de la meta
    /// </summary>
    Average = 1,

    /// <summary>
    /// Buen rendimiento - 70% a 89% de la meta
    /// </summary>
    Good = 2,

    /// <summary>
    /// Excelente rendimiento - 90% o más de la meta
    /// </summary>
    Excellent = 3
}

/// <summary>
/// Tipo de métrica de rendimiento
/// </summary>
public enum MetricType
{
    /// <summary>
    /// Total de llamadas realizadas
    /// </summary>
    TotalCalls = 0,

    /// <summary>
    /// Total de contactos gestionados
    /// </summary>
    TotalManaged = 1,

    /// <summary>
    /// Contactos efectivamente contactados
    /// </summary>
    Contacted = 2,

    /// <summary>
    /// Contactos que mostraron interés
    /// </summary>
    Interested = 3,

    /// <summary>
    /// Contactos cerrados/convertidos
    /// </summary>
    Closed = 4,

    /// <summary>
    /// Porcentaje de cumplimiento de meta
    /// </summary>
    GoalPercentage = 5
}

/// <summary>
/// Período de tiempo para las métricas
/// </summary>
public enum TimePeriod
{
    /// <summary>
    /// Métricas del día actual
    /// </summary>
    Daily = 0,

    /// <summary>
    /// Métricas de la semana actual
    /// </summary>
    Weekly = 1,

    /// <summary>
    /// Métricas del mes actual
    /// </summary>
    Monthly = 2,

    /// <summary>
    /// Métricas del trimestre actual
    /// </summary>
    Quarterly = 3,

    /// <summary>
    /// Métricas del año actual
    /// </summary>
    Yearly = 4
}

/// <summary>
/// Estado de procesamiento de datos
/// </summary>
public enum ProcessingStatus
{
    /// <summary>
    /// Datos sin procesar
    /// </summary>
    Unprocessed = 0,

    /// <summary>
    /// Procesamiento en curso
    /// </summary>
    Processing = 1,

    /// <summary>
    /// Datos procesados exitosamente
    /// </summary>
    Processed = 2,

    /// <summary>
    /// Error en el procesamiento
    /// </summary>
    Error = 3,

    /// <summary>
    /// Datos invalidados - necesitan reprocesamiento
    /// </summary>
    Invalidated = 4
}

/// <summary>
/// Tipo de error en Google Sheets
/// </summary>
public enum SheetErrorType
{
    /// <summary>
    /// Error de conexión o acceso
    /// </summary>
    ConnectionError = 0,

    /// <summary>
    /// Estructura de hoja inválida o modificada
    /// </summary>
    StructureError = 1,

    /// <summary>
    /// Datos faltantes o incompletos
    /// </summary>
    MissingData = 2,

    /// <summary>
    /// Formato de datos inválido
    /// </summary>
    InvalidFormat = 3,

    /// <summary>
    /// Permisos insuficientes
    /// </summary>
    PermissionDenied = 4,

    /// <summary>
    /// Hoja no encontrada
    /// </summary>
    SheetNotFound = 5,

    /// <summary>
    /// Límite de API excedido
    /// </summary>
    RateLimitExceeded = 6
}

/// <summary>
/// Prioridad de notificación
/// </summary>
public enum NotificationPriority
{
    /// <summary>
    /// Información general
    /// </summary>
    Info = 0,

    /// <summary>
    /// Advertencia que requiere atención
    /// </summary>
    Warning = 1,

    /// <summary>
    /// Error que requiere acción inmediata
    /// </summary>
    Error = 2,

    /// <summary>
    /// Error crítico del sistema
    /// </summary>
    Critical = 3
}

/// <summary>
/// Estado del sponsor
/// </summary>
public enum SponsorStatus
{
    /// <summary>
    /// Sponsor activo y funcionando normalmente
    /// </summary>
    Active = 0,

    /// <summary>
    /// Sponsor inactivo temporalmente
    /// </summary>
    Inactive = 1,

    /// <summary>
    /// Sponsor suspendido
    /// </summary>
    Suspended = 2,

    /// <summary>
    /// Sponsor con problemas técnicos
    /// </summary>
    TechnicalIssues = 3
}
public enum CallStatus
{
    Unknown = 0,
    NotManaged = 1,         // Sin Gestión
    InProgress = 2,         // En Gestión  
    NotContacted = 3,       // No Contactado
    Contacted = 4,          // Contactado
    NotInterested = 5,      // Sin Interés
    Interested = 6,         // Interesado
    Closed = 7             // Cerrado
}
public enum DateRange
{
    Today = 0,
    Yesterday = 1,
    ThisWeek = 2,
    LastWeek = 3,
    ThisMonth = 4,
    LastMonth = 5,
    LastThreeMonths = 6,
    ThisYear = 7,
    Custom = 8
}