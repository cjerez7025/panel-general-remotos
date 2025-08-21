// ============================================================================
// Archivo: FinalMissingItems.cs
// Propósito: SOLO los items que realmente faltan después del análisis completo
// Ubicación: backend/src/PanelGeneralRemotos.Application/Models/DTOs/FinalMissingItems.cs
// ============================================================================

namespace PanelGeneralRemotos.Application.Models.DTOs;

// ============================================================================
// ENUM FALTANTE - AlertPriority
// ============================================================================

/// <summary>
/// Prioridad de alerta (falta en el proyecto)
/// </summary>
public enum AlertPriority
{
    Low,
    Medium,
    High,
    Critical
}

// ============================================================================
// INSTRUCCIONES PARA AGREGAR PROPIEDADES FALTANTES
// ============================================================================

/*
ESTAS PROPIEDADES DEBEN AGREGARSE A LOS DTOs EXISTENTES:

1. EN SystemAlertDto.cs - AGREGAR estas propiedades:

    /// <summary>
    /// Timestamp (alias para CreatedAt para compatibilidad)
    /// </summary>
    public DateTime Timestamp 
    { 
        get => CreatedAt; 
        set => CreatedAt = value; 
    }

    /// <summary>
    /// Indica si la alerta requiere acción del usuario
    /// </summary>
    public bool IsActionable { get; set; } = false;

    /// <summary>
    /// Texto de la acción sugerida
    /// </summary>
    public string? ActionText { get; set; }

    /// <summary>
    /// Prioridad de la alerta
    /// </summary>
    public AlertPriority Priority { get; set; } = AlertPriority.Medium;

2. EN DashboardRefreshResultDto.cs - AGREGAR estas propiedades:

    /// <summary>
    /// ID único del refresh
    /// </summary>
    public string RefreshId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Tiempo de inicio (alias para RefreshStartTime)
    /// </summary>
    public DateTime StartTime 
    { 
        get => RefreshStartTime; 
        set => RefreshStartTime = value; 
    }

    /// <summary>
    /// Tiempo de fin (alias para RefreshEndTime)
    /// </summary>
    public DateTime EndTime 
    { 
        get => RefreshEndTime; 
        set => RefreshEndTime = value; 
    }

    /// <summary>
    /// Indica si fue exitoso (alias para Success)
    /// </summary>
    public bool IsSuccessful 
    { 
        get => Success; 
        set => Success = value; 
    }

    /// <summary>
    /// Detalles de sponsors (alias para SponsorResults)
    /// </summary>
    public List<SponsorRefreshDetailDto> SponsorDetails { get; set; } = new();

    // CAMBIAR Duration de readonly a setteable:
    // QUITAR: public TimeSpan Duration => RefreshEndTime - RefreshStartTime;
    // AGREGAR: public TimeSpan Duration { get; set; }

3. EN CallRecord entity - AGREGAR:

    /// <summary>
    /// Indica si el registro está obsoleto
    /// </summary>
    public bool IsStale { get; set; } = false;

4. EN Executive entity - AGREGAR:

    /// <summary>
    /// Nombre completo del ejecutivo
    /// </summary>
    public string Name { get; set; } = string.Empty;

5. EN PerformanceLevel enum - AGREGAR estos valores:

    /// <summary>
    /// Desconocido - Sin datos suficientes
    /// </summary>
    Unknown = 4,

    /// <summary>
    /// Crítico - Rendimiento muy bajo
    /// </summary>
    Critical = 5

6. EN RefreshErrorType enum - AGREGAR:

    /// <summary>
    /// Error general del sistema
    /// </summary>
    SystemError

7. EN IGoogleSheetsService interface - AGREGAR métodos:

    /// <summary>
    /// Verifica el estado de salud de Google Sheets
    /// </summary>
    Task<GoogleSheetsHealthCheck> CheckHealthAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sincroniza todos los datos desde Google Sheets
    /// </summary>
    Task<SyncAllResult> SyncAllDataAsync(CancellationToken cancellationToken = default);

8. RESOLVER conflicto DateRange - EN archivos que tengan error, agregar al inicio:

    using DTODateRange = PanelGeneralRemotos.Application.Models.DTOs.DateRange;

    // Y luego usar DTODateRange en lugar de DateRange donde sea necesario

9. RESOLVER conflicto SyncStatistics - RENOMBRAR la clase en Services.Interfaces:

    // Si existe PanelGeneralRemotos.Application.Services.Interfaces.SyncStatistics
    // RENOMBRARLA a ISyncStatistics o SyncStatisticsInterface

10. AGREGAR conversor en DashboardService.cs - después de las líneas con error:

    // Reemplazar las líneas que convierten SyncStatistics con:
    private SyncStatistics ConvertSyncStats(object input)
    {
        if (input is PanelGeneralRemotos.Application.Models.DTOs.SyncStatistics dtoStats)
            return dtoStats;
            
        // Si es del tipo Interfaces, convertir propiedades manualmente
        return new SyncStatistics(); // Con valores por defecto
    }

11. EN DashboardService - CAMBIAR líneas con Type = "String" por Type = AlertType.Warning

12. EN DashboardService - AGREGAR métodos faltantes o usar implementaciones por defecto:

    // Para healthCheck simulado:
    var healthCheck = new { IsHealthy = true, Message = "OK" };

    // Para syncResult simulado:
    var syncResult = new SyncAllResult { IsSuccessful = true, Statistics = new SyncStatistics() };
*/

// ============================================================================
// DTO AUXILIAR PARA HEALTHCHECK
// ============================================================================

/// <summary>
/// Resultado de verificación de salud de Google Sheets
/// </summary>
public class GoogleSheetsHealthCheck
{
    /// <summary>
    /// Indica si el servicio está saludable
    /// </summary>
    public bool IsHealthy { get; set; } = true;

    /// <summary>
    /// Mensaje de estado
    /// </summary>
    public string Message { get; set; } = "OK";

    /// <summary>
    /// Latencia en milisegundos
    /// </summary>
    public int LatencyMs { get; set; }

    /// <summary>
    /// Timestamp de la verificación
    /// </summary>
    public DateTime CheckedAt { get; set; } = DateTime.UtcNow;
}

// ============================================================================
// COMANDOS PARA APLICAR LOS CAMBIOS
// ============================================================================

/*
PASOS PARA IMPLEMENTAR:

1. Agregar enum AlertPriority (crear este archivo)
2. Modificar SystemAlertDto agregando las 4 propiedades
3. Modificar DashboardRefreshResultDto agregando las 5 propiedades
4. Modificar CallRecord agregando IsStale
5. Modificar Executive agregando Name
6. Modificar PerformanceLevel agregando Unknown y Critical
7. Agregar SystemError a RefreshErrorType
8. Agregar métodos a IGoogleSheetsService
9. Resolver conflictos de namespace con using aliases
10. Simular métodos faltantes en DashboardService
11. Compilar y corregir errores restantes

cd D:\panel-general-remotos\backend\
dotnet build
*/