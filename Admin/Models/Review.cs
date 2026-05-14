using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int FieldId { get; set; }

    public byte Rating { get; set; }

    public string? Comment { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsVisible { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Field Field { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
