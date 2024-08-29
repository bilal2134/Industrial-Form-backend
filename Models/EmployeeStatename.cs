using System;
using System.Collections.Generic;

namespace Enwage_API.Models;

public partial class EmployeeStatename
{
    public Guid EmployeeId { get; set; }

    public Guid StatenameId { get; set; }

    public Guid Id { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Statename Statename { get; set; } = null!;
}
