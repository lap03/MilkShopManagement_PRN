using System;
using System.Collections.Generic;

namespace Reprository.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
