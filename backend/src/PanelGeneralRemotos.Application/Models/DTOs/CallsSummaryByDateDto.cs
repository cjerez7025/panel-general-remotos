// ============================================================================
// Archivo: CallsSummaryByDateDto.cs
// Propósito: DTO para resumen de llamadas por fecha - Pestaña "Llamadas por Fecha"
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/CallsSummaryByDateDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para el resumen de llamadas organizadas por fecha
/// Usado en la pestaña "Llamadas por Fecha" del dashboard
/// </summary>
public class CallsSummaryByDateDto
{
    /// <summary>
    /// Rango de fechas del resumen
    /// </summary>
    public DateRange DateRange { get; set; } = new();

    /// <summary>
    /// Lista de sponsors con sus llamadas por fecha
    /// </summary>
    public List<SponsorCallsByDateDto> SponsorData { get; set; } = new();

    /// <summary>
    /// Totales consolidados por fecha
    /// </summary>
    public Dictionary<DateTime, DayTotalsDto> DailyTotals { get; set; } = new();

    /// <summary>
    /// Fechas incluidas en el análisis
    /// </summary>
    public List<DateTime> IncludedDates { get; set; } = new();

    /// <summary>
    /// Estadísticas generales del período
    /// </summary>
    public PeriodStatisticsDto PeriodStatistics { get; set; } = new();

    /// <summary>
    /// Timestamp de cuando se generó este resumen
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indica si hay datos faltantes o incompletos
    /// </summary>
    public bool HasIncompleteData { get; set; }

    /// <summary>
    /// Lista de problemas encontrados en los datos
    /// </summary>
    public List<string> DataIssues { get; set; } = new();

    /// <summary>
    /// Configuración utilizada para generar el resumen
    /// </summary>
    public SummaryConfigurationDto Configuration { get; set; } = new();
}

/// <summary>
/// Llamadas de un sponsor organizadas por fecha
/// </summary>
public class SponsorCallsByDateDto
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
    /// Color hexadecimal del sponsor para la UI
    /// </summary>
    public string? ColorHex { get; set; }

    /// <summary>
    /// Estado del sponsor
    /// </summary>
    public SponsorStatus Status { get; set; }

    /// <summary>
    /// Llamadas por fecha (Fecha -> Número de llamadas)
    /// </summary>
    public Dictionary<DateTime, int> CallsByDate { get; set; } = new();

    /// <summary>
    /// Metas por fecha (Fecha -> Meta del día)
    /// </summary>
    public Dictionary<DateTime, int> GoalsByDate { get; set; } = new();

    /// <summary>
    /// Porcentaje de cumplimiento por fecha (Fecha -> % cumplimiento)
    /// </summary>
    public Dictionary<DateTime, decimal> ComplianceByDate { get; set; } = new();

    /// <summary>
    /// Total de llamadas del sponsor en el período
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta total del sponsor para el período
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento general del sponsor
    /// </summary>
    public decimal OverallCompliance { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Mejor día del sponsor
    /// </summary>
    public DateTime? BestDay { get; set; }

    /// <summary>
    /// Llamadas en el mejor día
    /// </summary>
    public int BestDayCount { get; set; }

    /// <summary>
    /// Peor día del sponsor
    /// </summary>
    public DateTime? WorstDay { get; set; }

    /// <summary>
    /// Llamadas en el peor día
    /// </summary>
    public int WorstDayCount { get; set; }

    /// <summary>
    /// Número de ejecutivos activos del sponsor
    /// </summary>
    public int ActiveExecutives { get; set; }

    /// <summary>
    /// Días con datos disponibles
    /// </summary>
    public int DaysWithData { get; set; }

    /// <summary>
    /// Días donde se cumplió la meta
    /// </summary>
    public int DaysGoalMet { get; set; }

    /// <summary>
    /// Tendencia del sponsor (improving, declining, stable)
    /// </summary>
    public string Trend { get; set; } = string.Empty;

    /// <summary>
    /// Nivel de rendimiento general
    /// </summary>
    public PerformanceLevel PerformanceLevel { get; set; }

    /// <summary>
    /// Última actualización de datos del sponsor
    /// </summary>
    public DateTime? LastDataUpdate { get; set; }

    /// <summary>
    /// Indica si hay problemas con los datos del sponsor
    /// </summary>
    public bool HasDataIssues { get; set; }

    /// <summary>
    /// Lista de problemas específicos del sponsor
    /// </summary>
    public List<string> SponsorIssues { get; set; } = new();
}

/// <summary>
/// Totales de un día específico
/// </summary>
public class DayTotalsDto
{
    /// <summary>
    /// Fecha del día
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Total de llamadas de todos los sponsors
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta total del día (suma de todos los sponsors)
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento del día
    /// </summary>
    public decimal CompliancePercentage { get; set; }

    /// <summary>
    /// Número de sponsors que cumplieron meta
    /// </summary>
    public int SponsorsMetGoal { get; set; }

    /// <summary>
    /// Número total de sponsors activos
    /// </summary>
    public int TotalActiveSponsors { get; set; }

    /// <summary>
    /// Ejecutivos activos del día
    /// </summary>
    public int ActiveExecutives { get; set; }

    /// <summary>
    /// Promedio de llamadas por sponsor
    /// </summary>
    public decimal AverageCallsPerSponsor { get; set; }

    /// <summary>
    /// Promedio de llamadas por ejecutivo
    /// </summary>
    public decimal AverageCallsPerExecutive { get; set; }

    /// <summary>
    /// Día de la semana
    /// </summary>
    public DayOfWeek DayOfWeek { get; set; }

    /// <summary>
    /// Indica si es día laborable
    /// </summary>
    public bool IsWorkingDay { get; set; }

    /// <summary>
    /// Comparación con el día anterior
    /// </summary>
    public decimal ChangeFromPreviousDay { get; set; }

    /// <summary>
    /// Comparación con el mismo día de la semana anterior
    /// </summary>
    public decimal ChangeFromSameDayLastWeek { get; set; }
}

/// <summary>
/// Estadísticas del período analizado
/// </summary>
public class PeriodStatisticsDto
{
    /// <summary>
    /// Fecha de inicio del período
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin del período
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Número total de días en el período
    /// </summary>
    public int TotalDays { get; set; }

    /// <summary>
    /// Días con datos disponibles
    /// </summary>
    public int DaysWithData { get; set; }

    /// <summary>
    /// Total de llamadas en el período
    /// </summary>
    public int TotalCalls { get; set; }

    /// <summary>
    /// Meta total del período
    /// </summary>
    public int TotalGoal { get; set; }

    /// <summary>
    /// Porcentaje de cumplimiento general
    /// </summary>
    public decimal OverallCompliance { get; set; }

    /// <summary>
    /// Promedio de llamadas por día
    /// </summary>
    public decimal AverageCallsPerDay { get; set; }

    /// <summary>
    /// Día con más llamadas
    /// </summary>
    public DateTime? BestDay { get; set; }

    /// <summary>
    /// Llamadas en el mejor día
    /// </summary>
    public int BestDayCount { get; set; }

    /// <summary>
    /// Día con menos llamadas
    /// </summary>
    public DateTime? WorstDay { get; set; }

    /// <summary>
    /// Llamadas en el peor día
    /// </summary>
    public int WorstDayCount { get; set; }

    /// <summary>
    /// Varianza en las llamadas diarias
    /// </summary>
    public decimal DailyVariance { get; set; }

    /// <summary>
    /// Desviación estándar
    /// </summary>
    public decimal StandardDeviation { get; set; }

    /// <summary>
    /// Tendencia general del período
    /// </summary>
    public string OverallTrend { get; set; } = string.Empty;

    /// <summary>
    /// Días donde se cumplió la meta global
    /// </summary>
    public int DaysGlobalGoalMet { get; set; }

    /// <summary>
    /// Porcentaje de días con meta cumplida
    /// </summary>
    public decimal GoalMetDaysPercentage { get; set; }
}

/// <summary>
/// Configuración utilizada para generar el resumen
/// </summary>
public class SummaryConfigurationDto
{
    /// <summary>
    /// Incluir fines de semana en el análisis
    /// </summary>
    public bool IncludeWeekends { get; set; }

    /// <summary>
    /// Incluir días festivos
    /// </summary>
    public bool IncludeHolidays { get; set; }

    /// <summary>
    /// Incluir sponsors inactivos
    /// </summary>
    public bool IncludeInactiveSponsors { get; set; }

    /// <summary>
    /// Umbral mínimo de llamadas para considerar un día válido
    /// </summary>
    public int MinimumCallsThreshold { get; set; }

    /// <summary>
    /// Zona horaria utilizada para los cálculos
    /// </summary>
    public string TimeZone { get; set; } = "America/Santiago";

    /// <summary>
    /// Formato de fecha utilizado
    /// </summary>
    public string DateFormat { get; set; } = "yyyy-MM-dd";

    /// <summary>
    /// Incluir métricas de rendimiento
    /// </summary>
    public bool IncludePerformanceMetrics { get; set; } = true;

    /// <summary>
    /// Incluir comparaciones con períodos anteriores
    /// </summary>
    public bool IncludePeriodComparisons { get; set; } = true;
}