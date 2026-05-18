namespace Admin.ViewModels.Bookings
{
    public class BookingServiceItemVm
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}