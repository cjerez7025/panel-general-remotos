// ============================================================================
// Archivo: SystemConfigurationDto.cs
// Propósito: DTO para configuración del sistema
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/SystemConfigurationDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para configuración completa del sistema
/// </summary>
public class SystemConfigurationDto
{
    /// <summary>
    /// Configuración de sponsors
    /// </summary>
    public List<SponsorConfigDto> Sponsors { get; set; } = new();

    /// <summary>
    /// Configuración de Google Sheets
    /// </summary>
    public GoogleSheetsConfigDto GoogleSheetsConfig { get; set; } = new();

    /// <summary>
    /// Configuración de sincronización
    /// </summary>
    public SyncConfigDto SyncConfig { get; set; } = new();

    /// <summary>
    /// Configuración de alertas
    /// </summary>
    public AlertConfigDto AlertConfig { get; set; } = new();

    /// <summary>
    /// Configuración de notificaciones
    /// </summary>
    public NotificationConfigDto NotificationConfig { get; set; } = new();

    /// <summary>
    /// Configuración de performance
    /// </summary>
    public PerformanceConfigDto PerformanceConfig { get; set; } = new();

    /// <summary>
    /// Configuración de seguridad
    /// </summary>
    public SecurityConfigDto SecurityConfig { get; set; } = new();

    /// <summary>
    /// Configuración de logging
    /// </summary>
    public LoggingConfigDto LoggingConfig { get; set; } = new();

    /// <summary>
    /// Configuración de la interfaz de usuario
    /// </summary>
    public UIConfigDto UIConfig { get; set; } = new();

    /// <summary>
    /// Configuración de integración
    /// </summary>
    public IntegrationConfigDto IntegrationConfig { get; set; } = new();

    /// <summary>
    /// Versión de la configuración
    /// </summary>
    public string ConfigurationVersion { get; set; } = "1.0";

    /// <summary>
    /// Última modificación de la configuración
    /// </summary>
    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Usuario que realizó la última modificación
    /// </summary>
    public string? LastModifiedBy { get; set; }

    /// <summary>
    /// Ambiente (Development, Staging, Production)
    /// </summary>
    public string Environment { get; set; } = "Development";

    /// <summary>
    /// Indica si la configuración está activa
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Configuraciones personalizadas adicionales
    /// </summary>
    public Dictionary<string, object> CustomSettings { get; set; } = new();
}

/// <summary>
/// Configuración de sponsor
/// </summary>
public class SponsorConfigDto
{
    /// <summary>
    /// ID del sponsor
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del sponsor
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Meta mensual del sponsor
    /// </summary>
    public int MonthlyGoal { get; set; }

    /// <summary>
    /// Meta diaria del sponsor
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Color hexadecimal para la UI
    /// </summary>
    public string? ColorHex { get; set; }

    /// <summary>
    /// Icono del sponsor
    /// </summary>
    public string? IconName { get; set; }

    /// <summary>
    /// Orden de visualización
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Indica si el sponsor está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Configuración específica del sponsor
    /// </summary>
    public SponsorSpecificConfigDto SpecificConfig { get; set; } = new();

    /// <summary>
    /// Ejecutivos del sponsor
    /// </summary>
    public List<ExecutiveConfigDto> Executives { get; set; } = new();

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Última modificación
    /// </summary>
    public DateTime? LastModified { get; set; }
}

/// <summary>
/// Configuración específica del sponsor
/// </summary>
public class SponsorSpecificConfigDto
{
    /// <summary>
    /// ID de la hoja de Google Sheets
    /// </summary>
    public string? GoogleSheetId { get; set; }

    /// <summary>
    /// Nombre de la hoja
    /// </summary>
    public string? SheetName { get; set; }

    /// <summary>
    /// Rango de datos en la hoja
    /// </summary>
    public string? DataRange { get; set; }

    /// <summary>
    /// Fila donde empiezan los datos
    /// </summary>
    public int DataStartRow { get; set; } = 2;

    /// <summary>
    /// Mapeo de columnas
    /// </summary>
    public Dictionary<string, string> ColumnMapping { get; set; } = new();

    /// <summary>
    /// Configuraciones de validación
    /// </summary>
    public ValidationConfigDto ValidationConfig { get; set; } = new();

    /// <summary>
    /// Timezone del sponsor
    /// </summary>
    public string TimeZone { get; set; } = "America/Santiago";

    /// <summary>
    /// Configuraciones adicionales
    /// </summary>
    public Dictionary<string, object> AdditionalSettings { get; set; } = new();
}

/// <summary>
/// Configuración de validación
/// </summary>
public class ValidationConfigDto
{
    /// <summary>
    /// Validar formato de fechas
    /// </summary>
    public bool ValidateDateFormat { get; set; } = true;

    /// <summary>
    /// Validar rangos numéricos
    /// </summary>
    public bool ValidateNumericRanges { get; set; } = true;

    /// <summary>
    /// Validar duplicados
    /// </summary>
    public bool ValidateDuplicates { get; set; } = true;

    /// <summary>
    /// Validar integridad referencial
    /// </summary>
    public bool ValidateReferentialIntegrity { get; set; } = true;

    /// <summary>
    /// Umbral de calidad mínimo (0-100)
    /// </summary>
    public decimal MinimumQualityThreshold { get; set; } = 80;

    /// <summary>
    /// Reglas de validación personalizadas
    /// </summary>
    public List<ValidationRuleDto> CustomValidationRules { get; set; } = new();
}

/// <summary>
/// Regla de validación personalizada
/// </summary>
public class ValidationRuleDto
{
    /// <summary>
    /// Nombre de la regla
    /// </summary>
    public string RuleName { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la regla
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Campo al que aplica
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de validación
    /// </summary>
    public string ValidationType { get; set; } = string.Empty;

    /// <summary>
    /// Parámetros de la validación
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new();

    /// <summary>
    /// Severidad del error si falla la validación
    /// </summary>
    public string ErrorSeverity { get; set; } = "Warning";

    /// <summary>
    /// Mensaje de error personalizado
    /// </summary>
    public string? CustomErrorMessage { get; set; }

    /// <summary>
    /// Indica si la regla está activa
    /// </summary>
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Configuración de ejecutivo
/// </summary>
public class ExecutiveConfigDto
{
    /// <summary>
    /// ID del ejecutivo
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo del ejecutivo
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Nombre corto/identificador
    /// </summary>
    public string ShortName { get; set; } = string.Empty;

    /// <summary>
    /// Email del ejecutivo
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Teléfono del ejecutivo
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Indica si el ejecutivo está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Meta diaria individual
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Meta mensual individual
    /// </summary>
    public int MonthlyGoal { get; set; }

    /// <summary>
    /// Fecha de ingreso
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Fecha de término (si aplica)
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Configuraciones adicionales del ejecutivo
    /// </summary>
    public Dictionary<string, object> AdditionalSettings { get; set; } = new();

    /// <summary>
    /// Horario de trabajo
    /// </summary>
    public WorkScheduleDto? WorkSchedule { get; set; }
}

/// <summary>
/// Horario de trabajo
/// </summary>
public class WorkScheduleDto
{
    /// <summary>
    /// Hora de inicio (HH:mm)
    /// </summary>
    public string StartTime { get; set; } = "08:00";

    /// <summary>
    /// Hora de fin (HH:mm)
    /// </summary>
    public string EndTime { get; set; } = "18:00";

    /// <summary>
    /// Días laborales
    /// </summary>
    public List<DayOfWeek> WorkingDays { get; set; } = new() 
    { 
        DayOfWeek.Monday, 
        DayOfWeek.Tuesday, 
        DayOfWeek.Wednesday, 
        DayOfWeek.Thursday, 
        DayOfWeek.Friday 
    };

    /// <summary>
    /// Zona horaria
    /// </summary>
    public string TimeZone { get; set; } = "America/Santiago";

    /// <summary>
    /// Excepciones (días no laborales específicos)
    /// </summary>
    public List<DateTime> Exceptions { get; set; } = new();
}

/// <summary>
/// Configuración de Google Sheets
/// </summary>
public class GoogleSheetsConfigDto
{
    /// <summary>
    /// Ruta del archivo de credenciales
    /// </summary>
    public string CredentialsPath { get; set; } = string.Empty;

    /// <summary>
    /// Nombre de la aplicación
    /// </summary>
    public string ApplicationName { get; set; } = "Panel General Remotos";

    /// <summary>
    /// IDs de las hojas por sponsor
    /// </summary>
    public Dictionary<string, string> SpreadsheetIds { get; set; } = new();

    /// <summary>
    /// Intervalo de actualización en minutos
    /// </summary>
    public int RefreshIntervalMinutes { get; set; } = 30;

    /// <summary>
    /// Tiempo de timeout en segundos
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Número máximo de reintentos
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Delay entre reintentos en milisegundos
    /// </summary>
    public int RetryDelayMs { get; set; } = 1000;

    /// <summary>
    /// Límite diario de API calls
    /// </summary>
    public int DailyApiCallLimit { get; set; } = 10000;

    /// <summary>
    /// Rate limiting habilitado
    /// </summary>
    public bool RateLimitingEnabled { get; set; } = true;

    /// <summary>
    /// Requests por minuto permitidos
    /// </summary>
    public int RequestsPerMinute { get; set; } = 100;

    /// <summary>
    /// Cache habilitado
    /// </summary>
    public bool CacheEnabled { get; set; } = true;

    /// <summary>
    /// Duración del cache en minutos
    /// </summary>
    public int CacheDurationMinutes { get; set; } = 5;

    /// <summary>
    /// Configuraciones avanzadas
    /// </summary>
    public Dictionary<string, object> AdvancedSettings { get; set; } = new();
}

/// <summary>
/// Configuración de sincronización
/// </summary>
public class SyncConfigDto
{
    /// <summary>
    /// Sincronización automática habilitada
    /// </summary>
    public bool AutoSyncEnabled { get; set; } = true;

    /// <summary>
    /// Intervalo de sincronización automática (minutos)
    /// </summary>
    public int AutoSyncIntervalMinutes { get; set; } = 30;

    /// <summary>
    /// Horario de sincronización (cron expression)
    /// </summary>
    public string? SyncSchedule { get; set; }

    /// <summary>
    /// Sincronización incremental habilitada
    /// </summary>
    public bool IncrementalSyncEnabled { get; set; } = true;

    /// <summary>
    /// Limpieza automática de datos obsoletos
    /// </summary>
    public bool AutoCleanupEnabled { get; set; } = true;

    /// <summary>
    /// Días de retención de datos
    /// </summary>
    public int DataRetentionDays { get; set; } = 90;

    /// <summary>
    /// Backup antes de sincronización
    /// </summary>
    public bool BackupBeforeSync { get; set; } = false;

    /// <summary>
    /// Validación de datos antes de sincronización
    /// </summary>
    public bool ValidateBeforeSync { get; set; } = true;

    /// <summary>
    /// Número máximo de registros por lote
    /// </summary>
    public int MaxRecordsPerBatch { get; set; } = 1000;

    /// <summary>
    /// Paralelismo habilitado
    /// </summary>
    public bool ParallelProcessingEnabled { get; set; } = true;

    /// <summary>
    /// Número máximo de hilos paralelos
    /// </summary>
    public int MaxParallelThreads { get; set; } = 4;

    /// <summary>
    /// Configuraciones adicionales de sincronización
    /// </summary>
    public Dictionary<string, object> AdditionalSyncSettings { get; set; } = new();
}

/// <summary>
/// Configuración de alertas
/// </summary>
public class AlertConfigDto
{
    /// <summary>
    /// Alertas habilitadas
    /// </summary>
    public bool AlertsEnabled { get; set; } = true;

    /// <summary>
    /// Umbral para alertas de bajo rendimiento (%)
    /// </summary>
    public decimal LowPerformanceThreshold { get; set; } = 70;

    /// <summary>
    /// Umbral para alertas críticas (%)
    /// </summary>
    public decimal CriticalPerformanceThreshold { get; set; } = 50;

    /// <summary>
    /// Umbral para alertas de datos obsoletos (horas)
    /// </summary>
    public int StaleDataThresholdHours { get; set; } = 24;

    /// <summary>
    /// Umbral para alertas de errores de sincronización
    /// </summary>
    public int SyncErrorThreshold { get; set; } = 3;

    /// <summary>
    /// Auto-resolución de alertas habilitada
    /// </summary>
    public bool AutoResolveAlerts { get; set; } = true;

    /// <summary>
    /// Tiempo para auto-resolución (horas)
    /// </summary>
    public int AutoResolveAfterHours { get; set; } = 24;

    /// <summary>
    /// Escalamiento de alertas habilitado
    /// </summary>
    public bool AlertEscalationEnabled { get; set; } = false;

    /// <summary>
    /// Configuraciones de escalamiento
    /// </summary>
    public AlertEscalationConfigDto? EscalationConfig { get; set; }

    /// <summary>
    /// Configuraciones de alertas personalizadas
    /// </summary>
    public List<CustomAlertConfigDto> CustomAlerts { get; set; } = new();
}

/// <summary>
/// Configuración de escalamiento de alertas
/// </summary>
public class AlertEscalationConfigDto
{
    /// <summary>
    /// Niveles de escalamiento
    /// </summary>
    public List<EscalationLevelDto> EscalationLevels { get; set; } = new();

    /// <summary>
    /// Tiempo entre escalamientos (minutos)
    /// </summary>
    public int EscalationIntervalMinutes { get; set; } = 60;

    /// <summary>
    /// Máximo número de escalamientos
    /// </summary>
    public int MaxEscalationLevels { get; set; } = 3;

    /// <summary>
    /// Escalamiento en fines de semana
    /// </summary>
    public bool EscalateOnWeekends { get; set; } = false;

    /// <summary>
    /// Horario de escalamiento
    /// </summary>
    public WorkScheduleDto? EscalationSchedule { get; set; }
}

/// <summary>
/// Nivel de escalamiento
/// </summary>
public class EscalationLevelDto
{
    /// <summary>
    /// Nivel de escalamiento (1, 2, 3, etc.)
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Descripción del nivel
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Destinatarios del escalamiento
    /// </summary>
    public List<string> Recipients { get; set; } = new();

    /// <summary>
    /// Métodos de notificación
    /// </summary>
    public List<string> NotificationMethods { get; set; } = new();

    /// <summary>
    /// Tiempo de espera antes de este nivel (minutos)
    /// </summary>
    public int WaitTimeMinutes { get; set; }
}

/// <summary>
/// Configuración de alerta personalizada
/// </summary>
public class CustomAlertConfigDto
{
    /// <summary>
    /// ID de la alerta personalizada
    /// </summary>
    public string AlertId { get; set; } = string.Empty;

    /// <summary>
    /// Nombre de la alerta
    /// </summary>
    public string AlertName { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la alerta
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Condición que activa la alerta
    /// </summary>
    public string TriggerCondition { get; set; } = string.Empty;

    /// <summary>
    /// Severidad de la alerta
    /// </summary>
    public string Severity { get; set; } = "Warning";

    /// <summary>
    /// Mensaje de la alerta
    /// </summary>
    public string AlertMessage { get; set; } = string.Empty;

    /// <summary>
    /// Destinatarios específicos
    /// </summary>
    public List<string> Recipients { get; set; } = new();

    /// <summary>
    /// Indica si la alerta está activa
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Frecuencia de evaluación (minutos)
    /// </summary>
    public int EvaluationFrequencyMinutes { get; set; } = 5;
}

/// <summary>
/// Configuración de notificaciones
/// </summary>
public class NotificationConfigDto
{
    /// <summary>
    /// Notificaciones por email habilitadas
    /// </summary>
    public bool EmailNotificationsEnabled { get; set; } = true;

    /// <summary>
    /// Servidor SMTP
    /// </summary>
    public string? SmtpServer { get; set; }

    /// <summary>
    /// Puerto SMTP
    /// </summary>
    public int SmtpPort { get; set; } = 587;

    /// <summary>
    /// Usuario SMTP
    /// </summary>
    public string? SmtpUsername { get; set; }

    /// <summary>
    /// SSL habilitado para SMTP
    /// </summary>
    public bool SmtpUseSsl { get; set; } = true;

    /// <summary>
    /// Destinatarios de notificaciones por defecto
    /// </summary>
    public List<string> DefaultNotificationRecipients { get; set; } = new();

    /// <summary>
    /// Remitente de emails
    /// </summary>
    public string? EmailSender { get; set; }

    /// <summary>
    /// Nombre del remitente
    /// </summary>
    public string? EmailSenderName { get; set; }

    /// <summary>
    /// Template de email por defecto
    /// </summary>
    public string? DefaultEmailTemplate { get; set; }

    /// <summary>
    /// Notificaciones push habilitadas
    /// </summary>
    public bool PushNotificationsEnabled { get; set; } = false;

    /// <summary>
    /// Configuración de notificaciones push
    /// </summary>
    public PushNotificationConfigDto? PushConfig { get; set; }

    /// <summary>
    /// Notificaciones SMS habilitadas
    /// </summary>
    public bool SmsNotificationsEnabled { get; set; } = false;

    /// <summary>
    /// Configuración de SMS
    /// </summary>
    public SmsNotificationConfigDto? SmsConfig { get; set; }
}

/// <summary>
/// Configuración de notificaciones push
/// </summary>
public class PushNotificationConfigDto
{
    /// <summary>
    /// Proveedor de push notifications
    /// </summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// API Key del proveedor
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Configuraciones adicionales
    /// </summary>
    public Dictionary<string, object> ProviderSettings { get; set; } = new();
}

/// <summary>
/// Configuración de notificaciones SMS
/// </summary>
public class SmsNotificationConfigDto
{
    /// <summary>
    /// Proveedor de SMS
    /// </summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// API Key del proveedor
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Número de teléfono remitente
    /// </summary>
    public string? SenderPhone { get; set; }

    /// <summary>
    /// Configuraciones adicionales
    /// </summary>
    public Dictionary<string, object> ProviderSettings { get; set; } = new();
}

/// <summary>
/// Configuración de rendimiento
/// </summary>
public class PerformanceConfigDto
{
    /// <summary>
    /// Cache habilitado
    /// </summary>
    public bool CacheEnabled { get; set; } = true;

    /// <summary>
    /// Duración del cache por defecto (minutos)
    /// </summary>
    public int DefaultCacheDurationMinutes { get; set; } = 15;

    /// <summary>
    /// Compresión de respuestas habilitada
    /// </summary>
    public bool CompressionEnabled { get; set; } = true;

    /// <summary>
    /// Paginación por defecto
    /// </summary>
    public int DefaultPageSize { get; set; } = 50;

    /// <summary>
    /// Tamaño máximo de página
    /// </summary>
    public int MaxPageSize { get; set; } = 500;

    /// <summary>
    /// Timeout de consultas a base de datos (segundos)
    /// </summary>
    public int DatabaseTimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Pool de conexiones mínimo
    /// </summary>
    public int MinConnectionPoolSize { get; set; } = 5;

    /// <summary>
    /// Pool de conexiones máximo
    /// </summary>
    public int MaxConnectionPoolSize { get; set; } = 100;

    /// <summary>
    /// Monitoreo de rendimiento habilitado
    /// </summary>
    public bool PerformanceMonitoringEnabled { get; set; } = true;

    /// <summary>
    /// Métricas de rendimiento habilitadas
    /// </summary>
    public bool PerformanceMetricsEnabled { get; set; } = true;
}

/// <summary>
/// Configuración de seguridad
/// </summary>
public class SecurityConfigDto
{
    /// <summary>
    /// Autenticación requerida
    /// </summary>
    public bool AuthenticationRequired { get; set; } = true;

    /// <summary>
    /// Tipo de autenticación
    /// </summary>
    public string AuthenticationType { get; set; } = "JWT";

    /// <summary>
    /// Duración del token (minutos)
    /// </summary>
    public int TokenDurationMinutes { get; set; } = 480;

    /// <summary>
    /// HTTPS requerido
    /// </summary>
    public bool HttpsRequired { get; set; } = true;

    /// <summary>
    /// CORS habilitado
    /// </summary>
    public bool CorsEnabled { get; set; } = true;

    /// <summary>
    /// Orígenes permitidos para CORS
    /// </summary>
    public List<string> AllowedOrigins { get; set; } = new();

    /// <summary>
    /// Rate limiting habilitado
    /// </summary>
    public bool RateLimitingEnabled { get; set; } = true;

    /// <summary>
    /// Límite de requests por minuto
    /// </summary>
    public int RequestsPerMinuteLimit { get; set; } = 100;

    /// <summary>
    /// Auditoría habilitada
    /// </summary>
    public bool AuditingEnabled { get; set; } = true;

    /// <summary>
    /// Encriptación de datos habilitada
    /// </summary>
    public bool DataEncryptionEnabled { get; set; } = false;

    /// <summary>
    /// Configuración de encriptación
    /// </summary>
    public EncryptionConfigDto? EncryptionConfig { get; set; }
}

/// <summary>
/// Configuración de encriptación
/// </summary>
public class EncryptionConfigDto
{
    /// <summary>
    /// Algoritmo de encriptación
    /// </summary>
    public string Algorithm { get; set; } = "AES";

    /// <summary>
    /// Tamaño de la clave
    /// </summary>
    public int KeySize { get; set; } = 256;

    /// <summary>
    /// Modo de encriptación
    /// </summary>
    public string Mode { get; set; } = "CBC";

    /// <summary>
    /// Padding utilizado
    /// </summary>
    public string Padding { get; set; } = "PKCS7";

    /// <summary>
    /// Configuraciones adicionales
    /// </summary>
    public Dictionary<string, object> AdditionalSettings { get; set; } = new();
}

/// <summary>
/// Configuración de logging
/// </summary>
public class LoggingConfigDto
{
    /// <summary>
    /// Nivel de logging mínimo
    /// </summary>
    public string MinimumLogLevel { get; set; } = "Information";

    /// <summary>
    /// Logging a archivo habilitado
    /// </summary>
    public bool FileLoggingEnabled { get; set; } = true;

    /// <summary>
    /// Ruta de archivos de log
    /// </summary>
    public string? LogFilePath { get; set; }

    /// <summary>
    /// Rotación de logs habilitada
    /// </summary>
    public bool LogRotationEnabled { get; set; } = true;

    /// <summary>
    /// Tamaño máximo de archivo de log (MB)
    /// </summary>
    public int MaxLogFileSizeMB { get; set; } = 50;

    /// <summary>
    /// Número máximo de archivos de log
    /// </summary>
    public int MaxLogFiles { get; set; } = 10;

    /// <summary>
    /// Logging estructurado habilitado
    /// </summary>
    public bool StructuredLoggingEnabled { get; set; } = true;

    /// <summary>
    /// Logging de requests HTTP
    /// </summary>
    public bool HttpRequestLoggingEnabled { get; set; } = true;

    /// <summary>
    /// Logging de consultas SQL
    /// </summary>
    public bool SqlQueryLoggingEnabled { get; set; } = false;

    /// <summary>
    /// Proveedores de logging externos
    /// </summary>
    public List<ExternalLoggingProviderDto> ExternalProviders { get; set; } = new();
}

/// <summary>
/// Proveedor de logging externo
/// </summary>
public class ExternalLoggingProviderDto
{
    /// <summary>
    /// Nombre del proveedor
    /// </summary>
    public string ProviderName { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de proveedor
    /// </summary>
    public string ProviderType { get; set; } = string.Empty;

    /// <summary>
    /// Configuración del proveedor
    /// </summary>
    public Dictionary<string, object> Configuration { get; set; } = new();

    /// <summary>
    /// Indica si está habilitado
    /// </summary>
    public bool IsEnabled { get; set; } = true;
}

/// <summary>
/// Configuración de interfaz de usuario
/// </summary>
public class UIConfigDto
{
    /// <summary>
    /// Tema por defecto
    /// </summary>
    public string DefaultTheme { get; set; } = "light";

    /// <summary>
    /// Idioma por defecto
    /// </summary>
    public string DefaultLanguage { get; set; } = "es-CL";

    /// <summary>
    /// Zona horaria por defecto
    /// </summary>
    public string DefaultTimeZone { get; set; } = "America/Santiago";

    /// <summary>
    /// Actualización automática habilitada
    /// </summary>
    public bool AutoRefreshEnabled { get; set; } = true;

    /// <summary>
    /// Intervalo de actualización automática (segundos)
    /// </summary>
    public int AutoRefreshIntervalSeconds { get; set; } = 30;

    /// <summary>
    /// Mostrar notificaciones toast
    /// </summary>
    public bool ShowToastNotifications { get; set; } = true;

    /// <summary>
    /// Duración de notificaciones toast (segundos)
    /// </summary>
    public int ToastDurationSeconds { get; set; } = 5;

    /// <summary>
    /// Número de elementos por página por defecto
    /// </summary>
    public int DefaultItemsPerPage { get; set; } = 25;

    /// <summary>
    /// Formato de fecha por defecto
    /// </summary>
    public string DefaultDateFormat { get; set; } = "dd/MM/yyyy";

    /// <summary>
    /// Formato de hora por defecto
    /// </summary>
    public string DefaultTimeFormat { get; set; } = "HH:mm";

    /// <summary>
    /// Moneda por defecto
    /// </summary>
    public string DefaultCurrency { get; set; } = "CLP";

    /// <summary>
    /// Configuraciones de color personalizadas
    /// </summary>
    public Dictionary<string, string> CustomColors { get; set; } = new();

    /// <summary>
    /// Configuraciones adicionales de UI
    /// </summary>
    public Dictionary<string, object> AdditionalUISettings { get; set; } = new();
}

/// <summary>
/// Configuración de integración
/// </summary>
public class IntegrationConfigDto
{
    /// <summary>
    /// Integraciones habilitadas
    /// </summary>
    public List<IntegrationProviderDto> EnabledIntegrations { get; set; } = new();

    /// <summary>
    /// Webhooks habilitados
    /// </summary>
    public bool WebhooksEnabled { get; set; } = false;

    /// <summary>
    /// Configuración de webhooks
    /// </summary>
    public WebhookConfigDto? WebhookConfig { get; set; }

    /// <summary>
    /// API externa habilitada
    /// </summary>
    public bool ExternalApiEnabled { get; set; } = true;

    /// <summary>
    /// Versión de API externa
    /// </summary>
    public string ExternalApiVersion { get; set; } = "v1";

    /// <summary>
    /// Documentación de API habilitada
    /// </summary>
    public bool ApiDocumentationEnabled { get; set; } = true;
}

/// <summary>
/// Proveedor de integración
/// </summary>
public class IntegrationProviderDto
{
    /// <summary>
    /// Nombre del proveedor
    /// </summary>
    public string ProviderName { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de integración
    /// </summary>
    public string IntegrationType { get; set; } = string.Empty;

    /// <summary>
    /// Configuración específica del proveedor
    /// </summary>
    public Dictionary<string, object> ProviderConfiguration { get; set; } = new();

    /// <summary>
    /// Indica si está habilitado
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Última sincronización
    /// </summary>
    public DateTime? LastSync { get; set; }

    /// <summary>
    /// Estado de la integración
    /// </summary>
    public string Status { get; set; } = "Active";
}

/// <summary>
/// Configuración de webhooks
/// </summary>
public class WebhookConfigDto
{
    /// <summary>
    /// URL base para webhooks
    /// </summary>
    public string? BaseUrl { get; set; }

    /// <summary>
    /// Secreto para firmar webhooks
    /// </summary>
    public string? Secret { get; set; }

    /// <summary>
    /// Timeout para webhooks (segundos)
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Número máximo de reintentos
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Eventos suscritos
    /// </summary>
    public List<string> SubscribedEvents { get; set; } = new();

    /// <summary>
    /// Headers personalizados
    /// </summary>
    public Dictionary<string, string> CustomHeaders { get; set; } = new();
}