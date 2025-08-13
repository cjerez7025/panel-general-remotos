// ============================================================================
// Archivo: SyncStatusDto.cs
// Propósito: DTO para estado de sincronización con Google Sheets
// Creado: 11/08/2025 - ARCHIVO FALTANTE - Fix compilation errors
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/SyncStatusDto.cs
// ============================================================================

using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para estado de sincronización con Google Sheets
/// </summary>
public class SyncStatusDto
{
    /// <summary>
    /// Sponsor relacionado
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// ID de la hoja de Google Sheets
    /// </summary>
    public string GoogleSheetId { get; set; } = string.Empty;

    /// <summary>
    /// Nombre de la hoja
    /// </summary>
    public string SheetName { get; set; } = string.Empty;

    /// <summary>
    /// Estado de la sincronización (usa el enum del Domain)
    /// </summary>
    public SyncStatus Status { get; set; }

    /// <summary>
    /// Última sincronización exitosa
    /// </summary>
    public DateTime? LastSuccessfulSync { get; set; }

    /// <summary>
    /// Último intento de sincronización
    /// </summary>
    public DateTime? LastSyncAttempt { get; set; }

    /// <summary>
    /// Próxima sincronización programada
    /// </summary>
    public DateTime? NextScheduledSync { get; set; }

    /// <summary>
    /// Mensaje de estado
    /// </summary>
    public string StatusMessage { get; set; } = string.Empty;

    /// <summary>
    /// Errores de sincronización
    /// </summary>
    public List<string> SyncErrors { get; set; } = new();

    /// <summary>
    /// Warnings de sincronización
    /// </summary>
    public List<string> SyncWarnings { get; set; } = new();

    /// <summary>
    /// Número de registros sincronizados en la última vez
    /// </summary>
    public int RecordsSynced { get; set; }

    /// <summary>
    /// Duración de la última sincronización
    /// </summary>
    public TimeSpan? LastSyncDuration { get; set; }

    /// <summary>
    /// Indica si la sincronización automática está habilitada
    /// </summary>
    public bool AutoSyncEnabled { get; set; }

    /// <summary>
    /// Intervalo de sincronización automática en minutos
    /// </summary>
    public int AutoSyncIntervalMinutes { get; set; }

    /// <summary>
    /// Número de intentos fallidos consecutivos
    /// </summary>
    public int ConsecutiveFailures { get; set; }

    /// <summary>
    /// Salud general de la sincronización (0-100)
    /// </summary>
    public decimal HealthScore { get; set; }

    /// <summary>
    /// Tiempo promedio de sincronización
    /// </summary>
    public TimeSpan? AverageSyncTime { get; set; }

    /// <summary>
    /// Tasa de éxito de sincronización (%)
    /// </summary>
    public decimal SuccessRate { get; set; }

    /// <summary>
/// Rango de fechas
/// </summary>
public class DateRange
{
    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Número de días en el rango
    /// </summary>
    public int TotalDays => (EndDate - StartDate).Days + 1;

    /// <summary>
    /// Indica si incluye el día actual
    /// </summary>
    public bool IncludesToday => EndDate.Date >= DateTime.Today;

    /// <summary>
    /// Descripción del rango
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
}