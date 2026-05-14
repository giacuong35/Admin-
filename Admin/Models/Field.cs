using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Field
{
    public int FieldId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal BasePrice { get; set; }

    public decimal PeakPrice { get; set; }

    public string? ImageUrl { get; set; }

    public int TypeId { get; set; }

    public int StatusId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<FieldMaintenanceLog> FieldMaintenanceLogs { get; set; } = new List<FieldMaintenanceLog>();

    public virtual ICollection<FieldPriceHistory> FieldPriceHistories { get; set; } = new List<FieldPriceHistory>();

    public virtual ICollection<FieldSlot> FieldSlots { get; set; } = new List<FieldSlot>();

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual FieldStatus Status { get; set; } = null!;

    public virtual FieldType Type { get; set; } = null!;
}
