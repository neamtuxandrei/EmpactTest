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

        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSortedNews([FromQuery] SortBy sortBy, OrderBy orderBy)
        {
            var response = await _newsService.SortNews(sortBy, orderBy);
            return Ok(response);
        }

        [Route("key")]
        [HttpGet]
        public async Task<IActionResult> GetNewsByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return BadRequest("Key not valid.");
            var response = await _newsService.GetNewsByKey(key);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveList(List<News> news)
        {
            if (news.IsNullOrEmpty())
                return BadRequest("List isn't valid.");
            try
            {
                await _newsService.SaveNews(news);
                return Ok("List saved!");
            }catch(Exception ex)
            {
                return new ObjectResult(ex);
            }
        }
    }
}
