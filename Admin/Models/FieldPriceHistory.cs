using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class FieldPriceHistory
{
    public int HistoryId { get; set; }

    public int FieldId { get; set; }

    public decimal OldBasePrice { get; set; }

    public decimal OldPeakPrice { get; set; }

    public decimal NewBasePrice { get; set; }

    public decimal NewPeakPrice { get; set; }

    public int ChangedBy { get; set; }

    public DateTime? ChangedAt { get; set; }

    public string? Reason { get; set; }

    public virtual User ChangedByNavigation { get; set; } = null!;

    public virtual Field Field { get; set; } = null!;
}
