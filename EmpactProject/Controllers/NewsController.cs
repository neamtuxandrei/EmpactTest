using EmpactProject.Model;
using EmpactProject.Model.Enums;
using EmpactProject.Repository;
using EmpactProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using System.Reflection.Metadata.Ecma335;

namespace EmpactProject.Controllers
{
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;
        private readonly INewsService _newsService;
        public NewsController(INewsRepository newsRepository,INewsService newsService)
        {
            _newsRepository = newsRepository;
            _newsService = newsService;
        }
        [HttpGet]
        public IActionResult GetSortedNews([FromQuery] SortBy sortBy, OrderBy orderBy)
        {
            return Ok(_newsService.SortNews(sortBy,orderBy));
        }

        [Route("key")]
        [HttpGet]
        public IActionResult GetNewsByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return BadRequest("Key not valid.");
            return Ok(_newsService.GetNewsByKey(key));
        }
    }
}
