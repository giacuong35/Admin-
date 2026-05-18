namespace Admin.ViewModels.Invoices;

public class InvoicePageVm
{
    public DateTime Date { get; set; } = DateTime.Today;
    public List<InvoiceListItemVm> Items { get; set; } = [];
}