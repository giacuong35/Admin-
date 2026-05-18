using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Bookings
{
    public class CreateAdminWalkInBookingVm
    {
        public int? CustomerId { get; set; }

        public bool IsGuest { get; set; } = false;

        [MaxLength(100)]
        public string? GuestName { get; set; }

        [MaxLength(20)]
        public string? GuestPhone { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày")]
        public DateTime BookingDate { get; set; } = DateTime.Today;

        public int FieldId { get; set; }

        public List<int> SelectedSlotIds { get; set; } = new();

        [MaxLength(50)]
        public string? PromotionCode { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        public bool IsFullPayment { get; set; } = false;

        public int? PaymentMethodId { get; set; }

        [MaxLength(100)]
        public string? TransactionCode { get; set; }
    }
}