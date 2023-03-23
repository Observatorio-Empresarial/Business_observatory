using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Project
{
    public int IdProject { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? File { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Download> Downloads { get; } = new List<Download>();
}
