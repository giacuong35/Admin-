using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class IncidentStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}
