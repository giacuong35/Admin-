using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class PaymentStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
