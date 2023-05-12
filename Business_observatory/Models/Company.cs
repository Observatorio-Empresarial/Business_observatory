using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Nit { get; set; }

    public string? EconomicSector { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}
