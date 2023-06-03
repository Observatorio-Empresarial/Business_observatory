namespace Business_observatory.Models
{
    public class ProjectCreate
    {
        public Project? Project { get; set; }    
        public List<Category>? Categories { get; set; }
    }
}
