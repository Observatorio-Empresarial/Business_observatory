using Microsoft.AspNetCore.Identity;

namespace Business_observatory.Models
{
    public class ApplicationUser:IdentityUser
    {
		public virtual ICollection<Proyecto>? Proyectos { get; set; }
	}
}
