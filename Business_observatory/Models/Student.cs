using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Student
{
    public int IdUser { get; set; }

    public string StudentCode { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
