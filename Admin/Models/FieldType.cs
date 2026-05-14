using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class FieldType
{
    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Field> Fields { get; set; } = new List<Field>();
}
