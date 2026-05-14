using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class VwFieldOccupancyByMonth
{
    public int FieldId { get; set; }

    public string FieldName { get; set; } = null!;

    public string FieldType { get; set; } = null!;

    public int? Year { get; set; }

    public int? Month { get; set; }

    public int? TotalSlots { get; set; }

    public int? BookedSlots { get; set; }

    public decimal? OccupancyRate { get; set; }
}
