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

        public async Task<List<News>> GetNewsByKey(string key)
        {
            var list = await _newsRepository.SerializeXML();
            return list.Where(news => news.Title
            .Contains(key, StringComparison.OrdinalIgnoreCase) || news.Description
            .Contains(key, StringComparison.OrdinalIgnoreCase) || news.Link
            .Contains(key, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Task SaveNews(List<News> news)
        {
            _newsRepository.SaveListToDb(news);
            return _newsRepository.SaveChangesAsync();
        }

        public async Task<List<News>> SortNews(SortBy sortBy, OrderBy orderBy)
        {
            var list = await _newsRepository.SerializeXML();
            var sortedList = new List<News>();
            switch (sortBy)
            {
                case SortBy.PublicationDate:
                    sortedList = orderBy == OrderBy.Ascending ? list.OrderBy(d => d.PublicationDate).ToList() : list.OrderByDescending(d => d.PublicationDate).ToList();
                    break;
                case SortBy.Title:
                    sortedList = orderBy == OrderBy.Ascending ? list.OrderBy(t => t.Title).ToList() : list.OrderByDescending(t => t.Title).ToList();
                    break;
            }
            return sortedList;
        }
        
    }
}
