using System;
using System.Collections.Generic;

namespace Admin.ViewModels.Bookings
{
    public class BookingDetailVm
    {
        public int BookingId { get; set; }
        public BookingCustomerVm Customer { get; set; } = new();
        public string Status { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public string? PromotionCode { get; set; }
        public string? Note { get; set; }
        public string? CancelReason { get; set; }
        public int RescheduleCount { get; set; }
        public List<BookingDetailItemVm> Details { get; set; } = new();
        public List<BookingServiceItemVm> Services { get; set; } = new();
        public DepositVm? Deposit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}