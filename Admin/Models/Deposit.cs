using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Deposit
{
    public int DepositId { get; set; }

    public int BookingId { get; set; }

    public decimal RequiredAmount { get; set; }

    public decimal? PaidAmount { get; set; }

    public int StatusId { get; set; }

    public DateTime DeadlineAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime? RefundedAt { get; set; }

    public DateTime? ForfeitedAt { get; set; }

    public int? PaymentId { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Payment? Payment { get; set; }

    public virtual DepositStatus Status { get; set; } = null!;
}
