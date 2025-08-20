// ============================================================================
// Archivo: MissingDTOsComplement.cs
// Propósito: SOLO los DTOs que faltan para completar la implementación con datos reales
// Creado: 20/08/2025 - Solo DTOs no existentes en el proyecto
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/MissingDTOsComplement.cs
// ============================================================================

using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Models.DTOs;

// ============================================================================
// DTOS FALTANTES PARA DASHBOARDSERVICE REAL
// ============================================================================

/// <summary>
/// Resultado de operación de refresh del dashboard
/// </summary>
public class RefreshResult
{
    public bool Success { get; set; }
    public DateTime RefreshTimestamp { get; set; }
    public TimeSpan Duration { get; set; }
    public int RecordsUpdated { get; set; }
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Resultado de sincronización simplificado para GoogleSheetsService
/// </summary>
public class SyncResult
{
    public bool Success { get; set; }
    public DateTime SyncTimestamp { get; set; } = DateTime.UtcNow;
    public int TotalRecordsSynced { get; set; }
    public int SheetsProcessed { get; set; }
    public int SuccessfulSheets { get; set; }
    public int FailedSheets { get; set; }
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public TimeSpan Duration { get; set; }
    public bool HasErrors => Errors.Any();
    public bool HasWarnings => Warnings.Any();
}

/// <summary>
/// Estadísticas de sincronización simplificadas
/// </summary>
public class SyncStatistics
{
    public DateTime? LastSuccessfulSync { get; set; }
    public int TotalSheets { get; set; }
    public int SuccessfulSheets { get; set; }
    public int FailedSheets { get; set; }
    public int TotalRecordsSyncedToday { get; set; }
    public TimeSpan AverageSyncDuration { get; set; }
}

/// <summary>
/// Estado de salud de sincronización simplificado
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
    public string ComponentName { get; set; } = string.Empty;
    public SyncHealthStatus Status { get; set; }
    public DateTime LastSyncTime { get; set; }
    public int RecordsSynced { get; set; }
    public string? ErrorMessage { get; set; }
}

// ============================================================================
// ENUMS AUXILIARES PARA CALLRECORDSERVICE
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

/// <summary>
/// Período de tiempo extendido
/// </summary>
public enum TimePeriod
{
    Today,
    Yesterday,
    ThisWeek,
    LastWeek,
    ThisMonth,
    LastMonth,
    Last7Days,
    Last30Days,
    Custom
}

// ============================================================================
// DTOS AUXILIARES PARA ENTITIES
// ============================================================================

/// <summary>
/// Entidad Ejecutivo simplificada (para referencias en cache)
/// </summary>
public class Executive
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int SponsorId { get; set; }
    public virtual Sponsor Sponsor { get; set; } = null!;
}

/// <summary>
/// Entidad Sponsor simplificada (para referencias en cache)
/// </summary>
public class Sponsor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ColorHex { get; set; } = "#6B7280";
    public int MonthlyGoal { get; set; }
    public int DailyGoal { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<Executive> Executives { get; set; } = new List<Executive>();
}

// ============================================================================
// DTOS PARA VALIDACIÓN DE DATOS
// ============================================================================

/// <summary>
/// Resultado de validación de datos simplificado
/// </summary>
public class DataValidationResult
{
    public bool IsValid { get; set; }
    public List<string> ValidationErrors { get; set; } = new();
    public List<string> ValidationWarnings { get; set; } = new();
    public ValidationStatistics Statistics { get; set; } = new();
}

/// <summary>
/// Estadísticas de validación simplificadas
/// </summary>
public class ValidationStatistics
{
    public int TotalRecords { get; set; }
    public int ValidRecords { get; set; }
    public int InvalidRecords { get; set; }
    public int DuplicateRecords { get; set; }
    public int ObsoleteRecords { get; set; }
}

/// <summary>
/// Rango de fechas simplificado
/// </summary>
public class DateRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public int TotalDays => (EndDate - StartDate).Days + 1;
    public bool IsValid => StartDate <= EndDate;
}

// ============================================================================
// ENUMS ADICIONALES PARA SYSTEM ALERTS
// ============================================================================

/// <summary>
/// Tipo de alerta del sistema
/// </summary>
public enum AlertType
{
    SyncError,
    DataStale,
    NoActivity,
    LowPerformance,
    SystemError,
    ConfigurationError,
    NetworkError,
    AuthenticationError
}

/// <summary>
/// Severidad de alerta
/// </summary>
public enum AlertSeverity
{
    Info,
    Warning,
    Error,
    Critical
}