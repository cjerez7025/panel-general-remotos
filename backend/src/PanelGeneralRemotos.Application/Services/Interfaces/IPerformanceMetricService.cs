using PanelGeneralRemotos.Application.Models.DTOs;
using PanelGeneralRemotos.Domain.Entities;
using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Services.Interfaces
{
    /// <summary>
    /// Servicio para el manejo de métricas de rendimiento de ejecutivos remotos
    /// Integración con Google Sheets para obtener y procesar datos de llamadas
    /// </summary>
    public interface IPerformanceMetricService
    {
        #region Métricas Generales
        
        /// <summary>
        /// Obtiene las estadísticas rápidas del dashboard
        /// </summary>
        /// <param name="date">Fecha para consultar las métricas (opcional, por defecto hoy)</param>
        /// <returns>Estadísticas rápidas del día</returns>
        Task<QuickStatsDto> GetQuickStatsAsync(DateTime? date = null);

        /// <summary>
        /// Obtiene todas las métricas de rendimiento por sponsor
        /// </summary>
        /// <param name="sponsorName">Nombre del sponsor (opcional, si es null obtiene todos)</param>
        /// <param name="startDate">Fecha de inicio del rango</param>
        /// <param name="endDate">Fecha final del rango</param>
        /// <returns>Lista de métricas por sponsor</returns>
        Task<IEnumerable<SponsorPerformanceDto>> GetSponsorPerformanceAsync(
            string? sponsorName = null, 
            DateTime? startDate = null, 
            DateTime? endDate = null);

        /// <summary>
        /// Obtiene métricas de rendimiento detalladas por ejecutivo
        /// </summary>
        /// <param name="sponsorName">Nombre del sponsor</param>
        /// <param name="executiveName">Nombre del ejecutivo (opcional)</param>
        /// <param name="startDate">Fecha de inicio del rango</param>
        /// <param name="endDate">Fecha final del rango</param>
        /// <returns>Lista de métricas por ejecutivo</returns>
        Task<IEnumerable<ExecutivePerformanceDto>> GetExecutivePerformanceAsync(
            string sponsorName,
            string? executiveName = null,
            DateTime? startDate = null,
            DateTime? endDate = null);

        #endregion

        #region Métricas de Llamadas

        /// <summary>
        /// Obtiene el resumen de llamadas por fecha para todos los sponsors
        /// </summary>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha final</param>
        /// <returns>Resumen de llamadas organizadas por sponsor y fecha</returns>
        Task<IEnumerable<CallsSummaryByDateDto>> GetCallsSummaryByDateAsync(
            DateTime startDate, 
            DateTime endDate);

        /// <summary>
        /// Obtiene el detalle de llamadas para un sponsor específico
        /// </summary>
        /// <param name="sponsorName">Nombre del sponsor</param>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha final</param>
        /// <returns>Detalle de llamadas por ejecutivo y fecha</returns>
        Task<CallsDetailDto> GetCallsDetailAsync(
            string sponsorName,
            DateTime startDate,
            DateTime endDate);

        /// <summary>
        /// Obtiene las llamadas realizadas por un ejecutivo en un rango de fechas
        /// </summary>
        /// <param name="sponsorName">Nombre del sponsor</param>
        /// <param name="executiveName">Nombre del ejecutivo</param>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha final</param>
        /// <returns>Lista de llamadas del ejecutivo</returns>
        Task<IEnumerable<CallRecordDto>> GetExecutiveCallsAsync(
            string sponsorName,
            string executiveName,
            DateTime startDate,
            DateTime endDate);

        #endregion

        #region KPIs y Análisis

        /// <summary>
        /// Obtiene los KPIs principales del sistema
        /// </summary>
        /// <param name="sponsorName">Nombre del sponsor (opcional)</param>
        /// <param name="date">Fecha para los KPIs (opcional, por defecto hoy)</param>
        /// <returns>KPIs del sistema</returns>
        Task<SystemKpisDto> GetSystemKpisAsync(string? sponsorName = null, DateTime? date = null);

        /// <summary>
        /// Calcula el porcentaje de cumplimiento de metas por sponsor
        /// </summary>
        /// <param name="sponsorName">Nombre del sponsor (opcional)</param>
        /// <param name="period">Período para calcular (daily, weekly, monthly)</param>
        /// <param name="date">Fecha de referencia</param>
        /// <returns>Porcentajes de cumplimiento</returns>
        Task<IEnumerable<GoalComplianceDto>> GetGoalComplianceAsync(
            string? sponsorName = null,
            string period = "daily",
            DateTime? date = null);

        /// <summary>
        /// Obtiene métricas comparativas entre períodos
        /// </summary>
        /// <param name="currentPeriodStart">Inicio del período actual</param>
        /// <param name="currentPeriodEnd">Final del período actual</param>
        /// <param name="previousPeriodStart">Inicio del período anterior</param>
        /// <param name="previousPeriodEnd">Final del período anterior</param>
        /// <returns>Comparación entre períodos</returns>
        Task<PeriodComparisonDto> GetPeriodComparisonAsync(
            DateTime currentPeriodStart,
            DateTime currentPeriodEnd,
            DateTime previousPeriodStart,
            DateTime previousPeriodEnd);

        #endregion

        #region Estados y Validaciones

        /// <summary>
        /// Obtiene el estado de sincronización con Google Sheets
        /// </summary>
        /// <returns>Estado de sincronización de todas las hojas</returns>
        Task<IEnumerable<SyncStatusDto>> GetSyncStatusAsync();

        /// <summary>
        /// Obtiene las alertas y warnings del sistema
        /// </summary>
        /// <param name="severity">Nivel de severidad (info, warning, error)</param>
        /// <returns>Lista de alertas activas</returns>
        Task<IEnumerable<SystemAlertDto>> GetSystemAlertsAsync(string? severity = null);

        /// <summary>
        /// Valida la integridad de los datos de Google Sheets
        /// </summary>
        /// <param name="sponsorName">Nombre del sponsor a validar (opcional)</param>
        /// <returns>Reporte de validación de datos</returns>
        Task<DataValidationReportDto> ValidateDataIntegrityAsync(string? sponsorName = null);

        #endregion

        #region Exportación y Reportes

        /// <summary>
        /// Genera reporte de rendimiento en formato específico
        /// </summary>
        /// <param name="reportType">Tipo de reporte (summary, detailed, executive)</param>
        /// <param name="sponsorName">Nombre del sponsor (opcional)</param>
        /// <param name="startDate">Fecha de inicio</param>
        /// <param name="endDate">Fecha final</param>
        /// <param name="format">Formato de exportación (json, csv, excel)</param>
        /// <returns>Datos del reporte para exportar</returns>
        Task<ReportDataDto> GeneratePerformanceReportAsync(
            string reportType,
            string? sponsorName,
            DateTime startDate,
            DateTime endDate,
            string format = "json");

        #endregion

        #region Tiempo Real y Actualizaciones

        /// <summary>
        /// Fuerza la sincronización manual con Google Sheets
        /// </summary>
        /// <param name="sponsorName">Sponsor específico a sincronizar (opcional)</param>
        /// <returns>Resultado de la sincronización</returns>
        Task<SyncResultDto> ForceSyncAsync(string? sponsorName = null);

        /// <summary>
        /// Obtiene las métricas en tiempo real para SignalR
        /// </summary>
        /// <returns>Métricas actualizadas para broadcast</returns>
        Task<RealTimeMetricsDto> GetRealTimeMetricsAsync();

        /// <summary>
        /// Verifica si hay actualizaciones disponibles desde la última sincronización
        /// </summary>
        /// <param name="lastSyncTimestamp">Timestamp de última sincronización conocida</param>
        /// <returns>True si hay actualizaciones disponibles</returns>
        Task<bool> HasUpdatesAsync(DateTime lastSyncTimestamp);

        #endregion

        #region Configuración y Metadata

        /// <summary>
        /// Obtiene la configuración actual de sponsors y ejecutivos
        /// </summary>
        /// <returns>Configuración del sistema</returns>
        Task<SystemConfigurationDto> GetSystemConfigurationAsync();

        /// <summary>
        /// Obtiene las metas configuradas por sponsor y período
        /// </summary>
        /// <param name="sponsorName">Nombre del sponsor (opcional)</param>
        /// <param name="period">Período de las metas (daily, monthly)</param>
        /// <returns>Metas configuradas</returns>
        Task<IEnumerable<GoalConfigurationDto>> GetGoalConfigurationAsync(
            string? sponsorName = null,
            string period = "daily");

        #endregion
    }
}