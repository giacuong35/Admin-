namespace Admin.ViewModels.Dashboard
{
    public class MonthlyReportVm
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public decimal TotalRevenue { get; set; }
        public decimal CashRevenue { get; set; }
        public decimal VnPayRevenue { get; set; }

        public int TotalBookings { get; set; }
        public int CompletedBookings { get; set; }
        public int CancelledBookings { get; set; }
    }
}