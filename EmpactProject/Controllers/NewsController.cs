using EmpactProject.Data.Model;
using EmpactProject.Data.Repository;
using EmpactProject.Model;
using EmpactProject.Model.Enums;
using EmpactProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost]
        public async Task<IActionResult> SaveList(List<News> news)
        {
            if (news.IsNullOrEmpty())
                return BadRequest("List isn't valid.");
            try
            {
                _newsRepository.SaveListToDb(news);
                await _newsRepository.SaveChangesAsync();
                return Ok("List saved!");
            }catch(Exception ex)
            {
                return new ObjectResult(ex);
            }
        }
    }
}
