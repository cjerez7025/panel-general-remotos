// ============================================================================
// Archivo: RealTimeMetricsDto.cs
// Propósito: DTO para métricas en tiempo real (SignalR) - ARCHIVO COMPLETO
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/RealTimeMetricsDto.cs
// ============================================================================
using PanelGeneralRemotos.Domain.Enums;
namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// Información de sesión
/// </summary>
public class SessionInfoDto
{
    /// <summary>
    /// ID de la sesión
    /// </summary>
    public string SessionId { get; set; } = string.Empty;

    /// <summary>
    /// Usuario actual
    /// </summary>
    public string? CurrentUser { get; set; }

    /// <summary>
    /// Inicio de sesión
    /// </summary>
    public DateTime SessionStarted { get; set; }

    /// <summary>
    /// Última actividad
    /// </summary>
    public DateTime LastActivity { get; set; }

    /// <summary>
    /// Número de actualizaciones recibidas
    /// </summary>
    public int UpdatesReceived { get; set; }

    /// <summary>
    /// Página/vista actual
    /// </summary>
    public string? CurrentView { get; set; }

    /// <summary>
    /// Filtros activos
    /// </summary>
    public Dictionary<string, object> ActiveFilters { get; set; } = new();

    /// <summary>
    /// Preferencias del usuario
    /// </summary>
    public Dictionary<string, object> UserPreferences { get; set; } = new();

    /// <summary>
    /// Timezone del cliente
    /// </summary>
    public string ClientTimezone { get; set; } = "America/Santiago";

    /// <summary>
    /// Idioma del cliente
    /// </summary>
    public string ClientLanguage { get; set; } = "es-CL";
}

/// <summary>
/// Heartbeat del servidor
/// </summary>
public class ServerHeartbeatDto
{
    /// <summary>
    /// Timestamp del heartbeat
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Versión del servidor
    /// </summary>
    public string ServerVersion { get; set; } = string.Empty;

    /// <summary>
    /// Estado del servidor
    /// </summary>
    public ServerStatus Status { get; set; }

    /// <summary>
    /// Uptime en segundos
    /// </summary>
    public long UptimeSeconds { get; set; }

    /// <summary>
    /// Número de secuencia del heartbeat
    /// </summary>
    public long SequenceNumber { get; set; }

    /// <summary>
    /// Carga actual del servidor (0-100)
    /// </summary>
    public decimal ServerLoad { get; set; }

    /// <summary>
    /// Servicios en ejecución
    /// </summary>
    public List<string> RunningServices { get; set; } = new();

    /// <summary>
    /// Servicios con problemas
    /// </summary>
    public List<string> ProblematicServices { get; set; } = new();

    /// <summary>
    /// Próximo mantenimiento programado
    /// </summary>
    public DateTime? NextScheduledMaintenance { get; set; }

    /// <summary>
    /// Modo de mantenimiento activo
    /// </summary>
    public bool MaintenanceModeActive { get; set; }

    /// <summary>
    /// Ambiente (Development, Staging, Production)
    /// </summary>
    public string Environment { get; set; } = string.Empty;
}

/// <summary>
/// Estado del servidor
/// </summary>
public enum ServerStatus
{
    /// <summary>
    /// Funcionando normalmente
    /// </summary>
    Online,

    /// <summary>
    /// Funcionando con advertencias
    /// </summary>
    OnlineWithWarnings,

    /// <summary>
    /// Rendimiento degradado
    /// </summary>
    Degraded,

    /// <summary>
    /// En mantenimiento
    /// </summary>
    Maintenance,

    /// <summary>
    /// Fuera de línea
    /// </summary>
    Offline,

    /// <summary>
    /// Error crítico
    /// </summary>
    CriticalError
}

/// <summary>
/// Configuración de actualizaciones en tiempo real
/// </summary>
public class RealTimeConfigDto
{
    /// <summary>
    /// Intervalo de actualización en milisegundos
    /// </summary>
    public int UpdateIntervalMs { get; set; } = 5000;

    /// <summary>
    /// Intervalo de heartbeat en milisegundos
    /// </summary>
    public int HeartbeatIntervalMs { get; set; } = 30000;

    /// <summary>
    /// Habilitar actualizaciones de quick stats
    /// </summary>
    public bool EnableQuickStatsUpdates { get; set; } = true;

    /// <summary>
    /// Habilitar notificaciones de alertas
    /// </summary>
    public bool EnableAlertNotifications { get; set; } = true;

    /// <summary>
    /// Habilitar notificaciones de cambios
    /// </summary>
    public bool EnableChangeNotifications { get; set; } = true;

    /// <summary>
    /// Habilitar métricas de rendimiento
    /// </summary>
    public bool EnablePerformanceMetrics { get; set; } = false;

    /// <summary>
    /// Sponsors a monitorear específicamente
    /// </summary>
    public List<string> MonitoredSponsors { get; set; } = new();

    /// <summary>
    /// Nivel de detalle de las actualizaciones
    /// </summary>
    public RealTimeDetailLevel DetailLevel { get; set; } = RealTimeDetailLevel.Standard;

    /// <summary>
    /// Comprimir datos enviados
    /// </summary>
    public bool CompressData { get; set; } = true;

    /// <summary>
    /// Buffer de eventos (número máximo de eventos en memoria)
    /// </summary>
    public int EventBufferSize { get; set; } = 100;
}

/// <summary>
/// Nivel de detalle de actualizaciones en tiempo real
/// </summary>
public enum RealTimeDetailLevel
{
    /// <summary>
    /// Mínimo (solo cambios críticos)
    /// </summary>
    Minimal,

    /// <summary>
    /// Básico (cambios importantes)
    /// </summary>
    Basic,

    /// <summary>
    /// Estándar (configuración normal)
    /// </summary>
    Standard,

    /// <summary>
    /// Detallado (incluye métricas adicionales)
    /// </summary>
    Detailed,

    /// <summary>
    /// Completo (todos los datos disponibles)
    /// </summary>
    Comprehensive
}

/// <summary>
/// Respuesta de suscripción a actualizaciones en tiempo real
/// </summary>
public class RealTimeSubscriptionDto
{
    /// <summary>
    /// ID único de la suscripción
    /// </summary>
    public string SubscriptionId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// ID de conexión SignalR
    /// </summary>
    public string ConnectionId { get; set; } = string.Empty;

    /// <summary>
    /// Usuario suscrito
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Grupos suscritos
    /// </summary>
    public List<string> SubscribedGroups { get; set; } = new();

    /// <summary>
    /// Tipos de eventos suscritos
    /// </summary>
    public List<RealTimeUpdateType> SubscribedEventTypes { get; set; } = new();

    /// <summary>
    /// Filtros aplicados a la suscripción
    /// </summary>
    public Dictionary<string, object> SubscriptionFilters { get; set; } = new();

    /// <summary>
    /// Configuración de la suscripción
    /// </summary>
    public RealTimeConfigDto SubscriptionConfig { get; set; } = new();

    /// <summary>
    /// Timestamp de inicio de suscripción
    /// </summary>
    public DateTime SubscriptionStartTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Última actividad de la suscripción
    /// </summary>
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Número de actualizaciones enviadas
    /// </summary>
    public int UpdatesSent { get; set; }

    /// <summary>
    /// Estado de la suscripción
    /// </summary>
    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;
}

/// <summary>
/// Estado de la suscripción
/// </summary>
public enum SubscriptionStatus
{
    /// <summary>
    /// Suscripción activa
    /// </summary>
    Active,

    /// <summary>
    /// Suscripción pausada
    /// </summary>
    Paused,

    /// <summary>
    /// Suscripción inactiva
    /// </summary>
    Inactive,

    /// <summary>
    /// Suscripción expirada
    /// </summary>
    Expired,

    /// <summary>
    /// Error en la suscripción
    /// </summary>
    Error
}

/// <summary>
/// Estadísticas de broadcasting en tiempo real
/// </summary>
public class RealTimeBroadcastStatsDto
{
    /// <summary>
    /// Número total de clientes conectados
    /// </summary>
    public int TotalConnectedClients { get; set; }

    /// <summary>
    /// Número de suscripciones activas
    /// </summary>
    public int ActiveSubscriptions { get; set; }

    /// <summary>
    /// Mensajes enviados en la última hora
    /// </summary>
    public int MessagesLastHour { get; set; }

    /// <summary>
    /// Mensajes enviados hoy
    /// </summary>
    public int MessagesToday { get; set; }

    /// <summary>
    /// Promedio de latencia de entrega (ms)
    /// </summary>
    public decimal AverageDeliveryLatency { get; set; }

    /// <summary>
    /// Tasa de éxito de entrega (%)
    /// </summary>
    public decimal DeliverySuccessRate { get; set; }

    /// <summary>
    /// Clientes por grupo
    /// </summary>
    public Dictionary<string, int> ClientsByGroup { get; set; } = new();

    /// <summary>
    /// Distribución por tipo de cliente
    /// </summary>
    public Dictionary<string, int> ClientTypeDistribution { get; set; } = new();

    /// <summary>
    /// Último broadcast enviado
    /// </summary>
    public DateTime? LastBroadcastTime { get; set; }

    /// <summary>
    /// Próximo broadcast programado
    /// </summary>
    public DateTime? NextScheduledBroadcast { get; set; }
}
/// DTO para métricas en tiempo real enviadas vía SignalR
/// </summary>
public class RealTimeMetricsDto
{
    /// <summary>
    /// Timestamp de las métricas
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// ID único del broadcast
    /// </summary>
    public string BroadcastId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tipo de actualización en tiempo real
    /// </summary>
    public RealTimeUpdateType UpdateType { get; set; }

    /// <summary>
    /// Estadísticas rápidas actualizadas
    /// </summary>
    public QuickStatsDto QuickStats { get; set; } = new();

    /// <summary>
    /// Alertas activas del sistema
    /// </summary>
    public List<SystemAlertDto> ActiveAlerts { get; set; } = new();

    /// <summary>
    /// Estado de sincronización de todos los sponsors
    /// </summary>
    public List<SyncStatusDto> SyncStatuses { get; set; } = new();

    /// <summary>
    /// Cambios detectados desde la última actualización
    /// </summary>
    public List<ChangeNotificationDto> RecentChanges { get; set; } = new();

    /// <summary>
    /// Métricas de rendimiento del sistema
    /// </summary>
    public SystemPerformanceDto SystemPerformance { get; set; } = new();

    /// <summary>
    /// Estadísticas de conectividad
    /// </summary>
    public ConnectivityStatsDto ConnectivityStats { get; set; } = new();

    /// <summary>
    /// Eventos de sistema recientes
    /// </summary>
    public List<SystemEventDto> SystemEvents { get; set; } = new();

    /// <summary>
    /// Métricas específicas por sponsor
    /// </summary>
    public List<SponsorRealTimeMetricsDto> SponsorMetrics { get; set; } = new();

    /// <summary>
    /// Información de sesión
    /// </summary>
    public SessionInfoDto SessionInfo { get; set; } = new();

    /// <summary>
    /// Indica si hay actualizaciones críticas
    /// </summary>
    public bool HasCriticalUpdates { get; set; }

    /// <summary>
    /// Número de clientes conectados
    /// </summary>
    public int ConnectedClients { get; set; }

    /// <summary>
    /// Heartbeat del servidor
    /// </summary>
    public ServerHeartbeatDto ServerHeartbeat { get; set; } = new();

    /// <summary>
    /// Hash de estado para verificar cambios
    /// </summary>
    public string StateHash { get; set; } = string.Empty;
}

/// <summary>
/// Tipo de actualización en tiempo real
/// </summary>
public enum RealTimeUpdateType
{
    /// <summary>
    /// Actualización completa del dashboard
    /// </summary>
    FullUpdate,

    /// <summary>
    /// Actualización incremental
    /// </summary>
    IncrementalUpdate,

    /// <summary>
    /// Solo estadísticas rápidas
    /// </summary>
    QuickStatsOnly,

    /// <summary>
    /// Solo alertas
    /// </summary>
    AlertsOnly,

    /// <summary>
    /// Estado de sincronización
    /// </summary>
    SyncStatusUpdate,

    /// <summary>
    /// Evento crítico del sistema
    /// </summary>
    CriticalEvent,

    /// <summary>
    /// Heartbeat del servidor
    /// </summary>
    Heartbeat,

    /// <summary>
    /// Notificación de cambio de datos
    /// </summary>
    DataChange
}

/// <summary>
/// Notificación de cambio en tiempo real
/// </summary>
public class ChangeNotificationDto
{
    /// <summary>
    /// ID único del cambio
    /// </summary>
    public string ChangeId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tipo de cambio
    /// </summary>
    public string ChangeType { get; set; } = string.Empty;

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
    /// Entidad afectada
    /// </summary>
    public string? AffectedEntity { get; set; }

    /// <summary>
    /// Timestamp del cambio
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Severidad del cambio
    /// </summary>
    public ChangeSeverity Severity { get; set; }

    /// <summary>
    /// Acción recomendada
    /// </summary>
    public string? RecommendedAction { get; set; }

    /// <summary>
    /// Datos adicionales del cambio
    /// </summary>
    public Dictionary<string, object> ChangeData { get; set; } = new();

    /// <summary>
    /// Indica si requiere acción inmediata
    /// </summary>
    public bool RequiresImmediateAction { get; set; }

    /// <summary>
    /// URL para más detalles
    /// </summary>
    public string? DetailsUrl { get; set; }
}

/// <summary>
/// Severidad del cambio
/// </summary>
public enum ChangeSeverity
{
    /// <summary>
    /// Información
    /// </summary>
    Info,

    /// <summary>
    /// Advertencia
    /// </summary>
    Warning,

    /// <summary>
    /// Crítico
    /// </summary>
    Critical,

    /// <summary>
    /// Emergencia
    /// </summary>
    Emergency
}

/// <summary>
/// Rendimiento del sistema en tiempo real
/// </summary>
public class SystemPerformanceDto
{
    /// <summary>
    /// Uso de CPU (%)
    /// </summary>
    public decimal CpuUsage { get; set; }

    /// <summary>
    /// Uso de memoria (%)
    /// </summary>
    public decimal MemoryUsage { get; set; }

    /// <summary>
    /// Memoria disponible (MB)
    /// </summary>
    public decimal AvailableMemoryMB { get; set; }

    /// <summary>
    /// Latencia promedio de base de datos (ms)
    /// </summary>
    public decimal DatabaseLatency { get; set; }

    /// <summary>
    /// Latencia promedio de Google Sheets API (ms)
    /// </summary>
    public decimal GoogleSheetsLatency { get; set; }

    /// <summary>
    /// Número de conexiones activas a la base de datos
    /// </summary>
    public int ActiveDatabaseConnections { get; set; }

    /// <summary>
    /// Número de conexiones SignalR activas
    /// </summary>
    public int ActiveSignalRConnections { get; set; }

    /// <summary>
    /// Throughput de requests por segundo
    /// </summary>
    public decimal RequestsPerSecond { get; set; }

    /// <summary>
    /// Tiempo de respuesta promedio (ms)
    /// </summary>
    public decimal AverageResponseTime { get; set; }

    /// <summary>
    /// Tasa de errores (%)
    /// </summary>
    public decimal ErrorRate { get; set; }

    /// <summary>
    /// Uptime del servidor (horas)
    /// </summary>
    public decimal UptimeHours { get; set; }

    /// <summary>
    /// Último reinicio del servidor
    /// </summary>
    public DateTime? LastServerRestart { get; set; }

    /// <summary>
    /// Health score general del sistema (0-100)
    /// </summary>
    public decimal OverallHealthScore { get; set; }
}

/// <summary>
/// Estadísticas de conectividad
/// </summary>
public class ConnectivityStatsDto
{
    /// <summary>
    /// Estado de conectividad con Google Sheets
    /// </summary>
    public bool GoogleSheetsConnected { get; set; }

    /// <summary>
    /// Estado de conectividad con base de datos
    /// </summary>
    public bool DatabaseConnected { get; set; }

    /// <summary>
    /// Última verificación de conectividad
    /// </summary>
    public DateTime LastConnectivityCheck { get; set; }

    /// <summary>
    /// API calls restantes en Google Sheets
    /// </summary>
    public int RemainingGoogleApiCalls { get; set; }

    /// <summary>
    /// Límite diario de API calls
    /// </summary>
    public int DailyApiCallLimit { get; set; }

    /// <summary>
    /// API calls utilizadas hoy
    /// </summary>
    public int ApiCallsUsedToday { get; set; }

    /// <summary>
    /// Tiempo hasta reset de límite de API
    /// </summary>
    public TimeSpan? TimeToApiLimitReset { get; set; }

    /// <summary>
    /// Velocidad de red promedio (Mbps)
    /// </summary>
    public decimal AverageNetworkSpeed { get; set; }

    /// <summary>
    /// Latencia de red promedio (ms)
    /// </summary>
    public decimal AverageNetworkLatency { get; set; }

    /// <summary>
    /// Número de reintentos de conexión en la última hora
    /// </summary>
    public int ConnectionRetriesLastHour { get; set; }

    /// <summary>
    /// Tasa de éxito de conexiones (%)
    /// </summary>
    public decimal ConnectionSuccessRate { get; set; }
}

/// <summary>
/// Evento del sistema
/// </summary>
public class SystemEventDto
{
    /// <summary>
    /// ID único del evento
    /// </summary>
    public string EventId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tipo de evento
    /// </summary>
    public SystemEventType EventType { get; set; }

    /// <summary>
    /// Título del evento
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del evento
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp del evento
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Severidad del evento
    /// </summary>
    public EventSeverity Severity { get; set; }

    /// <summary>
    /// Sponsor relacionado (si aplica)
    /// </summary>
    public string? RelatedSponsor { get; set; }

    /// <summary>
    /// Usuario relacionado (si aplica)
    /// </summary>
    public string? RelatedUser { get; set; }

    /// <summary>
    /// Datos adicionales del evento
    /// </summary>
    public Dictionary<string, object> EventData { get; set; } = new();

    /// <summary>
    /// Indica si el evento fue resuelto
    /// </summary>
    public bool IsResolved { get; set; }

    /// <summary>
    /// Tiempo de resolución
    /// </summary>
    public DateTime? ResolvedAt { get; set; }
}

/// <summary>
/// Tipos de eventos del sistema
/// </summary>
public enum SystemEventType
{
    /// <summary>
    /// Sincronización iniciada
    /// </summary>
    SyncStarted,

    /// <summary>
    /// Sincronización completada
    /// </summary>
    SyncCompleted,

    /// <summary>
    /// Sincronización fallida
    /// </summary>
    SyncFailed,

    /// <summary>
    /// Usuario conectado
    /// </summary>
    UserConnected,

    /// <summary>
    /// Usuario desconectado
    /// </summary>
    UserDisconnected,

    /// <summary>
    /// Alerta generada
    /// </summary>
    AlertGenerated,

    /// <summary>
    /// Alerta resuelta
    /// </summary>
    AlertResolved,

    /// <summary>
    /// Configuración actualizada
    /// </summary>
    ConfigurationUpdated,

    /// <summary>
    /// Error del sistema
    /// </summary>
    SystemError,

    /// <summary>
    /// Mantenimiento iniciado
    /// </summary>
    MaintenanceStarted,

    /// <summary>
    /// Mantenimiento completado
    /// </summary>
    MaintenanceCompleted,

    /// <summary>
    /// Backup creado
    /// </summary>
    BackupCreated,

    /// <summary>
    /// Datos exportados
    /// </summary>
    DataExported
}

/// <summary>
/// Severidad del evento
/// </summary>
public enum EventSeverity
{
    /// <summary>
    /// Información
    /// </summary>
    Info,

    /// <summary>
    /// Advertencia
    /// </summary>
    Warning,

    /// <summary>
    /// Error
    /// </summary>
    Error,

    /// <summary>
    /// Crítico
    /// </summary>
    Critical
}

/// <summary>
/// Métricas en tiempo real específicas por sponsor
/// </summary>
public class SponsorRealTimeMetricsDto
{
    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Llamadas de hoy hasta ahora
    /// </summary>
    public int CallsToday { get; set; }

    /// <summary>
    /// Meta diaria
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento actual
    /// </summary>
    public decimal CurrentCompliance { get; set; }

    /// <summary>
    /// Tendencia actual (subiendo, bajando, estable)
    /// </summary>
    public string CurrentTrend { get; set; } = string.Empty;

    /// <summary>
    /// Último cambio detectado
    /// </summary>
    public DateTime? LastChangeDetected { get; set; }

    /// <summary>
    /// Ejecutivos activos ahora
    /// </summary>
    public int ActiveExecutivesNow { get; set; }

    /// <summary>
    /// Estado de salud del sponsor
    /// </summary>
    public SponsorHealthStatus HealthStatus { get; set; }

    /// <summary>
    /// Alertas activas del sponsor
    /// </summary>
    public int ActiveAlerts { get; set; }

    /// <summary>
    /// Estado de sincronización
    /// </summary>
    public SyncStatus SyncStatus { get; set; }

    /// <summary>
    /// Velocidad actual (llamadas por hora)
    /// </summary>
    public decimal CurrentVelocity { get; set; }

    /// <summary>
    /// Proyección de cumplimiento de meta diaria
    /// </summary>
    public decimal DailyGoalProjection { get; set; }
}

/// <summary>