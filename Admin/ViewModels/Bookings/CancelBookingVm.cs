using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Bookings
{
    public class CancelBookingVm
    {
        [MaxLength(500)]
        public string? Reason { get; set; }
    }
}