using EmpactProject.Data.Model;
using EmpactProject.Model;

namespace EmpactProject.Data.Repository
{
    public interface INewsRepository
    {
        List<News> NewsList { get; set; }
        Task SerializeXML();
        Task<bool> SaveChangesAsync();
        void SaveListToDb(List<News> news);
    }
}
