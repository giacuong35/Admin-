using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class VwDashboardSummary
{
    public int? PendingBookings { get; set; }

    public int? PendingDepositBookings { get; set; }

    public int? TodayConfirmed { get; set; }

    public int? ActiveFields { get; set; }

    public int? MaintenanceFields { get; set; }

    public int? NewIncidents { get; set; }

    public decimal? TodayRevenue { get; set; }

    public int? ActiveCustomers { get; set; }

    public int? LowStockCount { get; set; }

    public int? UrgentDepositCount { get; set; }
}
