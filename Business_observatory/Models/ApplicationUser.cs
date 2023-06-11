using Microsoft.AspNetCore.Identity;

namespace Business_observatory.Models
{
    public class ApplicationUser:IdentityUser
    {
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public virtual ICollection<Proyecto>? Proyectos { get; set; }

        public int ContactoId { get; set; }
        public virtual Contacto? Contacto { get; set; }
    }
}
