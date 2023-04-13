using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Categoriesproject> Categoriesprojects { get; } = new List<Categoriesproject>();
}
