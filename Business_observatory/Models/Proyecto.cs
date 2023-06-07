using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public string? Responsable { get; set; }

    public string? Empresa { get; set; }

    public DateTime? FechaCreacion { get; set; }
	public string? AspNetUserId { get; set; } = null!;
	public virtual ApplicationUser? ApplicationUser { get; set; }

	public virtual ICollection<Archivo>? Archivos { get; set; } = new List<Archivo>();

    public virtual ICollection<Categoria>? Categorias { get; set; } = new List<Categoria>();
}
