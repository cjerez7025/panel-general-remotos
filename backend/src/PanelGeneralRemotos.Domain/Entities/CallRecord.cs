// ============================================================================
// Archivo: CallRecord.cs
// Propósito: Entidad para datos dinámicos de llamadas leídos desde Google Sheets
// Creado: 11/08/2025 - Initial creation for real-time data from Google Sheets
// Modificado: 11/08/2025 - Added real-time sync capabilities and cache management
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Domain/Entities/CallRecord.cs
// ============================================================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PanelGeneralRemotos.Domain.Enums;
using PanelGeneralRemotos.Domain.Entities;
namespace PanelGeneralRemotos.Domain.Entities;

/// <summary>
/// Registro de llamadas dinámico leído desde Google Sheets
/// Esta entidad se actualiza en tiempo real cuando el usuario presiona "Actualizar"
/// NO es información estática - se sincroniza con Google Sheets en vivo
/// </summary>
public class CallRecord
{
    /// <summary>
    /// Identificador único del registro de llamada
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de las llamadas (extraída de Google Sheets)
    /// </summary>
    [Required]
    public DateTime CallDate { get; set; }

    /// <summary>
    /// Número total de llamadas realizadas en esta fecha
    /// DATO DINÁMICO - Se actualiza desde Google Sheets
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta de llamadas para esta fecha específica
    /// </summary>
    public int CallGoal { get; set; } = 60;

    /// <summary>
    /// Porcentaje de cumplimiento de la meta diaria
    /// Calculado dinámicamente: (TotalCalls / CallGoal) * 100
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal GoalPercentage { get; set; }

    /// <summary>
    /// Hora de la última actualización desde Google Sheets
    /// Permite saber cuándo fue la última sincronización
    /// </summary>
    public DateTime LastUpdatedFromSheet { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indica si este registro fue actualizado en la última sincronización
    /// </summary>
    public bool UpdatedInLastSync { get; set; } = true;

    /// <summary>
    /// Hash de los datos para detectar cambios
    /// Permite identificar si los datos cambiaron en Google Sheets
    /// </summary>
    [MaxLength(64)]
    public string? DataHash { get; set; }

    /// <summary>
    /// Fecha de creación del registro
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha de última actualización en la base de datos
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// ID del ejecutivo que realizó las llamadas
    /// </summary>
    [Required]
    public int ExecutiveId { get; set; }

    /// <summary>
    /// Ejecutivo que realizó las llamadas
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

    /// <summary>
    /// Fila origen en Google Sheets (para debugging y auditoria)
    /// </summary>
    public int? SourceSheetRow { get; set; }

    /// <summary>
    /// Nombre de la hoja de Google Sheets de origen
    /// </summary>
    [MaxLength(200)]
    public string? SourceSheetName { get; set; }

    /// <summary>
    /// Actualiza los datos de llamadas desde Google Sheets
    /// </summary>
    /// <param name="newCallCount">Nuevo número de llamadas desde la hoja</param>
    /// <param name="sheetRow">Fila de origen en la hoja</param>
    /// <param name="sheetName">Nombre de la hoja</param>
    public void UpdateFromSheet(int newCallCount, int? sheetRow = null, string? sheetName = null)
    {
        var hasChanged = TotalCalls != newCallCount;
        
        TotalCalls = newCallCount;
        GoalPercentage = CallGoal > 0 ? Math.Round((decimal)TotalCalls / CallGoal * 100, 2) : 0;
        LastUpdatedFromSheet = DateTime.UtcNow;
        UpdatedInLastSync = true;
        SourceSheetRow = sheetRow;
        SourceSheetName = sheetName;
        
        if (hasChanged)
        {
            UpdatedAt = DateTime.UtcNow;
            DataHash = GenerateDataHash();
        }
    }

    /// <summary>
    /// Genera un hash de los datos para detectar cambios
    /// </summary>
    /// <returns>Hash MD5 de los datos principales</returns>
    private string GenerateDataHash()
    {
        var dataString = $"{CallDate:yyyy-MM-dd}_{ExecutiveId}_{TotalCalls}_{CallGoal}";
        using var md5 = System.Security.Cryptography.MD5.Create();
        var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataString));
        return Convert.ToHexString(hash);
    }

    /// <summary>
    /// Determina el nivel de rendimiento basado en el porcentaje de cumplimiento
    /// </summary>
    /// <returns>Nivel de rendimiento calculado dinámicamente</returns>
    public PerformanceLevel GetPerformanceLevel()
    {
        return GoalPercentage switch
        {
            >= 90 => PerformanceLevel.Excellent,
            >= 70 => PerformanceLevel.Good,
            >= 50 => PerformanceLevel.Average,
            _ => PerformanceLevel.Poor
        };
    }

    /// <summary>
    /// Indica si el registro necesita ser actualizado (más de X minutos desde la última sync)
    /// </summary>
    /// <param name="maxAgeMinutes">Edad máxima en minutos antes de considerar obsoleto</param>
    /// <returns>True si necesita actualización</returns>
    public bool NeedsUpdate(int maxAgeMinutes = 30)
    {
        return DateTime.UtcNow.Subtract(LastUpdatedFromSheet).TotalMinutes > maxAgeMinutes;
    }

    /// <summary>
    /// Marca el registro como no actualizado en la última sincronización
    /// Útil para identificar registros que ya no existen en Google Sheets
    /// </summary>
    public void MarkAsNotUpdatedInSync()
    {
        UpdatedInLastSync = false;
        UpdatedAt = DateTime.UtcNow;
    }
}