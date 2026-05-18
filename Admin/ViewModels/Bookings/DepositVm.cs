using System;

namespace Admin.ViewModels.Bookings
{
    public class DepositVm
    {
        public int DepositId { get; set; }
        public int BookingId { get; set; }
        public decimal RequiredAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public DateTime DeadlineAt { get; set; }
        public int MinutesLeft { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}