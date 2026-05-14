using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Unit { get; set; }

    public int? StockQty { get; set; }

    public int? MinQty { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();
}
