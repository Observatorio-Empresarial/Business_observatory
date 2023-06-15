using Microsoft.AspNetCore.Identity;

namespace Business_observatory.Models
{
    public class UsuarioEncargado
    {
        public string CorreoEncargado { get; set; }
        public Contacto Contacto { get; set; }

    }
}
