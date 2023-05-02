using EmpactProject.Data.Model;
using EmpactProject.Model.Enums;

namespace EmpactProject.Services
{
    public interface INewsService
    {
        List<News> SortNews(SortBy sortBy, OrderBy orderBy);
        List<News> GetNewsByKey(string key);
    }
}
