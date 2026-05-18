namespace Admin.ViewModels.Dashboard
{
    public class FieldOccupancyVm
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Month { get; set; }
        public int TotalSlots { get; set; }
        public int BookedSlots { get; set; }
        public decimal OccupancyRate { get; set; }
    }
}