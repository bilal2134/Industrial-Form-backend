using System;
using System.Collections.Generic;

namespace Enwage_API.Models;

public partial class Client
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
