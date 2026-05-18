using System;

namespace Admin.ViewModels.Bookings
{
    public class BookingDetailItemVm
    {
        public int BookingDetailId { get; set; }
        public int FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
        public DateTime SlotDate { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}