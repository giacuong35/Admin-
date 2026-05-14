using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Incident
{
    public int IncidentId { get; set; }

    public int FieldId { get; set; }

    public int ReportedByUserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int StatusId { get; set; }

    public int? HandledByUserId { get; set; }

    public DateTime? HandledAt { get; set; }

    public string? HandledNote { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Field Field { get; set; } = null!;

    public virtual User? HandledByUser { get; set; }

    public virtual User ReportedByUser { get; set; } = null!;

    public virtual IncidentStatus Status { get; set; } = null!;
}
