using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class SystemConfig
{
    public string ConfigKey { get; set; } = null!;

    public string ConfigValue { get; set; } = null!;

    public string DataType { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }
}
