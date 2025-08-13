namespace PanelGeneralRemotos.Application.Models.DTOs
{
    public class ValidationResultDto
    {
        public string SheetName { get; set; } = string.Empty;
        public bool IsValid { get; set; }
        public DateTime ValidationTime { get; set; }
        public List<string> Errors { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
        public List<string> FoundHeaders { get; set; } = new();
        public int ExpectedColumns { get; set; }
        public int ActualColumns { get; set; }
    }
}