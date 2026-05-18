namespace Admin.ViewModels.Dashboard
{
    public class RevenueByMonthVm
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AvgBookingValue { get; set; }
    }
}