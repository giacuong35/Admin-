using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class PeakSchedule
{
    public int PeakScheduleId { get; set; }

    public byte DayOfWeek { get; set; }

    public int SlotId { get; set; }

    public bool? IsPeak { get; set; }

    public virtual TimeSlot Slot { get; set; } = null!;
}
