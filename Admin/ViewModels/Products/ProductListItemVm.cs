namespace Admin.ViewModels.Products
{
    public class ProductListItemVm
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public int StockQty { get; set; }
        public int MinQty { get; set; }
        public bool IsLowStock { get; set; }
        public int StockBuffer { get; set; }
    }
}