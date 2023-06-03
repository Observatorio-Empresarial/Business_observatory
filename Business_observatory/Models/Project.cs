using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_observatory.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? RegistrationDate { get; set; }=DateTime.Now;

    public string? Status { get; set; }="Activo";

    public int? CompanyId { get; set; }=null;
    public string? AspNetUserId { get; set; } = null!;

    public virtual ApplicationUser? ApplicationUser { get; set; }

    public virtual ICollection<Categoriesproject> Categoriesprojects { get; } = new List<Categoriesproject>();

    public virtual Company? Company { get; set; }
    public virtual ICollection<File> Files { get; } = new List<File>();
}
