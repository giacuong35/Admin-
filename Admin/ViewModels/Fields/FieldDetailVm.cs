namespace Admin.ViewModels.Fields
{
    public class FieldDetailVm
    {
        public int FieldId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public decimal PeakPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string? FieldType { get; set; }
        public int TypeId { get; set; }
        public string? Status { get; set; }
        public int StatusId { get; set; }
        public double? AvgRating { get; set; }
        public int TotalReviews { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}