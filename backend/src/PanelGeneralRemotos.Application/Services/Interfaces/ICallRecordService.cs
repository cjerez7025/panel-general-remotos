// ============================================================================
// Archivo: ICallRecordService.cs
// Propósito: Interface para gestión de registros de llamadas dinámicos
// Creado: 11/08/2025 - Initial creation para manejo de datos de llamadas
// Modificado: 20/08/2025 - CORRECCIÓN: Eliminadas clases duplicadas y referencias ambiguas
// Autor: Panel General Remotos Team
// Ubicación: backend/src/PanelGeneralRemotos.Application/Services/Interfaces/ICallRecordService.cs
// ============================================================================

using PanelGeneralRemotos.Domain.Entities;
using PanelGeneralRemotos.Domain.Enums;
using PanelGeneralRemotos.Application.Models.DTOs;

namespace PanelGeneralRemotos.Application.Services.Interfaces;

/// <summary>
/// Servicio para gestión de registros de llamadas con datos dinámicos desde Google Sheets
/// Maneja la lógica de negocio para la pestaña "Llamadas por Fecha" del dashboard
/// </summary>
public interface ICallRecordService
{
    /// <summary>
    /// Obtiene todas las llamadas en un rango de fechas específico
    /// </summary>
    /// <param name="startDate">Fecha inicio del rango</param>
    /// <param name="endDate">Fecha fin del rango</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de registros de llamadas</returns>
    Task<List<CallRecord>> GetCallRecordsByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene llamadas agrupadas por sponsor para la vista principal
    /// CORRECCIÓN: Usar el namespace completo del DTO existente
    /// </summary>
    /// <param name="startDate">Fecha inicio</param>
    /// <param name="endDate">Fecha fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Llamadas agrupadas por sponsor</returns>
    Task<List<PanelGeneralRemotos.Application.Models.DTOs.SponsorCallsSummary>> GetCallsSummaryBySponsorAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene llamadas detalladas por ejecutivos de un sponsor específico (drill-down)
    /// </summary>
    /// <param name="sponsorId">ID del sponsor</param>
    /// <param name="startDate">Fecha inicio</param>
    /// <param name="endDate">Fecha fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Detalle de llamadas por ejecutivo</returns>
    Task<CallsDetailBySponsor> GetCallsDetailBySponsorAsync(int sponsorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene llamadas de un ejecutivo específico
    /// </summary>
    /// <param name="executiveId">ID del ejecutivo</param>
    /// <param name="startDate">Fecha inicio</param>
    /// <param name="endDate">Fecha fin</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Llamadas del ejecutivo</returns>
    Task<List<CallRecord>> GetCallRecordsByExecutiveAsync(int executiveId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza o crea registros de llamadas desde datos de Google Sheets
    /// Método principal para la sincronización dinámica
    /// </summary>
    /// <param name="callRecords">Lista de registros a actualizar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la actualización</returns>
    Task<UpdateResult> UpdateCallRecordsAsync(List<CallRecord> callRecords, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene estadísticas de sincronización
    /// CORRECCIÓN: Usar el namespace completo del DTO existente
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Estadísticas de sincronización</returns>
    Task<PanelGeneralRemotos.Application.Models.DTOs.SyncStatistics> GetSyncStatisticsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Marca registros como no actualizados en la última sincronización
    /// Útil para identificar datos que ya no existen en Google Sheets
    /// </summary>
    /// <param name="syncDateTime">Fecha y hora de la sincronización</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Número de registros marcados</returns>
    Task<int> MarkRecordsAsNotUpdatedAsync(DateTime syncDateTime, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina registros obsoletos que no fueron actualizados en múltiples sincronizaciones
    /// </summary>
    /// <param name="daysOld">Días de antigüedad para considerar obsoleto</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Número de registros eliminados</returns>
    Task<int> CleanupObsoleteRecordsAsync(int daysOld = 7, CancellationToken cancellationToken = default);

    /// <summary>
    /// Valida la consistencia de los datos de llamadas
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Resultado de la validación</returns>
    Task<DataValidationResult> ValidateCallRecordsConsistencyAsync(CancellationToken cancellationToken = default);
}

// ============================================================================
// ✅ CORRECCIÓN APLICADA: 
// - Eliminadas TODAS las definiciones de clases de este archivo
// - Las clases deben estar SOLO en los archivos de DTOs
// - Usado namespace completo para evitar ambigüedades
// ============================================================================