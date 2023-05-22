using EmpactProject.Data.Model;
using EmpactProject.Model;

namespace EmpactProject.Data.Repository
{
    public interface INewsRepository
    {
        Task<List<News>> SerializeXML();
        Task<bool> SaveChangesAsync();
        void SaveListToDb(List<News> news);
    }
}
