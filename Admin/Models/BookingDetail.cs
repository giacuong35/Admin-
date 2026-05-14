using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int BookingId { get; set; }

    public int FieldSlotId { get; set; }

    public decimal Price { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual FieldSlot FieldSlot { get; set; } = null!;
}
