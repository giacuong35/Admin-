using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public int SupplierId { get; set; }

    public int CreatedByUserId { get; set; }

    public int StatusId { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Note { get; set; }

    public DateTime? ConfirmedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User CreatedByUser { get; set; } = null!;

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual PurchaseOrderStatus Status { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
