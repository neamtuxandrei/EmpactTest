using EmpactProject.Model;

namespace EmpactProject.Repository
{
    public interface INewsRepository
    {
        List<News> NewsList { get; set; }
        Task SerializeXML();
    }
}
