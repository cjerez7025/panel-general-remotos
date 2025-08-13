// ============================================================================
// Archivo: DataValidationReportDto.cs
// Propósito: DTO para reporte de validación de datos
// Creado: 11/08/2025 - Initial creation
// Proyecto: Panel General Remotos
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/DataValidationReportDto.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

/// <summary>
/// DTO para reporte de validación de datos
/// </summary>
public class DataValidationReportDto
{
    /// <summary>
    /// Indica si la validación fue exitosa
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Score de calidad de datos (0-100)
    /// </summary>
    public decimal DataQualityScore { get; set; }

    /// <summary>
    /// Timestamp de la validación
    /// </summary>
    public DateTime ValidationDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Duración de la validación
    /// </summary>
    public TimeSpan ValidationDuration { get; set; }

    /// <summary>
    /// Errores críticos encontrados
    /// </summary>
    public List<ValidationIssueDto> CriticalErrors { get; set; } = new();

    /// <summary>
    /// Errores encontrados
    /// </summary>
    public List<ValidationIssueDto> Errors { get; set; } = new();

    /// <summary>
    /// Advertencias encontradas
    /// </summary>
    public List<ValidationIssueDto> Warnings { get; set; } = new();

    /// <summary>
    /// Información adicional
    /// </summary>
    public List<ValidationIssueDto> InfoMessages { get; set; } = new();

    /// <summary>
    /// Estadísticas de validación
    /// </summary>
    public ValidationStatisticsDto Statistics { get; set; } = new();

    /// <summary>
    /// Recomendaciones para mejorar la calidad de datos
    /// </summary>
    public List<string> Recommendations { get; set; } = new();

    /// <summary>
    /// Sponsors validados
    /// </summary>
    public List<SponsorValidationDto> SponsorValidations { get; set; } = new();

    /// <summary>
    /// Resumen ejecutivo de la validación
    /// </summary>
    public string ExecutiveSummary { get; set; } = string.Empty;

    /// <summary>
    /// Acciones recomendadas
    /// </summary>
    public List<RecommendedActionDto> RecommendedActions { get; set; } = new();
}

/// <summary>
/// Issue específico de validación
/// </summary>
public class ValidationIssueDto
{
    /// <summary>
    /// ID del issue
    /// </summary>
    public string IssueId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tipo de issue
    /// </summary>
    public ValidationIssueType IssueType { get; set; }

    /// <summary>
    /// Severidad del issue
    /// </summary>
    public IssueSeverity Severity { get; set; }

    /// <summary>
    /// Código del error
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;

    /// <summary>
    /// Título del problema
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descripción detallada del problema
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Sponsor afectado
    /// </summary>
    public string? AffectedSponsor { get; set; }

    /// <summary>
    /// Ejecutivo afectado
    /// </summary>
    public string? AffectedExecutive { get; set; }

    /// <summary>
    /// Fecha afectada
    /// </summary>
    public DateTime? AffectedDate { get; set; }

    /// <summary>
    /// Campo o columna afectada
    /// </summary>
    public string? AffectedField { get; set; }

    /// <summary>
    /// Valor esperado
    /// </summary>
    public string? ExpectedValue { get; set; }

    /// <summary>
    /// Valor actual encontrado
    /// </summary>
    public string? ActualValue { get; set; }

    /// <summary>
    /// Sugerencia de corrección
    /// </summary>
    public string? SuggestedFix { get; set; }

    /// <summary>
    /// Impacto estimado del problema
    /// </summary>
    public ValidationImpact Impact { get; set; }

    /// <summary>
    /// Ubicación del problema (fila, columna, etc.)
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Timestamp cuando se detectó
    /// </summary>
    public DateTime DetectedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Tipos de issues de validación
/// </summary>
public enum ValidationIssueType
{
    /// <summary>
    /// Datos faltantes
    /// </summary>
    MissingData,

    /// <summary>
    /// Formato inválido
    /// </summary>
    InvalidFormat,

    /// <summary>
    /// Fuera de rango esperado
    /// </summary>
    OutOfRange,

    /// <summary>
    /// Registro duplicado
    /// </summary>
    Duplicate,

    /// <summary>
    /// Datos inconsistentes
    /// </summary>
    Inconsistent,

    /// <summary>
    /// Datos obsoletos
    /// </summary>
    Obsolete,

    /// <summary>
    /// Error estructural
    /// </summary>
    StructuralError,

    /// <summary>
    /// Error de integridad referencial
    /// </summary>
    ReferentialIntegrity,

    /// <summary>
    /// Valor inesperado
    /// </summary>
    UnexpectedValue,

    /// <summary>
    /// Regla de negocio violada
    /// </summary>
    BusinessRuleViolation
}

/// <summary>
/// Severidad del issue
/// </summary>
public enum IssueSeverity
{
    /// <summary>
    /// Información
    /// </summary>
    Info,

    /// <summary>
    /// Advertencia
    /// </summary>
    Warning,

    /// <summary>
    /// Error
    /// </summary>
    Error,

    /// <summary>
    /// Crítico
    /// </summary>
    Critical
}

/// <summary>
/// Impacto de la validación
/// </summary>
public enum ValidationImpact
{
    /// <summary>
    /// Impacto bajo
    /// </summary>
    Low,

    /// <summary>
    /// Impacto medio
    /// </summary>
    Medium,

    /// <summary>
    /// Impacto alto
    /// </summary>
    High,

    /// <summary>
    /// Impacto crítico
    /// </summary>
    Critical
}

/// <summary>
/// Validación específica por sponsor
/// </summary>
public class SponsorValidationDto
{
    /// <summary>
    /// Nombre del sponsor
    /// </summary>
    public string SponsorName { get; set; } = string.Empty;

    /// <summary>
    /// Indica si el sponsor pasó la validación
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Score de calidad del sponsor (0-100)
    /// </summary>
    public decimal QualityScore { get; set; }

    /// <summary>
    /// Issues específicos del sponsor
    /// </summary>
    public List<ValidationIssueDto> SponsorIssues { get; set; } = new();

    /// <summary>
    /// Número de ejecutivos validados
    /// </summary>
    public int ExecutivesValidated { get; set; }

    /// <summary>
    /// Número de ejecutivos con problemas
    /// </summary>
    public int ExecutivesWithIssues { get; set; }

    /// <summary>
    /// Número de registros validados
    /// </summary>
    public int RecordsValidated { get; set; }

    /// <summary>
    /// Número de registros con problemas
    /// </summary>
    public int RecordsWithIssues { get; set; }

    /// <summary>
    /// Porcentaje de integridad de datos
    /// </summary>
    public decimal DataIntegrityPercentage { get; set; }

    /// <summary>
    /// Recomendaciones específicas para el sponsor
    /// </summary>
    public List<string> SponsorRecommendations { get; set; } = new();
}

/// <summary>
/// Estadísticas de validación
/// </summary>
public class ValidationStatisticsDto
{
    /// <summary>
    /// Total de registros validados
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// Registros válidos
    /// </summary>
    public int ValidRecords { get; set; }

    /// <summary>
    /// Registros con errores
    /// </summary>
    public int InvalidRecords { get; set; }

    /// <summary>
    /// Registros duplicados
    /// </summary>
    public int DuplicateRecords { get; set; }

    /// <summary>
    /// Registros obsoletos
    /// </summary>
    public int ObsoleteRecords { get; set; }

    /// <summary>
    /// Registros incompletos
    /// </summary>
    public int IncompleteRecords { get; set; }

    /// <summary>
    /// Total de sponsors validados
    /// </summary>
    public int TotalSponsors { get; set; }

    /// <summary>
    /// Sponsors con problemas
    /// </summary>
    public int SponsorsWithIssues { get; set; }

    /// <summary>
    /// Total de ejecutivos validados
    /// </summary>
    public int TotalExecutives { get; set; }

    /// <summary>
    /// Ejecutivos con problemas
    /// </summary>
    public int ExecutivesWithIssues { get; set; }

    /// <summary>
    /// Porcentaje de éxito de validación
    /// </summary>
    public decimal SuccessRate { get; set; }

    /// <summary>
    /// Issues por tipo
    /// </summary>
    public Dictionary<ValidationIssueType, int> IssuesByType { get; set; } = new();

    /// <summary>
    /// Issues por severidad
    /// </summary>
    public Dictionary<IssueSeverity, int> IssuesBySeverity { get; set; } = new();
}

/// <summary>
/// Acción recomendada
/// </summary>
public class RecommendedActionDto
{
    /// <summary>
    /// ID de la acción
    /// </summary>
    public string ActionId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Título de la acción
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la acción
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Prioridad de la acción
    /// </summary>
    public ActionPriority Priority { get; set; }

    /// <summary>
    /// Esfuerzo estimado
    /// </summary>
    public ActionEffort EstimatedEffort { get; set; }

    /// <summary>
    /// Impacto esperado
    /// </summary>
    public ActionImpact ExpectedImpact { get; set; }

    /// <summary>
    /// Categoría de la acción
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Fecha límite recomendada
    /// </summary>
    public DateTime? RecommendedDeadline { get; set; }

    /// <summary>
    /// Responsable sugerido
    /// </summary>
    public string? SuggestedOwner { get; set; }
}

/// <summary>
/// Prioridad de acción
/// </summary>
public enum ActionPriority
{
    Low,
    Medium,
    High,
    Critical,
    Immediate
}

/// <summary>
/// Esfuerzo de acción
/// </summary>
public enum ActionEffort
{
    Minimal,
    Low,
    Medium,
    High,
    Extensive
}

/// <summary>
/// Impacto de acción
/// </summary>
public enum ActionImpact
{
    Low,
    Medium,
    High,
    Critical
}