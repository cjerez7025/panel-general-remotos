// ============================================================================
// Archivo: IDashboardService.cs
// Propósito: Interface para el servicio principal del dashboard con datos dinámicos
// Creado: 11/08/2025 - Initial creation para manejo de datos del dashboard
// Modificado: 11/08/2025 - Added real-time dashboard aggregation and drill-down capabilities
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Application/Services/Interfaces/IDashboardService.cs
// ============================================================================

using PanelGeneralRemotos.Domain.Entities;
using PanelGeneralRemotos.Domain.Enums;
using PanelGeneralRemotos.Application.Models.DTOs;

namespace PanelGeneralRemotos.Application.Services.Interfaces;

/// <summary>
/// Servicio principal del dashboard para agregación de datos en tiempo real
/// Maneja la lógica de negocio para todas las vistas del dashboard principal
/// </summary>
public interface IDashboardService
{
    #region Dashboard Principal

    /// <summary>
    /// Obtiene el resumen completo del dashboard con todos los componentes principales
    /// Método principal que alimenta toda la vista del dashboard
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Datos completos del dashboard</returns>
    Task<DashboardSummary> GetDashboardSummaryAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene las estadísticas rápidas para los cards superiores del dashboard
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Estadísticas rápidas (Quick Stats)</returns>
    Task<QuickStatsDto> GetQuickStatsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene las alertas activas del sistema para la sección de warnings
    /// </summary>
    /// <param name="severityFilter">Filtro opcional por severidad</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de alertas del sistema</returns>
    Task<List<SystemAlertDto>> GetSystemAlertsAsync(AlertSeverity? severityFilter = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza todos los datos del dashboard mediante el botón "ACTUALIZAR"
    /// </summary>
    /// <param name="forceFullRefresh">Forzar actualización completa</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la actualización</returns>
    Task<DashboardRefreshResultDto> RefreshDashboardDataAsync(bool forceFullRefresh = false, CancellationToken cancellationToken = default);

    #endregion

    #region Pestaña "Llamadas por Fecha"

    /// <summary>
    /// Obtiene el resumen de llamadas por fecha para la vista principal de la pestaña
    /// Muestra sponsors como filas y fechas como columnas
    /// </summary>
    /// <param name="startDate">Fecha de inicio</param>
    /// <param name="endDate">Fecha de fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resumen de llamadas organizadas por fecha</returns>
    Task<CallsSummaryByDateDto> GetCallsSummaryByDateAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el detalle de llamadas para un sponsor específico (drill-down)
    /// Se ejecuta cuando se hace clic en un sponsor en la vista summary
    /// </summary>
    /// <param name="sponsorId">ID del sponsor</param>
    /// <param name="startDate">Fecha de inicio</param>
    /// <param name="endDate">Fecha de fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Detalle de llamadas por ejecutivo</returns>
    Task<CallsDetailDto> GetCallsDetailBySponsorAsync(int sponsorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    #endregion

    #region Pestaña "Rendimiento y Métricas"

    /// <summary>
    /// Obtiene el resumen de rendimiento para la vista principal de la pestaña
    /// Muestra métricas por sponsor con KPIs principales
    /// </summary>
    /// <param name="startDate">Fecha de inicio</param>
    /// <param name="endDate">Fecha de fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resumen de rendimiento por sponsor</returns>
    Task<PerformanceSummary> GetPerformanceSummaryAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el detalle de rendimiento para un sponsor específico (drill-down)
    /// Se ejecuta cuando se hace clic en un sponsor en la vista de rendimiento
    /// </summary>
    /// <param name="sponsorId">ID del sponsor</param>
    /// <param name="startDate">Fecha de inicio</param>
    /// <param name="endDate">Fecha de fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Detalle de rendimiento por ejecutivo</returns>
    Task<PerformanceDetailBySponsor> GetPerformanceDetailBySponsorAsync(int sponsorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    #endregion

    #region Gestión de Estado en Tiempo Real

    /// <summary>
    /// Obtiene el estado actual de sincronización con Google Sheets
    /// Para mostrar en el header del dashboard
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Estado de sincronización por sponsor</returns>
    Task<List<object>> GetSyncStatusAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Obtiene métricas en tiempo real para envío vía SignalR
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Métricas actualizadas para broadcast</returns>
    Task<RealTimeMetricsDto> GetRealTimeMetricsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica si hay cambios desde la última actualización
    /// </summary>
    /// <param name="lastUpdateTimestamp">Timestamp de la última actualización conocida</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>True si hay cambios disponibles</returns>
    Task<bool> HasDataChangesAsync(DateTime lastUpdateTimestamp, CancellationToken cancellationToken = default);

    #endregion

    #region Navegación y Breadcrumbs

    /// <summary>
    /// Obtiene la información de navegación para breadcrumbs
    /// </summary>
    /// <param name="currentView">Vista actual</param>
    /// <param name="sponsorId">ID del sponsor (si aplica)</param>
    /// <param name="executiveId">ID del ejecutivo (si aplica)</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de breadcrumbs para navegación</returns>
    Task<List<BreadcrumbDto>> GetNavigationBreadcrumbsAsync(string currentView, int? sponsorId = null, int? executiveId = null, CancellationToken cancellationToken = default);

    #endregion

    #region Validación y Salud del Sistema

    /// <summary>
    /// Valida la integridad de los datos del dashboard
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Reporte de validación de datos</returns>
    Task<DataValidationReportDto> ValidateDashboardDataIntegrityAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene el estado de salud general del sistema
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Indicadores de salud del sistema</returns>
    Task<SystemHealthStatus> GetSystemHealthAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Exportación de Datos

    /// <summary>
    /// Genera datos para exportación desde el dashboard
    /// </summary>
    /// <param name="exportType">Tipo de exportación (calls, performance, summary)</param>
    /// <param name="format">Formato de exportación (csv, excel, pdf)</param>
    /// <param name="startDate">Fecha de inicio</param>
    /// <param name="endDate">Fecha de fin</param>
    /// <param name="sponsorFilter">Filtro opcional por sponsor</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Datos preparados para exportación</returns>
    Task<ReportDataDto> GenerateExportDataAsync(string exportType, string format, DateTime startDate, DateTime endDate, int? sponsorFilter = null, CancellationToken cancellationToken = default);

    #endregion

    #region Configuración del Dashboard

    /// <summary>
    /// Obtiene la configuración actual del dashboard
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Configuración del dashboard</returns>
    Task<DashboardConfigurationDto> GetDashboardConfigurationAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza la configuración del dashboard
    /// </summary>
    /// <param name="configuration">Nueva configuración</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la actualización</returns>
    Task<bool> UpdateDashboardConfigurationAsync(DashboardConfigurationDto configuration, CancellationToken cancellationToken = default);

    #endregion
}

/// <summary>
/// Resumen completo del dashboard
/// </summary>
public class DashboardSummary
{
    /// <summary>
    /// Estadísticas rápidas (quick stats cards)
    /// </summary>
    public QuickStatsDto QuickStats { get; set; } = new();

    /// <summary>
    /// Alertas del sistema
    /// </summary>
    public List<SystemAlertDto> SystemAlerts { get; set; } = new();

    /// <summary>
    /// Resumen de llamadas por fecha (preview)
    /// </summary>
    public CallsSummaryPreviewDto CallsSummaryPreview { get; set; } = new();

    /// <summary>
    /// Resumen de rendimiento (preview)
    /// </summary>
    public PerformancePreviewDto PerformancePreview { get; set; } = new();

    /// <summary>
    /// Estado de sincronización
    /// </summary>
    public List<SyncStatusDto> SyncStatus { get; set; } = new();

    /// <summary>
    /// Timestamp de generación
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Próxima actualización automática
    /// </summary>
    public DateTime? NextAutoRefresh { get; set; }

    /// <summary>
    /// Indica si hay datos críticos que requieren atención
    /// </summary>
    public bool HasCriticalIssues { get; set; }
}

/// <summary>
/// Preview de llamadas para el dashboard principal
/// </summary>
public class CallsSummaryPreviewDto
{
    /// <summary>
    /// Total de llamadas hoy
    /// </summary>
    public int TotalCallsToday { get; set; }

    /// <summary>
    /// Sponsors con mejor rendimiento hoy
    /// </summary>
    public List<string> TopPerformingSponsors { get; set; } = new();

    /// <summary>
    /// Sponsors que necesitan atención
    /// </summary>
    public List<string> SponsorsNeedingAttention { get; set; } = new();

    /// <summary>
    /// Tendencia general del día
    /// </summary>
    public string TodayTrend { get; set; } = string.Empty;
}

/// <summary>
/// Preview de rendimiento para el dashboard principal
/// </summary>
public class PerformancePreviewDto
{
    /// <summary>
    /// Porcentaje de cumplimiento global
    /// </summary>
    public decimal GlobalCompliancePercentage { get; set; }

    /// <summary>
    /// Mejor sponsor del período
    /// </summary>
    public string? BestPerformingSponsor { get; set; }

    /// <summary>
    /// Sponsor que más mejoró
    /// </summary>
    public string? MostImprovedSponsor { get; set; }

    /// <summary>
    /// KPIs principales
    /// </summary>
    public Dictionary<string, decimal> KeyMetrics { get; set; } = new();
}

/// <summary>
/// Resumen de rendimiento por sponsors
/// </summary>
public class PerformanceSummary
{
    /// <summary>
    /// Rango de fechas del análisis
    /// </summary>
    public PanelGeneralRemotos.Application.Models.DTOs.DateRange DateRange { get; set; } = new();

    /// <summary>
    /// Resumen por sponsors
    /// </summary>
    public List<SponsorPerformanceSummary> SponsorSummaries { get; set; } = new();

    /// <summary>
    /// Totales consolidados
    /// </summary>
    public PerformanceTotals Totals { get; set; } = new();

    /// <summary>
    /// Timestamp de generación
    /// </summary>
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Comparación con período anterior
    /// </summary>
    public PeriodComparisonDto? PeriodComparison { get; set; }
}

/// <summary>
/// Configuración del dashboard
/// </summary>
public class DashboardConfigurationDto
{
    /// <summary>
    /// Intervalo de actualización automática (minutos)
    /// </summary>
    public int AutoRefreshIntervalMinutes { get; set; } = 5;

    /// <summary>
    /// Mostrar quick stats
    /// </summary>
    public bool ShowQuickStats { get; set; } = true;

    /// <summary>
    /// Mostrar alertas
    /// </summary>
    public bool ShowAlerts { get; set; } = true;

    /// <summary>
    /// Tema del dashboard
    /// </summary>
    public string Theme { get; set; } = "light";

    /// <summary>
    /// Sponsors a mostrar por defecto
    /// </summary>
    public List<int> DefaultSponsors { get; set; } = new();

    /// <summary>
    /// Configuración de notificaciones
    /// </summary>
    public NotificationConfigDto NotificationConfig { get; set; } = new();

    /// <summary>
    /// Configuración de exportación
    /// </summary>
    public ExportConfigDto ExportConfig { get; set; } = new();
}

/// <summary>
/// Configuración de notificaciones
/// </summary>
public class NotificationConfigDto
{
    /// <summary>
    /// Notificaciones habilitadas
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Notificar en alertas críticas
    /// </summary>
    public bool NotifyOnCriticalAlerts { get; set; } = true;

    /// <summary>
    /// Notificar en cambios de datos
    /// </summary>
    public bool NotifyOnDataChanges { get; set; } = false;

    /// <summary>
    /// Intervalo de notificación (minutos)
    /// </summary>
    public int NotificationIntervalMinutes { get; set; } = 30;
}

/// <summary>
/// Configuración de exportación
/// </summary>
public class ExportConfigDto
{
    /// <summary>
    /// Formatos habilitados
    /// </summary>
    public List<string> EnabledFormats { get; set; } = new() { "CSV", "Excel" };

    /// <summary>
    /// Incluir gráficos en exportaciones
    /// </summary>
    public bool IncludeCharts { get; set; } = false;

    /// <summary>
    /// Límite máximo de registros por exportación
    /// </summary>
    public int MaxRecordsPerExport { get; set; } = 10000;
}