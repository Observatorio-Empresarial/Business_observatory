using Microsoft.AspNetCore.Identity;

namespace Business_observatory.Models
{
    public class ApplicationUser:IdentityUser
    {
		public virtual ICollection<Proyecto>? Proyectos { get; set; }
        public string? Apellido { get; set; }
        public string? Compania { get; set;}
        public string? Ciudad { get; set; }
        public string? Provincia { get; set; }
	}
}
