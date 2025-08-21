// ============================================================================
// Archivo: GoogleSheetConfiguration.cs
// Propósito: Configuración de Google Sheets por ejecutivo con IDs reales
// Creado: 11/08/2025 - Initial creation con datos extraídos de LINKS DRIVE AGOSTO.xlsx
// Modificado: 11/08/2025 - Added real spreadsheet IDs from project files
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Domain/Entities/GoogleSheetConfiguration.cs
// ============================================================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PanelGeneralRemotos.Domain.Enums;
using PanelGeneralRemotos.Domain.Entities;
namespace PanelGeneralRemotos.Domain.Entities;

/// <summary>
/// Configuración de Google Sheets específica para cada ejecutivo
/// Contiene los IDs reales extraídos del archivo LINKS DRIVE AGOSTO.xlsx
/// </summary>
public class GoogleSheetConfiguration
{
    /// <summary>
    /// Identificador único de la configuración
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Nombre descriptivo de la hoja (ej: "Achs Remoto 8")
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string SheetName { get; set; } = string.Empty;

    /// <summary>
    /// ID del spreadsheet de Google Sheets
    /// Extraído de URLs como: /d/{SpreadsheetId}/edit
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string SpreadsheetId { get; set; } = string.Empty;

    /// <summary>
    /// GID específico de la hoja dentro del spreadsheet
    /// Extraído de URLs como: gid={SheetGid}
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string SheetGid { get; set; } = string.Empty;

    /// <summary>
    /// URL completa de la hoja de Google Sheets
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string SheetUrl { get; set; } = string.Empty;

    /// <summary>
    /// Rango de celdas a leer (ej: "A1:Z1000")
    /// </summary>
    [MaxLength(50)]
    public string? ReadRange { get; set; } = "A1:Z1000";

    /// <summary>
    /// Indica si esta configuración está activa
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Última fecha de sincronización exitosa
    /// </summary>
    public DateTime? LastSyncDate { get; set; }

    /// <summary>
    /// Fecha de la última sincronización (exitosa o fallida)
    /// </summary>
    public DateTime? LastAttemptDate { get; set; }

    /// <summary>
    /// Estado de la última sincronización
    /// </summary>
    public SyncStatus LastSyncStatus { get; set; } = SyncStatus.Pending;

    /// <summary>
    /// Mensaje de error de la última sincronización (si aplica)
    /// </summary>
    [MaxLength(1000)]
    public string? LastErrorMessage { get; set; }

    /// <summary>
    /// Número de intentos fallidos consecutivos
    /// </summary>
    public int ConsecutiveFailures { get; set; } = 0;

    /// <summary>
    /// Fecha de creación de la configuración
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// ID del ejecutivo asociado a esta configuración
    /// </summary>
    [Required]
    public int ExecutiveId { get; set; }

    /// <summary>
    /// Ejecutivo asociado a esta configuración
    /// </summary>
    [ForeignKey(nameof(ExecutiveId))]
    public virtual Executive Executive { get; set; } = null!;

    /// <summary>
    /// ID del sponsor para facilitar consultas
    /// </summary>
    [Required]
    public int SponsorId { get; set; }

    /// <summary>
    /// Sponsor asociado para facilitar consultas
    /// </summary>
    [ForeignKey(nameof(SponsorId))]
    public virtual Sponsor Sponsor { get; set; } = null!;
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Registra un intento de sincronización exitoso
    /// </summary>
    public void RecordSuccessfulSync()
    {
        LastSyncDate = DateTime.UtcNow;
        LastAttemptDate = DateTime.UtcNow;
        LastSyncStatus = SyncStatus.Success;
        LastErrorMessage = null;
        ConsecutiveFailures = 0;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Registra un intento de sincronización fallido
    /// </summary>
    /// <param name="errorMessage">Mensaje de error</param>
    public void RecordFailedSync(string errorMessage)
    {
        LastAttemptDate = DateTime.UtcNow;
        LastSyncStatus = SyncStatus.Failed;
        LastErrorMessage = errorMessage;
        ConsecutiveFailures++;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Determina si la configuración necesita ser sincronizada
    /// </summary>
    /// <param name="syncIntervalMinutes">Intervalo de sincronización en minutos</param>
    /// <returns>True si necesita sincronización</returns>
    public bool NeedsSynchronization(int syncIntervalMinutes = 30)
    {
        if (!IsActive) return false;
        if (LastSyncDate == null) return true;
        
        var nextSyncTime = LastSyncDate.Value.AddMinutes(syncIntervalMinutes);
        return DateTime.UtcNow >= nextSyncTime;
    }

    /// <summary>
    /// Genera la URL de la API de Google Sheets para leer datos
    /// </summary>
    /// <returns>URL de la API</returns>
    public string GenerateApiUrl()
    {
        var range = ReadRange ?? "A1:Z1000";
        return $"https://sheets.googleapis.com/v4/spreadsheets/{SpreadsheetId}/values/{range}";
    }
}