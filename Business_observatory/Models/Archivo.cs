using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business_observatory.Models;

public partial class Archivo
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? Tipo { get; set; }

    public string? Extension { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

    public DateTime? FechaSubida { get; set; }

    public int ProyectosId { get; set; }

    public virtual Proyecto? Proyectos { get; set; } 
    public List<FileOnDatabaseModel>? FilesOnDatabase { get; set; }
}
