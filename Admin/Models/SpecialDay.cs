using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class SpecialDay
{
    public int SpecialDayId { get; set; }

    public DateOnly SpecialDate { get; set; }

    public string Name { get; set; } = null!;

    public decimal PriceMultiplier { get; set; }

    public bool? IsFullDayPeak { get; set; }

    public string? Note { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;
}
