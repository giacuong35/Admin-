using System;

namespace Admin.ViewModels.Bookings
{
    public class BookingListItemVm
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public decimal? TotalAmount { get; set; }
        public int SlotCount { get; set; }
        public DateTime? EarliestSlotDate { get; set; }
        public string? EarliestSlotTime { get; set; }
        public string? FieldName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}