using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class DepositStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Deposit> Deposits { get; set; } = new List<Deposit>();
}
