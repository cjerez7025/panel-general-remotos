using PanelGeneralRemotos.Domain.Enums;

namespace PanelGeneralRemotos.Application.Models.DTOs
{
    public class SheetStatusDto
    {
        public string SheetName { get; set; } = string.Empty;
        public string SpreadsheetId { get; set; } = string.Empty;
        public bool HasData { get; set; }
        public int RecordCount { get; set; }
        public DateTime? LastSyncTime { get; set; }
        public SyncStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}