using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels.Bookings
{
    public class RecordBookingPaymentVm
    {
        [Required]
        [Range(1, 3)]
        public int MethodId { get; set; } = 1;

        [MaxLength(100)]
        public string? TransactionCode { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}