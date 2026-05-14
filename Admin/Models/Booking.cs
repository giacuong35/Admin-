using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? DepositAmount { get; set; }

    public int? PromotionId { get; set; }

    public string? Note { get; set; }

    public string? CancelReason { get; set; }

    public int? RescheduleCount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual ICollection<BookingLog> BookingLogs { get; set; } = new List<BookingLog>();

    public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();

    public virtual Deposit? Deposit { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Promotion? Promotion { get; set; }

    public virtual Review? Review { get; set; }

    public virtual BookingStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
