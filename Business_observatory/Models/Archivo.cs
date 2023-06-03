using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Archivo
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Tipo { get; set; }

    public string? Extension { get; set; }

    public DateTime? FechaSubida { get; set; }

    public int ProyectosId { get; set; }

    public virtual Proyecto Proyectos { get; set; } = null!;
}
