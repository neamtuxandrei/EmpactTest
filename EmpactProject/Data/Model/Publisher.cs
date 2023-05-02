namespace EmpactProject.Data.Model
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<News> News { get; set; } = new List<News>();
    }
}
