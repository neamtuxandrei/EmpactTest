using EmpactProject.Model;
using EmpactProject.Model.Enums;
using EmpactProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using System.Reflection.Metadata.Ecma335;

namespace EmpactProject.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private INewsRepository _newsRepository;
        private List<News> _newsList;
        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
            _newsList = newsRepository.GetNews();
        }
        [HttpGet]
        public IActionResult GetSortedNews([FromQuery] SortBy sortBy, OrderBy orderBy)
        {
            var sortedList = new List<News>();
            switch (sortBy)
            {
                case SortBy.PublicationDate:
                    sortedList = orderBy == OrderBy.Ascending ? _newsList.OrderBy(d => d.PublicationDate).ToList() : _newsList.OrderByDescending(d => d.PublicationDate).ToList();
                    break;
                case SortBy.Title:
                    sortedList = orderBy == OrderBy.Ascending ? _newsList.OrderBy(t => t.Title).ToList() : _newsList.OrderByDescending(t => t.Title).ToList();
                    break;
            }
            return Ok(sortedList);
        }

        [Route("key")]
        [HttpGet]
        public IActionResult GetNewsByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return BadRequest("Key not valid.");
            return Ok(_newsList.Where(news => news.Title
            .Contains(key, StringComparison.OrdinalIgnoreCase) || news.Description
            .Contains(key, StringComparison.OrdinalIgnoreCase) || news.Link
            .Contains(key, StringComparison.OrdinalIgnoreCase)).ToList());
        }
    }
}
