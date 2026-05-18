namespace Admin.ViewModels.PurchaseOrders
{
    public class PurchaseOrderListItemVm
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrderSupplierVm Supplier { get; set; } = new();
        public string CreatedBy { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Note { get; set; }
    }
}