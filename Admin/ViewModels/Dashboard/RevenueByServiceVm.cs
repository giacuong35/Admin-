namespace Admin.ViewModels.Dashboard
{
    public class RevenueByServiceVm
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalBookings { get; set; }
    }
}