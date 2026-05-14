using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class PurchaseOrderStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
