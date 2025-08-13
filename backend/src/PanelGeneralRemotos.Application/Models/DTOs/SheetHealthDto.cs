using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Models.DTOs
{
    public class SheetHealthDto
    {
        public int TotalSheets { get; set; }
        public int SheetsWithData { get; set; }
        public int SheetsWithoutData { get; set; }
        public int TotalRecords { get; set; }
        public DateTime LastSyncTime { get; set; }
        public List<SheetStatusDto> SheetStatuses { get; set; } = new();
    }
}