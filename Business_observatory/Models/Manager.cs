using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Manager
{
    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
