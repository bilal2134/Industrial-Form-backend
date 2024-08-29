using System;
using System.Collections.Generic;

namespace Enwage_API.Models;

public partial class Statename
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<EmployeeStatename> EmployeeStatenames { get; set; } = new List<EmployeeStatename>();
}
