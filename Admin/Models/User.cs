using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int RoleId { get; set; }

    public int StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<BookingLog> BookingLogs { get; set; } = new List<BookingLog>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<FieldMaintenanceLog> FieldMaintenanceLogs { get; set; } = new List<FieldMaintenanceLog>();

    public virtual ICollection<FieldPriceHistory> FieldPriceHistories { get; set; } = new List<FieldPriceHistory>();

    public virtual ICollection<Incident> IncidentHandledByUsers { get; set; } = new List<Incident>();

    public virtual ICollection<Incident> IncidentReportedByUsers { get; set; } = new List<Incident>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Profile? Profile { get; set; }

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SpecialDay> SpecialDays { get; set; } = new List<SpecialDay>();

    public virtual UserStatus Status { get; set; } = null!;

    public virtual ICollection<SystemConfig> SystemConfigs { get; set; } = new List<SystemConfig>();
}
