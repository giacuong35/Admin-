using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class VwBookingHistory
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerPhone { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string BookingStatus { get; set; } = null!;

    public int BookingStatusId { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? DepositAmount { get; set; }

    public int? RescheduleCount { get; set; }

    public string? PromotionCode { get; set; }

    public string? Note { get; set; }

    public string? CancelReason { get; set; }

    public DateTime? BookingDate { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int BookingDetailId { get; set; }

    public int FieldId { get; set; }

    public string FieldName { get; set; } = null!;

    public string FieldType { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public DateOnly SlotDate { get; set; }

    public decimal SlotPrice { get; set; }

    public decimal? PaidAmount { get; set; }

    public DateTime? PaidAt { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public decimal? DepositRequired { get; set; }

    public decimal? DepositPaid { get; set; }

    public string? DepositStatus { get; set; }
}
