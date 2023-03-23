using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Incharge
{
    public int IdUser { get; set; }

    public int IdEnterprise { get; set; }

    public virtual Enterprise IdEnterpriseNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
