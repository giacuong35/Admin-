using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class UserStatus
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
