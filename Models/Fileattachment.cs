using System;
using System.Collections.Generic;

namespace Enwage_API.Models;

public partial class Fileattachment
{
    public Guid Id { get; set; }

    public Guid? Employeeid { get; set; }

    public byte[]? Filedata { get; set; }

    public DateTime? Uploaddate { get; set; }

    public string? Filename { get; set; }

    public string? Contenttype { get; set; }

    public virtual Employee? Employee { get; set; }
}
