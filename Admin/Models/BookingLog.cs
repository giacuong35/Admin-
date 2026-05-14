using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class BookingLog
{
    public int LogId { get; set; }

    public int BookingId { get; set; }

    public int? OldStatusId { get; set; }

    public int NewStatusId { get; set; }

    public int? ChangedByUserId { get; set; }

    public string? Note { get; set; }

    public DateTime? ChangedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual User? ChangedByUser { get; set; }

    public virtual BookingStatus NewStatus { get; set; } = null!;

    public virtual BookingStatus? OldStatus { get; set; }
}
