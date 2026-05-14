using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class VwFieldSchedule
{
    public int FieldSlotId { get; set; }

    public int FieldId { get; set; }

    public string FieldName { get; set; } = null!;

    public string? FieldImageUrl { get; set; }

    public string FieldType { get; set; } = null!;

    public int SlotId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool? IsPeakHour { get; set; }

    public DateOnly SlotDate { get; set; }

    public decimal Price { get; set; }

    public string SlotStatus { get; set; } = null!;

    public int SlotStatusId { get; set; }

    public DateTime? HoldExpireAt { get; set; }

    public int? HoldRemainingSeconds { get; set; }
}
