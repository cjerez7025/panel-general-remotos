// ============================================================================
// Archivo: MissingCallRecordDTOs.cs
// Propósito: SOLO los DTOs que faltan para CallRecordService
// Creado: 20/08/2025 - Corregir errores CS0246
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/MissingCallRecordDTOs.cs
// ============================================================================

using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Models.DTOs;

// ============================================================================
// DTOs FALTANTES IDENTIFICADOS EN LOS ERRORES
// ============================================================================

/// <summary>
/// Detalle de llamadas por sponsor (para drill-down)
/// CS0246: CallsDetailBySponsor no se encontró
/// </summary>
public class CallsDetailBySponsor
{
    /// <summary>
    /// ID del sponsor
    /// </summary>
    public int SponsorId { get; set; }

    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Rango de fechas de los datos
    /// </summary>
    public DateRange DateRange { get; set; } = new();

    /// <summary>
    /// Llamadas detalladas por ejecutivo
    /// </summary>
    public List<ExecutiveCallsDetail> ExecutiveDetails { get; set; } = new();

    /// <summary>
    /// Total de llamadas del sponsor
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta total del sponsor
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento
    /// </summary>
    public decimal GoalAchievementPercentage { get; set; }

    /// <summary>
    /// Número de ejecutivos
    /// </summary>
    public int ExecutiveCount { get; set; }

    /// <summary>
    /// Llamadas por fecha
    /// </summary>
    public Dictionary<DateTime, int> CallsByDate { get; set; } = new();

    /// <summary>
    /// Fechas incluidas en el análisis
    /// </summary>
    public List<DateTime> IncludedDates { get; set; } = new();
}

/// <summary>
/// Detalle de llamadas por ejecutivo
/// </summary>
public class ExecutiveCallsDetail
{
    /// <summary>
    /// ID del ejecutivo
    /// </summary>
    public int ExecutiveId { get; set; }

    /// <summary>
    /// Nombre completo del ejecutivo
    /// </summary>
    public string ExecutiveName { get; set; } = string.Empty;

    /// <summary>
    /// Nombre corto/identificador
    /// </summary>
    public string ShortName { get; set; } = string.Empty;

    /// <summary>
    /// Indica si el ejecutivo está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Llamadas por fecha
    /// </summary>
    public Dictionary<DateTime, int> CallsByDate { get; set; } = new();

    /// <summary>
    /// Total de llamadas del ejecutivo
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta diaria del ejecutivo
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Meta total para el período
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento
    /// </summary>
    public decimal GoalAchievementPercentage { get; set; }

    /// <summary>
    /// Nivel de rendimiento del ejecutivo
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Mejor día (más llamadas)
    /// </summary>
    public DateTime? BestDay { get; set; }

    /// <summary>
    /// Peor día (menos llamadas)
    /// </summary>
    public DateTime? WorstDay { get; set; }

    /// <summary>
    /// Estado de las llamadas del ejecutivo
    /// </summary>
    public CallsStatus Status { get; set; }
}

/// <summary>
/// Resultado de actualización
/// CS0246: UpdateResult no se encontró
/// </summary>
public class UpdateResult
{
    /// <summary>
    /// Indica si la actualización fue exitosa
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Total de registros procesados
    /// </summary>
    public int TotalRecordsProcessed { get; set; }

    /// <summary>
    /// Registros actualizados
    /// </summary>
    public int UpdatedRecords { get; set; }

    /// <summary>
    /// Registros creados
    /// </summary>
    public int CreatedRecords { get; set; }

    /// <summary>
    /// Registros eliminados
    /// </summary>
    public int DeletedRecords { get; set; }

    /// <summary>
    /// Timestamp de la actualización
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Lista de errores
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Lista de warnings
    /// </summary>
    public List<string> Warnings { get; set; } = new();
}

/// <summary>
/// Resultado de validación de datos
/// CS0246: DataValidationResult no se encontró
/// </summary>
public class DataValidationResult
{
    /// <summary>
    /// Indica si los datos son válidos
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Total de registros validados
    /// </summary>
    public int TotalRecordsValidated { get; set; }

    /// <summary>
    /// Errores de validación
    /// </summary>
    public List<string> ValidationErrors { get; set; } = new();

    /// <summary>
    /// Warnings de validación
    /// </summary>
    public List<string> ValidationWarnings { get; set; } = new();

    /// <summary>
    /// Timestamp de validación
    /// </summary>
    public DateTime ValidatedAt { get; set; } = DateTime.UtcNow;
}

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
    public int DayCount => (EndDate.Date - StartDate.Date).Days + 1;

    /// <summary>
    /// Indica si el rango es válido
    /// </summary>
    public bool IsValid => EndDate >= StartDate;
}

/// <summary>
/// Estado de llamadas para reportes
/// </summary>
public enum CallsStatus
{
    /// <summary>
    /// Excelente rendimiento
    /// </summary>
    Excellent,

    /// <summary>
    /// Buen rendimiento
    /// </summary>
    Good,

    /// <summary>
    /// Rendimiento promedio
    /// </summary>
    Average,

    /// <summary>
    /// Rendimiento pobre
    /// </summary>
    Poor,

    /// <summary>
    /// Sin actividad
    /// </summary>
    NoActivity,

    /// <summary>
    /// Estado desconocido
    /// </summary>
    Unknown
}