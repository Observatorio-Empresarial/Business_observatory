using System;
using System.Collections.Generic;

namespace Business_observatory.Models;

public partial class Contacto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public string? Message { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }
    public string? AspNetUserId { get; set; }
    public virtual ApplicationUser? ApplicationUser { get; set; }

}
