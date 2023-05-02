using System.Globalization;
using System.Net;
using System.Xml.Serialization;
using System.Xml;
using EmpactProject.Model.Enums;
using EmpactProject.Data.Model;
using EmpactProject.Data.Repository;

namespace EmpactProject.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public List<News> GetNewsByKey(string key)
        {
            return _newsRepository.NewsList.Where(news => news.Title
            .Contains(key, StringComparison.OrdinalIgnoreCase) || news.Description
            .Contains(key, StringComparison.OrdinalIgnoreCase) || news.Link
            .Contains(key, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<News> SortNews(SortBy sortBy, OrderBy orderBy)
        {
            var sortedList = new List<News>();
            switch (sortBy)
            {
                case SortBy.PublicationDate:
                    sortedList = orderBy == OrderBy.Ascending ? _newsRepository.NewsList.OrderBy(d => d.PublicationDate).ToList() : _newsRepository.NewsList.OrderByDescending(d => d.PublicationDate).ToList();
                    break;
                case SortBy.Title:
                    sortedList = orderBy == OrderBy.Ascending ? _newsRepository.NewsList.OrderBy(t => t.Title).ToList() : _newsRepository.NewsList.OrderByDescending(t => t.Title).ToList();
                    break;
            }
            return sortedList;
        }
        
    }
}
