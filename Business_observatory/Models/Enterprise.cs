using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Enterprise
{
    public int IdUser { get; set; }

    public string Nit { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Item { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Incharge> Incharges { get; } = new List<Incharge>();
}
