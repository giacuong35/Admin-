using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class VwPendingDeposit
{
    public int BookingId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerPhone { get; set; } = null!;

    public decimal? TotalAmount { get; set; }

    public decimal DepositRequired { get; set; }

    public decimal? DepositPaid { get; set; }

    public DateTime DepositDeadline { get; set; }

    public int? MinutesLeft { get; set; }

    public DateTime? EarliestSlot { get; set; }

    public DateTime? BookingDate { get; set; }
}
