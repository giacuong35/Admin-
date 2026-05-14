using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int TypeId { get; set; }

    public decimal DiscountValue { get; set; }

    public decimal? MaxDiscount { get; set; }

    public decimal? MinOrderAmount { get; set; }

    public int? UsageLimit { get; set; }

    public int? UsageCount { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool? IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual PromotionType Type { get; set; } = null!;
}
