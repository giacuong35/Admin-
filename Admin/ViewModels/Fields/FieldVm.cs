namespace Admin.ViewModels.Fields
{
    public class FieldVm
    {
        public int FieldId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public decimal PeakPrice { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public string? ImageUrl { get; set; }
    }
}