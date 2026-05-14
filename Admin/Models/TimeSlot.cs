using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class TimeSlot
{
    public int SlotId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool? IsPeakHour { get; set; }

    public virtual ICollection<FieldSlot> FieldSlots { get; set; } = new List<FieldSlot>();

    public virtual ICollection<PeakSchedule> PeakSchedules { get; set; } = new List<PeakSchedule>();
}
