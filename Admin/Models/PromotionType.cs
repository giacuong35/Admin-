using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class PromotionType
{
    public int TypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
