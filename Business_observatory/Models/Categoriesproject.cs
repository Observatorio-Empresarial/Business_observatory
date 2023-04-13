using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Categoriesproject
{
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Project? Project { get; set; }
}
