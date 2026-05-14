using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class FieldSlot
{
    public int FieldSlotId { get; set; }

    public int FieldId { get; set; }

    public int SlotId { get; set; }

    public DateOnly SlotDate { get; set; }

    public decimal Price { get; set; }

    public int StatusId { get; set; }

    public DateTime? HoldExpireAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual BookingDetail? BookingDetail { get; set; }

    public virtual Field Field { get; set; } = null!;

    public virtual TimeSlot Slot { get; set; } = null!;

    public virtual FieldSlotStatus Status { get; set; } = null!;
}
