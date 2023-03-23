using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Download
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int? IdProject { get; set; }

    public DateOnly? DownloadDate { get; set; }

    public virtual Project? IdProjectNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
