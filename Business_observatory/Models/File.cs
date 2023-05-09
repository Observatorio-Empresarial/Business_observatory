using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_observatory.Models;

public partial class File
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? RegistrationDate { get; set; }

    public string? Format { get; set; }

    public string? Route { get; set; }

    public int? ProjectId { get; set; }

    public virtual Project? Project { get; set; }
    [NotMapped]
    public IFormFile FileUpload { get; set; }  // Propiedad para el archivo cargado

}
