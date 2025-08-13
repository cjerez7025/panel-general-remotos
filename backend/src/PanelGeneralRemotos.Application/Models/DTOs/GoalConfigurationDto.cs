// ============================================================================
// Archivo: GoalConfigurationDto.cs
// Propósito: DTO para configuración de metas del sistema
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/GoalConfigurationDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para configuración de metas del sistema
/// </summary>
public class GoalConfigurationDto
{
    /// <summary>
    /// ID de la configuración
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre de la configuración
    /// </summary>
    public string ConfigurationName { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la configuración
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Sponsor relacionado (opcional, si es null aplica a todos)
    /// </summary>
    public string? SponsorName { get; set; }

    /// <summary>
    /// Período de la meta (daily, weekly, monthly, quarterly, yearly)
    /// </summary>
    public string Period { get; set; } = "daily";

    /// <summary>
    /// Fecha de inicio de vigencia
    /// </summary>
    public DateTime EffectiveFrom { get; set; }

    /// <summary>
    /// Fecha de fin de vigencia
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

    /// <summary>
    /// Indica si la configuración está activa
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Indica si es la configuración por defecto
    /// </summary>
    public bool IsDefault { get; set; } = false;

    /// <summary>
    /// Configuraciones de metas por sponsor
    /// </summary>
    public List<SponsorGoalConfigDto> SponsorGoals { get; set; } = new();

    /// <summary>
    /// Configuraciones globales de metas
    /// </summary>
    public GlobalGoalConfigDto GlobalGoals { get; set; } = new();

    /// <summary>
    /// Configuraciones de escalamiento de metas
    /// </summary>
    public GoalEscalationConfigDto EscalationConfig { get; set; } = new();

    /// <summary>
    /// Configuraciones de ajuste automático
    /// </summary>
    public AutoAdjustmentConfigDto? AutoAdjustmentConfig { get; set; }

    /// <summary>
    /// Usuario que creó la configuración
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Usuario que modificó por última vez
    /// </summary>
    public string? LastModifiedBy { get; set; }

    /// <summary>
    /// Fecha de última modificación
    /// </summary>
    public DateTime? LastModifiedDate { get; set; }

    /// <summary>
    /// Versión de la configuración
    /// </summary>
    public int Version { get; set; } = 1;

    /// <summary>
    /// Comentarios adicionales
    /// </summary>
    public string? Comments { get; set; }

    /// <summary>
    /// Configuraciones adicionales personalizadas
    /// </summary>
    public Dictionary<string, object> CustomSettings { get; set; } = new();
}

/// <summary>
/// Configuración de metas específica por sponsor
/// </summary>
public class SponsorGoalConfigDto
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
    /// Meta diaria del sponsor
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Meta semanal del sponsor
    /// </summary>
    public int WeeklyGoal { get; set; }

    /// <summary>
    /// Meta mensual del sponsor
    /// </summary>
    public int MonthlyGoal { get; set; }

    /// <summary>
    /// Meta trimestral del sponsor
    /// </summary>
    public int QuarterlyGoal { get; set; }

    /// <summary>
    /// Meta anual del sponsor
    /// </summary>
    public int YearlyGoal { get; set; }

    /// <summary>
    /// Configuración por días de la semana
    /// </summary>
    public Dictionary<DayOfWeek, int> DailyGoalsByDayOfWeek { get; set; } = new();

    /// <summary>
    /// Configuración por meses del año
    /// </summary>
    public Dictionary<int, int> MonthlyGoalsByMonth { get; set; } = new();

    /// <summary>
    /// Metas específicas por ejecutivo
    /// </summary>
    public List<ExecutiveGoalDto> ExecutiveGoals { get; set; } = new();

    /// <summary>
    /// Configuración de flexibilidad de metas
    /// </summary>
    public GoalFlexibilityConfigDto FlexibilityConfig { get; set; } = new();

    /// <summary>
    /// Configuración de incentivos
    /// </summary>
    public IncentiveConfigDto? IncentiveConfig { get; set; }

    /// <summary>
    /// Indica si el sponsor participa en metas grupales
    /// </summary>
    public bool ParticipatesInGroupGoals { get; set; } = true;

    /// <summary>
    /// Peso del sponsor en metas globales (0-100%)
    /// </summary>
    public decimal WeightInGlobalGoals { get; set; } = 100;

    /// <summary>
    /// Configuraciones especiales por fechas
    /// </summary>
    public List<SpecialDateGoalConfigDto> SpecialDateConfigs { get; set; } = new();

    /// <summary>
    /// Último cálculo de metas
    /// </summary>
    public DateTime? LastGoalCalculation { get; set; }

    /// <summary>
    /// Próximo ajuste programado
    /// </summary>
    public DateTime? NextScheduledAdjustment { get; set; }
}

/// <summary>
/// Meta específica de ejecutivo
/// </summary>
public class ExecutiveGoalDto
{
    /// <summary>
    /// ID del ejecutivo
    /// </summary>
    public int ExecutiveId { get; set; }

    /// <summary>
    /// Nombre del ejecutivo
    /// </summary>
    public string ExecutiveName { get; set; } = string.Empty;

    /// <summary>
    /// Meta diaria individual
    /// </summary>
    public int DailyGoal { get; set; }

    /// <summary>
    /// Meta semanal individual
    /// </summary>
    public int WeeklyGoal { get; set; }

    /// <summary>
    /// Meta mensual individual
    /// </summary>
    public int MonthlyGoal { get; set; }

    /// <summary>
    /// Meta trimestral individual
    /// </summary>
    public int QuarterlyGoal { get; set; }

    /// <summary>
    /// Meta anual individual
    /// </summary>
    public int YearlyGoal { get; set; }

    /// <summary>
    /// Indica si el ejecutivo está activo
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Fecha de inicio de vigencia para este ejecutivo
    /// </summary>
    public DateTime EffectiveFrom { get; set; }

    /// <summary>
    /// Fecha de fin de vigencia para este ejecutivo
    /// </summary>
    public DateTime? EffectiveTo { get; set; }

    /// <summary>
    /// Horario de trabajo del ejecutivo
    /// </summary>
    public WorkScheduleDto? WorkSchedule { get; set; }

    /// <summary>
    /// Factor de ajuste individual (multiplicador de meta base)
    /// </summary>
    public decimal AdjustmentFactor { get; set; } = 1.0m;

    /// <summary>
    /// Nivel de experiencia del ejecutivo
    /// </summary>
    public ExperienceLevel ExperienceLevel { get; set; } = ExperienceLevel.Intermediate;

    /// <summary>
    /// Configuración de rampa de metas (para nuevos ejecutivos)
    /// </summary>
    public GoalRampConfigDto? RampConfig { get; set; }

    /// <summary>
    /// Metas especiales por fechas específicas
    /// </summary>
    public List<SpecialDateGoalConfigDto> SpecialGoals { get; set; } = new();

    /// <summary>
    /// Configuraciones adicionales del ejecutivo
    /// </summary>
    public Dictionary<string, object> AdditionalSettings { get; set; } = new();
}

/// <summary>
/// Nivel de experiencia del ejecutivo
/// </summary>
public enum ExperienceLevel
{
    /// <summary>
    /// Principiante (0-3 meses)
    /// </summary>
    Beginner,

    /// <summary>
    /// Junior (3-6 meses)
    /// </summary>
    Junior,

    /// <summary>
    /// Intermedio (6-12 meses)
    /// </summary>
    Intermediate,

    /// <summary>
    /// Senior (12+ meses)
    /// </summary>
    Senior,

    /// <summary>
    /// Experto (24+ meses)
    /// </summary>
    Expert
}

/// <summary>
/// Configuración de rampa de metas para nuevos ejecutivos
/// </summary>
public class GoalRampConfigDto
{
    /// <summary>
    /// Duración de la rampa en días
    /// </summary>
    public int RampDurationDays { get; set; } = 30;

    /// <summary>
    /// Meta inicial (% de la meta final)
    /// </summary>
    public decimal InitialGoalPercentage { get; set; } = 50;

    /// <summary>
    /// Incremento semanal (% de la meta final)
    /// </summary>
    public decimal WeeklyIncrementPercentage { get; set; } = 12.5m;

    /// <summary>
    /// Meta final (100%)
    /// </summary>
    public decimal FinalGoalPercentage { get; set; } = 100;

    /// <summary>
    /// Tipo de rampa (linear, exponential, custom)
    /// </summary>
    public string RampType { get; set; } = "linear";

    /// <summary>
    /// Puntos personalizados de rampa
    /// </summary>
    public List<RampPointDto> CustomRampPoints { get; set; } = new();
}

/// <summary>
/// Punto específico en la rampa de metas
/// </summary>
public class RampPointDto
{
    /// <summary>
    /// Día en la rampa
    /// </summary>
    public int Day { get; set; }

    /// <summary>
    /// Porcentaje de meta en ese día
    /// </summary>
    public decimal GoalPercentage { get; set; }

    /// <summary>
    /// Descripción del hito
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Configuración global de metas
/// </summary>
public class GlobalGoalConfigDto
{
    /// <summary>
    /// Meta diaria global (suma de todos los sponsors)
    /// </summary>
    public int GlobalDailyGoal { get; set; }

    /// <summary>
    /// Meta semanal global
    /// </summary>
    public int GlobalWeeklyGoal { get; set; }

    /// <summary>
    /// Meta mensual global
    /// </summary>
    public int GlobalMonthlyGoal { get; set; }

    /// <summary>
    /// Meta trimestral global
    /// </summary>
    public int GlobalQuarterlyGoal { get; set; }

    /// <summary>
    /// Meta anual global
    /// </summary>
    public int GlobalYearlyGoal { get; set; }

    /// <summary>
    /// Modo de cálculo de metas globales
    /// </summary>
    public GlobalGoalCalculationMode CalculationMode { get; set; } = GlobalGoalCalculationMode.Sum;

    /// <summary>
    /// Factor de ajuste global
    /// </summary>
    public decimal GlobalAdjustmentFactor { get; set; } = 1.0m;

    /// <summary>
    /// Configuración de distribución de metas
    /// </summary>
    public GoalDistributionConfigDto DistributionConfig { get; set; } = new();

    /// <summary>
    /// Configuración de sincronización de metas
    /// </summary>
    public GoalSyncConfigDto SyncConfig { get; set; } = new();
}

/// <summary>
/// Modo de cálculo de metas globales
/// </summary>
public enum GlobalGoalCalculationMode
{
    /// <summary>
    /// Suma de todas las metas individuales
    /// </summary>
    Sum,

    /// <summary>
    /// Promedio ponderado
    /// </summary>
    WeightedAverage,

    /// <summary>
    /// Valor fijo configurado manualmente
    /// </summary>
    Fixed,

    /// <summary>
    /// Calculado dinámicamente basado en histórico
    /// </summary>
    Dynamic
}

/// <summary>
/// Configuración de distribución de metas
/// </summary>
public class GoalDistributionConfigDto
{
    /// <summary>
    /// Método de distribución
    /// </summary>
    public DistributionMethod Method { get; set; } = DistributionMethod.Equal;

    /// <summary>
    /// Factores de peso por sponsor
    /// </summary>
    public Dictionary<string, decimal> SponsorWeights { get; set; } = new();

    /// <summary>
    /// Distribución automática habilitada
    /// </summary>
    public bool AutoDistributionEnabled { get; set; } = true;

    /// <summary>
    /// Considerar capacidad histórica
    /// </summary>
    public bool ConsiderHistoricalCapacity { get; set; } = true;

    /// <summary>
    /// Considerar estacionalidad
    /// </summary>
    public bool ConsiderSeasonality { get; set; } = false;

    /// <summary>
    /// Período de análisis histórico (días)
    /// </summary>
    public int HistoricalAnalysisDays { get; set; } = 90;
}

/// <summary>
/// Método de distribución de metas
/// </summary>
public enum DistributionMethod
{
    /// <summary>
    /// Distribución igual entre todos
    /// </summary>
    Equal,

    /// <summary>
    /// Basado en capacidad histórica
    /// </summary>
    HistoricalCapacity,

    /// <summary>
    /// Basado en pesos configurados
    /// </summary>
    WeightedDistribution,

    /// <summary>
    /// Distribución proporcional
    /// </summary>
    Proportional,

    /// <summary>
    /// Configuración manual
    /// </summary>
    Manual
}

/// <summary>
/// Configuración de sincronización de metas
/// </summary>
public class GoalSyncConfigDto
{
    /// <summary>
    /// Sincronización automática habilitada
    /// </summary>
    public bool AutoSyncEnabled { get; set; } = true;

    /// <summary>
    /// Frecuencia de sincronización (daily, weekly, monthly)
    /// </summary>
    public string SyncFrequency { get; set; } = "daily";

    /// <summary>
    /// Hora del día para sincronización (HH:mm)
    /// </summary>
    public string SyncTime { get; set; } = "00:00";

    /// <summary>
    /// Días de la semana para sincronización
    /// </summary>
    public List<DayOfWeek> SyncDaysOfWeek { get; set; } = new() 
    { 
        DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, 
        DayOfWeek.Thursday, DayOfWeek.Friday 
    };

    /// <summary>
    /// Sincronizar con fuentes externas
    /// </summary>
    public bool SyncWithExternalSources { get; set; } = false;

    /// <summary>
    /// Fuentes externas configuradas
    /// </summary>
    public List<ExternalGoalSourceDto> ExternalSources { get; set; } = new();
}

/// <summary>
/// Fuente externa de metas
/// </summary>
public class ExternalGoalSourceDto
{
    /// <summary>
    /// Nombre de la fuente
    /// </summary>
    public string SourceName { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de fuente (api, file, database)
    /// </summary>
    public string SourceType { get; set; } = string.Empty;

    /// <summary>
    /// Configuración de conexión
    /// </summary>
    public Dictionary<string, object> ConnectionConfig { get; set; } = new();

    /// <summary>
    /// Mapeo de campos
    /// </summary>
    public Dictionary<string, string> FieldMapping { get; set; } = new();

    /// <summary>
    /// Última sincronización
    /// </summary>
    public DateTime? LastSync { get; set; }

    /// <summary>
    /// Estado de la fuente
    /// </summary>
    public string Status { get; set; } = "Active";
}

/// <summary>
/// Configuración de escalamiento de metas
/// </summary>
public class GoalEscalationConfigDto
{
    /// <summary>
    /// Escalamiento automático habilitado
    /// </summary>
    public bool AutoEscalationEnabled { get; set; } = false;

    /// <summary>
    /// Umbral de bajo rendimiento para escalamiento (%)
    /// </summary>
    public decimal LowPerformanceThreshold { get; set; } = 70;

    /// <summary>
    /// Umbral de alto rendimiento para escalamiento (%)
    /// </summary>
    public decimal HighPerformanceThreshold { get; set; } = 120;

    /// <summary>
    /// Período de evaluación para escalamiento (días)
    /// </summary>
    public int EvaluationPeriodDays { get; set; } = 7;

    /// <summary>
    /// Factor de escalamiento hacia arriba
    /// </summary>
    public decimal UpwardEscalationFactor { get; set; } = 1.1m;

    /// <summary>
    /// Factor de escalamiento hacia abajo
    /// </summary>
    public decimal DownwardEscalationFactor { get; set; } = 0.9m;

    /// <summary>
    /// Límite máximo de escalamiento (% de meta original)
    /// </summary>
    public decimal MaxEscalationLimit { get; set; } = 150;

    /// <summary>
    /// Límite mínimo de escalamiento (% de meta original)
    /// </summary>
    public decimal MinEscalationLimit { get; set; } = 50;

    /// <summary>
    /// Configuraciones de escalamiento por nivel
    /// </summary>
    public List<EscalationLevelConfigDto> EscalationLevels { get; set; } = new();
}

/// <summary>
/// Configuración de nivel de escalamiento
/// </summary>
public class EscalationLevelConfigDto
{
    /// <summary>
    /// Nivel de escalamiento
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Descripción del nivel
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Umbral de rendimiento para este nivel (%)
    /// </summary>
    public decimal PerformanceThreshold { get; set; }

    /// <summary>
    /// Factor de ajuste para este nivel
    /// </summary>
    public decimal AdjustmentFactor { get; set; }

    /// <summary>
    /// Acciones automáticas en este nivel
    /// </summary>
    public List<string> AutoActions { get; set; } = new();

    /// <summary>
    /// Notificaciones en este nivel
    /// </summary>
    public List<string> Notifications { get; set; } = new();
}

/// <summary>
/// Configuración de ajuste automático de metas
/// </summary>
public class AutoAdjustmentConfigDto
{
    /// <summary>
    /// Ajuste automático habilitado
    /// </summary>
    public bool AutoAdjustmentEnabled { get; set; } = false;

    /// <summary>
    /// Tipo de ajuste automático
    /// </summary>
    public AutoAdjustmentType AdjustmentType { get; set; } = AutoAdjustmentType.Performance;

    /// <summary>
    /// Frecuencia de ajuste
    /// </summary>
    public string AdjustmentFrequency { get; set; } = "weekly";

    /// <summary>
    /// Período de análisis para ajuste (días)
    /// </summary>
    public int AnalysisPeriodDays { get; set; } = 14;

    /// <summary>
    /// Sensibilidad del ajuste (1-10, siendo 10 más sensible)
    /// </summary>
    public int AdjustmentSensitivity { get; set; } = 5;

    /// <summary>
    /// Límites de ajuste automático
    /// </summary>
    public AdjustmentLimitsDto AdjustmentLimits { get; set; } = new();

    /// <summary>
    /// Algoritmo de ajuste
    /// </summary>
    public string AdjustmentAlgorithm { get; set; } = "linear_regression";

    /// <summary>
    /// Parámetros del algoritmo
    /// </summary>
    public Dictionary<string, object> AlgorithmParameters { get; set; } = new();
}

/// <summary>
/// Tipo de ajuste automático
/// </summary>
public enum AutoAdjustmentType
{
    /// <summary>
    /// Basado en rendimiento histórico
    /// </summary>
    Performance,

    /// <summary>
    /// Basado en tendencias
    /// </summary>
    Trends,

    /// <summary>
    /// Basado en estacionalidad
    /// </summary>
    Seasonality,

    /// <summary>
    /// Algoritmo de machine learning
    /// </summary>
    MachineLearning,

    /// <summary>
    /// Combinación de múltiples factores
    /// </summary>
    Hybrid
}

/// <summary>
/// Límites de ajuste automático
/// </summary>
public class AdjustmentLimitsDto
{
    /// <summary>
    /// Ajuste máximo hacia arriba (% de meta actual)
    /// </summary>
    public decimal MaxUpwardAdjustment { get; set; } = 25;

    /// <summary>
    /// Ajuste máximo hacia abajo (% de meta actual)
    /// </summary>
    public decimal MaxDownwardAdjustment { get; set; } = 25;

    /// <summary>
    /// Ajuste mínimo requerido para aplicar (% de meta actual)
    /// </summary>
    public decimal MinimumAdjustmentThreshold { get; set; } = 5;

    /// <summary>
    /// Número máximo de ajustes por período
    /// </summary>
    public int MaxAdjustmentsPerPeriod { get; set; } = 2;

    /// <summary>
    /// Días mínimos entre ajustes
    /// </summary>
    public int MinDaysBetweenAdjustments { get; set; } = 7;
}

/// <summary>
/// Configuración de flexibilidad de metas
/// </summary>
public class GoalFlexibilityConfigDto
{
    /// <summary>
    /// Flexibilidad habilitada
    /// </summary>
    public bool FlexibilityEnabled { get; set; } = true;

    /// <summary>
    /// Porcentaje de tolerancia hacia abajo
    /// </summary>
    public decimal DownwardTolerancePercentage { get; set; } = 10;

    /// <summary>
    /// Porcentaje de tolerancia hacia arriba
    /// </summary>
    public decimal UpwardTolerancePercentage { get; set; } = 20;

    /// <summary>
    /// Período de gracia para nuevos ejecutivos (días)
    /// </summary>
    public int NewExecutiveGracePeriodDays { get; set; } = 14;

    /// <summary>
    /// Compensación entre períodos habilitada
    /// </summary>
    public bool PeriodCompensationEnabled { get; set; } = true;

    /// <summary>
    /// Días máximos para compensación
    /// </summary>
    public int MaxCompensationDays { get; set; } = 30;

    /// <summary>
    /// Configuraciones especiales por situaciones
    /// </summary>
    public List<SpecialSituationConfigDto> SpecialSituations { get; set; } = new();
}

/// <summary>
/// Configuración para situaciones especiales
/// </summary>
public class SpecialSituationConfigDto
{
    /// <summary>
    /// Nombre de la situación
    /// </summary>
    public string SituationName { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la situación
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Factor de ajuste para esta situación
    /// </summary>
    public decimal AdjustmentFactor { get; set; } = 1.0m;

    /// <summary>
    /// Duración máxima de la situación (días)
    /// </summary>
    public int MaxDurationDays { get; set; } = 30;

    /// <summary>
    /// Aplicación automática habilitada
    /// </summary>
    public bool AutoApplicationEnabled { get; set; } = false;

    /// <summary>
    /// Condiciones para aplicación automática
    /// </summary>
    public List<string> AutoApplicationConditions { get; set; } = new();
}

/// <summary>
/// Configuración de incentivos
/// </summary>
public class IncentiveConfigDto
{
    /// <summary>
    /// Incentivos habilitados
    /// </summary>
    public bool IncentivesEnabled { get; set; } = false;

    /// <summary>
    /// Tipo de incentivo (monetary, points, recognition)
    /// </summary>
    public string IncentiveType { get; set; } = "points";

    /// <summary>
    /// Umbrales para incentivos
    /// </summary>
    public List<IncentiveThresholdDto> IncentiveThresholds { get; set; } = new();

    /// <summary>
    /// Frecuencia de evaluación para incentivos
    /// </summary>
    public string EvaluationFrequency { get; set; } = "monthly";

    /// <summary>
    /// Configuración de bonificaciones
    /// </summary>
    public BonusConfigDto? BonusConfig { get; set; }
}

/// <summary>
/// Umbral de incentivo
/// </summary>
public class IncentiveThresholdDto
{
    /// <summary>
    /// Porcentaje de cumplimiento requerido
    /// </summary>
    public decimal PerformanceThreshold { get; set; }

    /// <summary>
    /// Valor del incentivo
    /// </summary>
    public decimal IncentiveValue { get; set; }

    /// <summary>
    /// Descripción del incentivo
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Tipo específico de incentivo
    /// </summary>
    public string IncentiveType { get; set; } = string.Empty;
}

/// <summary>
/// Configuración de bonificaciones
/// </summary>
public class BonusConfigDto
{
    /// <summary>
    /// Bonificaciones habilitadas
    /// </summary>
    public bool BonusEnabled { get; set; } = false;

    /// <summary>
    /// Bonificación por superación de meta (%)
    /// </summary>
    public decimal ExceedanceBonus { get; set; } = 0;

    /// <summary>
    /// Bonificación por consistencia
    /// </summary>
    public decimal ConsistencyBonus { get; set; } = 0;

    /// <summary>
    /// Bonificación por mejora
    /// </summary>
    public decimal ImprovementBonus { get; set; } = 0;

    /// <summary>
    /// Configuraciones adicionales de bonificación
    /// </summary>
    public Dictionary<string, object> AdditionalBonusSettings { get; set; } = new();
}

/// <summary>
/// Configuración especial por fechas
/// </summary>
public class SpecialDateGoalConfigDto
{
    /// <summary>
    /// Fecha específica
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Tipo de fecha especial (holiday, campaign, event)
    /// </summary>
    public string DateType { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la fecha especial
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Factor de ajuste para esta fecha
    /// </summary>
    public decimal AdjustmentFactor { get; set; } = 1.0m;

    /// <summary>
    /// Meta específica para esta fecha
    /// </summary>
    public int? SpecificGoal { get; set; }

    /// <summary>
    /// Indica si es un día no laborable
    /// </summary>
    public bool IsNonWorkingDay { get; set; } = false;

    /// <summary>
    /// Configuraciones adicionales para esta fecha
    /// </summary>
    public Dictionary<string, object> AdditionalSettings { get; set; } = new();
}