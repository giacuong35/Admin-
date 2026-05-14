using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int BookingId { get; set; }

    public decimal Amount { get; set; }

    public int StatusId { get; set; }

    public int MethodId { get; set; }

    public string? TransactionCode { get; set; }

    public string? GatewayResponse { get; set; }

    public string? Note { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual ICollection<Deposit> Deposits { get; set; } = new List<Deposit>();

    public virtual PaymentMethod Method { get; set; } = null!;

    public virtual PaymentStatus Status { get; set; } = null!;
}
