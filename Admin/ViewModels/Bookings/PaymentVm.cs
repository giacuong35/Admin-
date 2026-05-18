using System;

namespace Admin.ViewModels.Bookings
{
    public class PaymentVm
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public int MethodId { get; set; }
        public string? TransactionCode { get; set; }
        public string? Note { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}