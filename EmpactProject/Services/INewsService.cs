using EmpactProject.Data.Model;
using EmpactProject.Model.Enums;

namespace EmpactProject.Services
{
    public interface INewsService
    {
        Task<List<News>> SortNews(SortBy sortBy, OrderBy orderBy);
        Task<List<News>> GetNewsByKey(string key);
        Task SaveNews(List<News> news);
    }
}
