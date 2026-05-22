namespace Admin.ViewModels.Dashboard
{
    public class DashboardPageVm
    {
        public DashboardSummaryVm Summary { get; set; } = new();
        public List<RevenueByMonthVm> RevenueByMonth { get; set; } = new();
        public List<FieldOccupancyVm> FieldOccupancy { get; set; } = new();
        public List<RevenueByServiceVm> RevenueByService { get; set; } = new();
        public MonthlyReportVm MonthlyReport { get; set; } = new();

        public int SelectedYear { get; set; }
        public int? SelectedMonth { get; set; }

    }
}