using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class VwRevenueByService
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public int? TotalQuantitySold { get; set; }

    public decimal? TotalRevenue { get; set; }

    public int? TotalBookings { get; set; }
}
