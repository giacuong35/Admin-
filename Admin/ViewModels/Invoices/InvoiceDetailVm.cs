using Admin.ViewModels.Bookings;

namespace Admin.ViewModels.Invoices;

public class InvoiceDetailVm
{
    public int PaymentId { get; set; }
    public string InvoiceCode { get; set; } = string.Empty;

    public int BookingId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;

    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
    public string? TransactionCode { get; set; }
    public string? Note { get; set; }
    public DateTime? PaidAt { get; set; }

    public List<BookingDetailItemVm> Details { get; set; } = [];
    public List<BookingServiceItemVm> Services { get; set; } = [];
}