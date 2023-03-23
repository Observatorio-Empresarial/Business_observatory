using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? SecondLastName { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string TypeUser { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Status { get; set; }

    public string? AspNetUserId { get; set; }

    public virtual ICollection<Download> Downloads { get; } = new List<Download>();

    public virtual Enterprise? Enterprise { get; set; }

    public virtual Incharge? Incharge { get; set; }

    public virtual Manager? Manager { get; set; }

    public virtual Student? Student { get; set; }
}
