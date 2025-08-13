// ============================================================================
// Archivo: PerformanceMetric.cs
// Propósito: Métricas de rendimiento dinámicas actualizadas desde Google Sheets
// Creado: 11/08/2025 - Initial creation for real-time performance tracking
// Modificado: 11/08/2025 - Added dynamic calculation and refresh capabilities
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Domain/Entities/PerformanceMetric.cs
// ============================================================================

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PanelGeneralRemotos.Domain.Enums;
using PanelGeneralRemotos.Domain.Entities;
namespace PanelGeneralRemotos.Domain.Entities;

/// <summary>
/// Métricas de rendimiento dinámicas calculadas desde datos de Google Sheets
/// Se actualiza en tiempo real cuando el usuario presiona "Actualizar" en el dashboard
/// Contiene KPIs como: Gestionado, Meta, % Avance, Contactados, Interesados, Cerrados
/// </summary>
public class PerformanceMetric
{
    /// <summary>
    /// Identificador único de la métrica
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de la métrica
    /// </summary>
    [Required]
    public DateTime MetricDate { get; set; }

    /// <summary>
    /// Tipo de métrica (Calls, Managed, Contacted, etc.)
    /// </summary>
    [Required]
    public MetricType MetricType { get; set; }

    /// <summary>
    /// Período de tiempo de la métrica (Daily, Weekly, Monthly)
    /// </summary>
    [Required]
    public TimePeriod TimePeriod { get; set; }

    // ============================================================================
    // DATOS DINÁMICOS LEÍDOS DESDE GOOGLE SHEETS
    // ============================================================================

    /// <summary>
    /// Total gestionado (contactos procesados)
    /// DATO DINÁMICO - Actualizado desde Google Sheets
    /// </summary>
    public int TotalManaged { get; set; }

    /// <summary>
    /// Meta establecida para el período
    /// DATO DINÁMICO - Puede cambiar en Google Sheets
    /// </summary>
    public int Goal { get; set; }

    /// <summary>
    /// Porcentaje de avance hacia la meta
    /// CALCULADO DINÁMICAMENTE: (TotalManaged / Goal) * 100
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal GoalPercentage { get; set; }

    /// <summary>
    /// Total de contactos efectivamente contactados
    /// DATO DINÁMICO - Actualizado desde Google Sheets
    /// </summary>
    public int TotalContacted { get; set; }

    /// <summary>
    /// Porcentaje de contactados sobre gestionados
    /// CALCULADO DINÁMICAMENTE: (TotalContacted / TotalManaged) * 100
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal ContactedPercentage { get; set; }

    /// <summary>
    /// Total de contactos que mostraron interés
    /// DATO DINÁMICO - Actualizado desde Google Sheets
    /// </summary>
    public int TotalInterested { get; set; }

    /// <summary>
    /// Porcentaje de interesados sobre contactados
    /// CALCULADO DINÁMICAMENTE: (TotalInterested / TotalContacted) * 100
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal InterestedPercentage { get; set; }

    /// <summary>
    /// Total de contactos cerrados/convertidos
    /// DATO DINÁMICO - Actualizado desde Google Sheets
    /// </summary>
    public int TotalClosed { get; set; }

    /// <summary>
    /// Porcentaje de rendimiento global (cerrados sobre gestionados)
    /// CALCULADO DINÁMICAMENTE: (TotalClosed / TotalManaged) * 100
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal OverallPerformancePercentage { get; set; }

    // ============================================================================
    // METADATOS DE SINCRONIZACIÓN
    // ============================================================================

    /// <summary>
    /// Última vez que se actualizó desde Google Sheets
    /// </summary>
    public DateTime LastSyncFromSheet { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indica si fue actualizado en la última sincronización
    /// </summary>
    public bool UpdatedInLastSync { get; set; } = true;

    /// <summary>
    /// Hash de datos para detectar cambios
    /// </summary>
    [MaxLength(64)]
    public string? DataHash { get; set; }

    /// <summary>
    /// Nivel de rendimiento calculado automáticamente
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Estado de procesamiento de los datos
    /// </summary>
    public ProcessingStatus ProcessingStatus { get; set; } = ProcessingStatus.Processed;

    /// <summary>
    /// Fecha de creación del registro
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    // ============================================================================
    // RELACIONES
    // ============================================================================

    /// <summary>
    /// ID del ejecutivo asociado
    /// </summary>
    [Required]
    public int ExecutiveId { get; set; }

    /// <summary>
    /// Ejecutivo asociado
    /// </summary>
    [ForeignKey(nameof(ExecutiveId))]
    public virtual Executive Executive { get; set; } = null!;

    /// <summary>
    /// ID del sponsor para facilitar consultas
    /// </summary>
    [Required]
    public int SponsorId { get; set; }

    /// <summary>
    /// Sponsor asociado
    /// </summary>
    [ForeignKey(nameof(SponsorId))]
    public virtual Sponsor Sponsor { get; set; } = null!;

    // ============================================================================
    // MÉTODOS DINÁMICOS PARA ACTUALIZACIÓN DESDE GOOGLE SHEETS
    // ============================================================================

    /// <summary>
    /// Actualiza todas las métricas desde datos de Google Sheets
    /// </summary>
    /// <param name="managed">Número gestionado</param>
    /// <param name="goal">Meta del período</param>
    /// <param name="contacted">Número contactado</param>
    /// <param name="interested">Número de interesados</param>
    /// <param name="closed">Número cerrado</param>
    public void UpdateFromSheetData(int managed, int goal, int contacted, int interested, int closed)
    {
        var oldHash = DataHash;

        TotalManaged = managed;
        Goal = goal;
        TotalContacted = contacted;
        TotalInterested = interested;
        TotalClosed = closed;

        // Calcular porcentajes dinámicamente
        RecalculatePercentages();
        
        // Determinar nivel de rendimiento
        PerformanceLevel = DeterminePerformanceLevel();
        
        // Actualizar metadatos
        LastSyncFromSheet = DateTime.UtcNow;
        UpdatedInLastSync = true;
        ProcessingStatus = ProcessingStatus.Processed;
        
        var newHash = GenerateDataHash();
        if (oldHash != newHash)
        {
            DataHash = newHash;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    /// <summary>
    /// Recalcula todos los porcentajes basado en los valores actuales
    /// </summary>
    public void RecalculatePercentages()
    {
        // Porcentaje de avance hacia la meta
        GoalPercentage = Goal > 0 ? Math.Round((decimal)TotalManaged / Goal * 100, 2) : 0;

        // Porcentaje de contactados
        ContactedPercentage = TotalManaged > 0 ? Math.Round((decimal)TotalContacted / TotalManaged * 100, 2) : 0;

        // Porcentaje de interesados
        InterestedPercentage = TotalContacted > 0 ? Math.Round((decimal)TotalInterested / TotalContacted * 100, 2) : 0;

        // Rendimiento global (cerrados sobre gestionados)
        OverallPerformancePercentage = TotalManaged > 0 ? Math.Round((decimal)TotalClosed / TotalManaged * 100, 2) : 0;
    }

    /// <summary>
    /// Determina el nivel de rendimiento basado en métricas globales
    /// </summary>
    /// <returns>Nivel de rendimiento</returns>
    public PerformanceLevel DeterminePerformanceLevel()
    {
        // Usar una combinación de métricas para determinar el nivel
        var avgPerformance = (GoalPercentage + ContactedPercentage + OverallPerformancePercentage) / 3;

        return avgPerformance switch
        {
            >= 90 => PerformanceLevel.Excellent,
            >= 70 => PerformanceLevel.Good,
            >= 50 => PerformanceLevel.Average,
            _ => PerformanceLevel.Poor
        };
    }

    /// <summary>
    /// Genera hash de los datos para detectar cambios
    /// </summary>
    /// <returns>Hash MD5 de los datos principales</returns>
    private string GenerateDataHash()
    {
        var dataString = $"{MetricDate:yyyy-MM-dd}_{ExecutiveId}_{TotalManaged}_{Goal}_{TotalContacted}_{TotalInterested}_{TotalClosed}";
        using var md5 = System.Security.Cryptography.MD5.Create();
        var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataString));
        return Convert.ToHexString(hash);
    }

    /// <summary>
    /// Indica si la métrica necesita actualización
    /// </summary>
    /// <param name="maxAgeMinutes">Edad máxima en minutos</param>
    /// <returns>True si necesita actualización</returns>
    public bool NeedsRefresh(int maxAgeMinutes = 30)
    {
        return DateTime.UtcNow.Subtract(LastSyncFromSheet).TotalMinutes > maxAgeMinutes;
    }

    /// <summary>
    /// Marca la métrica como no actualizada en la última sincronización
    /// </summary>
    public void MarkAsStale()
    {
        UpdatedInLastSync = false;
        ProcessingStatus = ProcessingStatus.Invalidated;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Crea una métrica vacía para inicialización
    /// </summary>
    /// <param name="executiveId">ID del ejecutivo</param>
    /// <param name="sponsorId">ID del sponsor</param>
    /// <param name="date">Fecha de la métrica</param>
    /// <param name="type">Tipo de métrica</param>
    /// <param name="period">Período de tiempo</param>
    /// <returns>Nueva métrica inicializada</returns>
    public static PerformanceMetric CreateEmpty(int executiveId, int sponsorId, DateTime date, MetricType type, TimePeriod period)
    {
        return new PerformanceMetric
        {
            ExecutiveId = executiveId,
            SponsorId = sponsorId,
            MetricDate = date,
            MetricType = type,
            TimePeriod = period,
            TotalManaged = 0,
            Goal = 0,
            TotalContacted = 0,
            TotalInterested = 0,
            TotalClosed = 0,
            PerformanceLevel = PerformanceLevel.Poor,
            ProcessingStatus = ProcessingStatus.Unprocessed
        };
    }
}