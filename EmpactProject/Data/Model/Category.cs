namespace EmpactProject.Data.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DomainUrl { get; set; }
        public List<News> News { get; set; }
    }
}
