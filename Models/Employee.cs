        using System;
using System.Collections.Generic;

namespace Enwage_API.Models;

public partial class Employee
{
    public Guid Id { get; set; }

    public bool? Changestate { get; set; }

    public DateTime? Experiencestartdate { get; set; }

    public DateTime? Experienceenddate { get; set; }

    public decimal? Hourlyrate { get; set; }

    public bool? Ispresent { get; set; }

    public DateTime? Dateofbirth { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Gender { get; set; }

    public Guid? ClientId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<EmployeeStatename> EmployeeStatenames { get; set; } = new List<EmployeeStatename>();

    public virtual ICollection<Fileattachment> Fileattachments { get; set; } = new List<Fileattachment>();
}
