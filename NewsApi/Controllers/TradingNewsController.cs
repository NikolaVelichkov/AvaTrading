using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Models;
using NewsApi.Services.Contracts;

namespace NewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingNewsController : ControllerBase
    {
        private readonly ITradingNewsService _tradingNewsService;
        public TradingNewsController(ITradingNewsService tradingNewsService)
        {
            _tradingNewsService = tradingNewsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllNews()
        {
            List<TradingNews> news = await _tradingNewsService.GetAsync();
            if (news is not null)
            {
                return Ok(news);

            }
            return BadRequest();
        }

        [HttpGet("today/{days}")]
        [Authorize]
        public async Task<ActionResult> GetNewsFromToday(int days)
        {
            var filteredNews = await _tradingNewsService.GetNewsFromToday(days);
            if (filteredNews is not null)
            {
                return Ok(filteredNews);

            }
            return BadRequest();
        }

        [HttpGet("instrument/{instrumentName}/{limit}")]
        [Authorize]
        public async Task<ActionResult> GetNewsByInstrument(string instrumentName, int limit = 10)
        {
            var filteredNews = await _tradingNewsService.GetNewsByInstrument(instrumentName, limit);
            if (filteredNews is not null)
            {
                return Ok(filteredNews);
            }
            return BadRequest();

        }

        [HttpGet("search/{text}")]
        [Authorize]
        public async Task<ActionResult> SearchNews(string text)
        {
            var searchedNews = await _tradingNewsService.SearchNews(text);
            if (searchedNews is not null)
            {
                return Ok(searchedNews);

            }
            return BadRequest();
        }
    }
}
