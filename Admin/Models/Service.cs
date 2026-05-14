using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsAvailable { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
}
