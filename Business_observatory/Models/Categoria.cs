using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }=DateTime.Now;

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
