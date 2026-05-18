namespace Admin.ViewModels.PurchaseOrders
{
    public class PurchaseOrderItemVm
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}