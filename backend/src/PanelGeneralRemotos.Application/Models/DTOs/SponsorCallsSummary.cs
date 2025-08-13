namespace PanelGeneralRemotos.Application.Models.DTOs
{
    /// <summary>
    /// Resumen de llamadas por sponsor
    /// </summary>
    public class SponsorCallsSummary
    {
        /// <summary>
        /// Nombre del sponsor
        /// </summary>
        public string SponsorName { get; set; } = string.Empty;

        /// <summary>
        /// Total de llamadas en el período
        /// </summary>
        public int TotalCalls { get; set; }

        /// <summary>
        /// Datos diarios de llamadas
        /// </summary>
        public List<DailyCallsData> DailyCalls { get; set; } = new();

        /// <summary>
        /// Promedio de llamadas por día
        /// </summary>
        public decimal AveragePerDay { get; set; }

        /// <summary>
        /// Porcentaje de cumplimiento de meta
        /// </summary>
        public decimal GoalPercentage { get; set; }

        /// <summary>
        /// Día con más llamadas
        /// </summary>
        public DateTime? BestDay => DailyCalls.OrderByDescending(d => d.CallCount).FirstOrDefault()?.Date;

        /// <summary>
        /// Máximo de llamadas en un día
        /// </summary>
        public int MaxCallsInDay => DailyCalls.Any() ? DailyCalls.Max(d => d.CallCount) : 0;

        /// <summary>
        /// Tendencia (calculada comparando primera y última semana)
        /// </summary>
        public string Trend
        {
            get
            {
                if (DailyCalls.Count < 7) return "stable";
                
                var firstWeekAvg = DailyCalls.Take(7).Average(d => d.CallCount);
                var lastWeekAvg = DailyCalls.TakeLast(7).Average(d => d.CallCount);
                
                var difference = (lastWeekAvg - firstWeekAvg) / firstWeekAvg * 100;
                
                return difference switch
                {
                    > 5 => "up",
                    < -5 => "down",
                    _ => "stable"
                };
            }
        }
    }
}