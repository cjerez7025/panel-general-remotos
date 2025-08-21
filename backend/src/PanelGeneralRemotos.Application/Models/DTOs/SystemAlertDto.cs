// ============================================================================
// Archivo: SystemAlertDto.cs
// Propósito: DTO para alertas y warnings del sistema
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/SystemAlertDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para alertas del sistema mostradas en la sección de warnings del dashboard
/// </summary>
public class SystemAlertDto
{
    /// <summary>
    /// ID único de la alerta
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tipo de alerta
    /// </summary>
    public AlertType Type { get; set; }

    /// <summary>
    /// Nivel de severidad
    /// </summary>
    public AlertSeverity Severity { get; set; }

    /// <summary>
    /// Título de la alerta
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Mensaje descriptivo de la alerta
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Detalles adicionales de la alerta
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Sponsor relacionado con la alerta (si aplica)
    /// </summary>
    public string? RelatedSponsor { get; set; }

    /// <summary>
    /// Ejecutivo relacionado con la alerta (si aplica)
    /// </summary>
    public string? RelatedExecutive { get; set; }

    /// <summary>
    /// Timestamp cuando se generó la alerta
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp cuando se actualizó por última vez
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indica si la alerta está activa
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Indica si la alerta fue reconocida por un usuario
    /// </summary>
    public bool IsAcknowledged { get; set; } = false;

    /// <summary>
    /// Usuario que reconoció la alerta
    /// </summary>
    public string? AcknowledgedBy { get; set; }

    /// <summary>
    /// Timestamp del reconocimiento
    /// </summary>
    public DateTime? AcknowledgedAt { get; set; }

    /// <summary>
    /// Icono a mostrar en la UI
    /// </summary>
    public string IconName { get; set; } = "alert-triangle";

    /// <summary>
    /// Color hexadecimal para la alerta
    /// </summary>
    public string ColorHex { get; set; } = "#FFA500";

    /// <summary>
    /// Indica si la alerta debe mostrarse como toast notification
    /// </summary>
    public bool ShowAsToast { get; set; } = false;

    /// <summary>
    /// Duración en segundos para el toast (si aplica)
    /// </summary>
    public int? ToastDurationSeconds { get; set; }

    /// <summary>
    /// Acciones disponibles para esta alerta
    /// </summary>
    public List<AlertActionDto> AvailableActions { get; set; } = new();

    /// <summary>
    /// Datos adicionales relacionados con la alerta
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();

    /// <summary>
    /// Indica si la alerta se resuelve automáticamente
    /// </summary>
    public bool AutoResolve { get; set; } = true;

    /// <summary>
    /// Tiempo en minutos después del cual la alerta expira
    /// </summary>
    public int? ExpiresInMinutes { get; set; }
}

/// <summary>
/// Tipo de alerta del sistema
/// </summary>
public enum AlertType
{
    /// <summary>
    /// Problema de sincronización con Google Sheets
    /// </summary>
    SyncError,

    /// <summary>
    /// Problema de estructura de datos
    /// </summary>
    DataStructure,

    /// <summary>
    /// Datos incompletos o faltantes
    /// </summary>
    IncompleteData,

    /// <summary>
    /// Problemas de conectividad
    /// </summary>
    Connectivity,

    /// <summary>
    /// Rendimiento bajo de sponsor/ejecutivo
    /// </summary>
    Performance,

    /// <summary>
    /// Meta no cumplida
    /// </summary>
    GoalNotMet,

    /// <summary>
    /// Datos obsoletos
    /// </summary>
    StaleData,

    /// <summary>
    /// Error del sistema
    /// </summary>
    SystemError,

    /// <summary>
    /// Configuración incorrecta
    /// </summary>
    Configuration,

    /// <summary>
    /// Información general
    /// </summary>
    Information
}

/// <summary>
/// Nivel de severidad de la alerta
/// </summary>
public enum AlertSeverity
{
    /// <summary>
    /// Información - No requiere acción inmediata
    /// </summary>
    Info,

    /// <summary>
    /// Advertencia - Requiere atención
    /// </summary>
    Warning,

    /// <summary>
    /// Error - Requiere acción inmediata
    /// </summary>
    Error,

    /// <summary>
    /// Crítico - Sistema comprometido
    /// </summary>
    Critical
}

/// <summary>
/// Acción disponible para una alerta
/// </summary>
public class AlertActionDto
{
    /// <summary>
    /// ID único de la acción
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Texto a mostrar en el botón/enlace
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de lo que hace la acción
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Tipo de acción
    /// </summary>
    public AlertActionType ActionType { get; set; }

    /// <summary>
    /// URL o endpoint a llamar
    /// </summary>
    public string? ActionUrl { get; set; }

    /// <summary>
    /// Método HTTP (GET, POST, etc.)
    /// </summary>
    public string HttpMethod { get; set; } = "POST";

    /// <summary>
    /// Parámetros adicionales para la acción
    /// </summary>
    public Dictionary<string, object>? Parameters { get; set; }

    /// <summary>
    /// Indica si la acción requiere confirmación
    /// </summary>
    public bool RequiresConfirmation { get; set; } = false;

    /// <summary>
    /// Mensaje de confirmación
    /// </summary>
    public string? ConfirmationMessage { get; set; }

    /// <summary>
    /// Icono para la acción
    /// </summary>
    public string? IconName { get; set; }

    /// <summary>
    /// Estilo CSS para el botón
    /// </summary>
    public string CssClass { get; set; } = "btn-secondary";
}

/// <summary>
/// Tipo de acción de alerta
/// </summary>
public enum AlertActionType
{
    /// <summary>
    /// Reconocer la alerta
    /// </summary>
    Acknowledge,

    /// <summary>
    /// Descartar la alerta
    /// </summary>
    Dismiss,

    /// <summary>
    /// Reintentar operación
    /// </summary>
    Retry,

    /// <summary>
    /// Forzar sincronización
    /// </summary>
    ForceSync,

    /// <summary>
    /// Ver detalles
    /// </summary>
    ViewDetails,

    /// <summary>
    /// Ir a configuración
    /// </summary>
    GoToSettings,

    /// <summary>
    /// Navegar a sponsor/ejecutivo
    /// </summary>
    Navigate,

    /// <summary>
    /// Acción personalizada
    /// </summary>
    Custom
}

/// <summary>
/// Resumen de alertas del sistema
/// </summary>
public class AlertsSummaryDto
{
    /// <summary>
    /// Total de alertas activas
    /// </summary>
    public int TotalAlerts { get; set; }

    /// <summary>
    /// Alertas por severidad
    /// </summary>
    public Dictionary<AlertSeverity, int> AlertsBySeverity { get; set; } = new();

    /// <summary>
    /// Alertas por tipo
    /// </summary>
    public Dictionary<AlertType, int> AlertsByType { get; set; } = new();

    /// <summary>
    /// Indica si hay alertas críticas
    /// </summary>
    public bool HasCriticalAlerts { get; set; }

    /// <summary>
    /// Número de alertas no reconocidas
    /// </summary>
    public int UnacknowledgedAlerts { get; set; }

    /// <summary>
    /// Última alerta generada
    /// </summary>
    public DateTime? LastAlertTime { get; set; }
    /// <summary>
    /// Timestamp (alias para CreatedAt para compatibilidad)
    /// </summary>
    public DateTime Timestamp 
    { 
        get => CreatedAt; 
        set => CreatedAt = value; 
    }

    /// <summary>
    /// Indica si la alerta requiere acción del usuario
    /// </summary>
    public bool IsActionable { get; set; } = false;

    /// <summary>
    /// Texto de la acción sugerida
    /// </summary>
    public string? ActionText { get; set; }

    /// <summary>
    /// Prioridad de la alerta
    /// </summary>
    public AlertPriority Priority { get; set; } = AlertPriority.Medium;
    public DateTime CreatedAt { get; private set; }
}