using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class FieldMaintenanceLog
{
    public int LogId { get; set; }

    public int FieldId { get; set; }

    public string Reason { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Field Field { get; set; } = null!;
}
