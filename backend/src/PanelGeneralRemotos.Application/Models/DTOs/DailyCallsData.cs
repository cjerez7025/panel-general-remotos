namespace PanelGeneralRemotos.Application.Models.DTOs
{
    /// <summary>
    /// Datos de llamadas por día específico
    /// </summary>
    public class DailyCallsData
    {
        /// <summary>
        /// Fecha del registro
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Número de llamadas realizadas en este día
        /// </summary>
        public int CallCount { get; set; }

        /// <summary>
        /// Meta de llamadas para este día
        /// </summary>
        public int Goal { get; set; }

        /// <summary>
        /// Porcentaje de cumplimiento (CallCount / Goal * 100)
        /// </summary>
        public decimal CompletionPercentage { get; set; }

        /// <summary>
        /// Indica si se cumplió la meta del día
        /// </summary>
        public bool GoalMet => CallCount >= Goal;

        /// <summary>
        /// Diferencia entre llamadas realizadas y meta
        /// </summary>
        public int GoalDifference => CallCount - Goal;
    }
}