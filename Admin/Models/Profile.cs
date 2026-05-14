using System;
using System.Collections.Generic;

namespace Admin.Models;

public partial class Profile
{
    public int ProfileId { get; set; }

    public int UserId { get; set; }

    public string? AvatarUrl { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public virtual User User { get; set; } = null!;
}
