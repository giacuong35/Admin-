using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Bookings
{
    public class AdminRescheduleBookingVm
    {
        public int BookingId { get; set; }

        [Required]
        public int BookingDetailId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày mới.")]
        public DateTime SelectedDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn khung giờ mới.")]
        public int NewFieldSlotId { get; set; }
    }
}