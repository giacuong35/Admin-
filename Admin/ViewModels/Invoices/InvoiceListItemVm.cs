namespace Admin.ViewModels.Invoices;

public class InvoiceListItemVm
{
    public int PaymentId { get; set; }
    public string InvoiceCode { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}