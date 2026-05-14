using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class BookingStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BookingLog> BookingLogNewStatuses { get; set; } = new List<BookingLog>();

    public virtual ICollection<BookingLog> BookingLogOldStatuses { get; set; } = new List<BookingLog>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
