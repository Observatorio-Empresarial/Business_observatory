namespace Business_observatory.Models
{
    public class UserRoles
    {
        public int Id { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public ICollection<ApplicationRole> applicationRoles { get; set; }

    }
}
