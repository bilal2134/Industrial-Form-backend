using Enwage_API.Models;
using System;
using System.Collections.Generic;

namespace Enwage_API.DTOs
{
    public class StatenameDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<EmployeeStatename> EmployeeStatenames { get; set; } = new List<EmployeeStatename>();
    }
}