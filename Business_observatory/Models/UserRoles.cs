namespace Business_observatory.Models
{
    public class UserRoles
    {
        public ApplicationUser applicationUser { get; set; }
        public List<ApplicationRole> applicationRoles { get; set; }

    }
}
