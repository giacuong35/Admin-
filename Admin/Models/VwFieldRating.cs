using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class VwFieldRating
{
    public int FieldId { get; set; }

    public string FieldName { get; set; } = null!;

    public string FieldType { get; set; } = null!;

    public int? TotalReviews { get; set; }

    public decimal? AvgRating { get; set; }

    public int? Stars5 { get; set; }

    public int? Stars4 { get; set; }

    public int? Stars3 { get; set; }

    public int? Stars2 { get; set; }

    public int? Stars1 { get; set; }
}
