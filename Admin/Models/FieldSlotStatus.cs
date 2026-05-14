using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class FieldSlotStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<FieldSlot> FieldSlots { get; set; } = new List<FieldSlot>();
}
