using AvaTrading.Entities;
using AvaTrading.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaTrading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ITradingNewsService _tradingNewsService;
        public NewsController(ITradingNewsService tradingNewsService)
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
            var filteredNews = await _tradingNewsService.GetNewsFromToday(-days);
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

        //[HttpPost("subscribe")]
        //[Authorize]
        //public ActionResult Subscribe(string email)
        //{
        //    if (_subscriptions.Any(s => s.Email == email))
        //    {
        //        return BadRequest("Email already subscribed");
        //    }

        //    var subscription = new Subscription
        //    {
        //        Id = Guid.NewGuid(),
        //        Email = email,
        //        CreatedAt = DateTime.UtcNow
        //    };
        //    _subscriptions.Add(subscription);

        //    return Ok(subscription);
        //}

        //[HttpGet("latest")]
        //[AllowAnonymous]
        //public ActionResult GetLatestNews()
        //{
        //    var latestNews = _news.GroupBy(n => n.InstrumentName)
        //                          .Select(g => g.OrderByDescending(n => n.PublishedUtc).First())
        //                          .Take(5)
        //                          .ToList();

        //    return Ok(latestNews);
        //}
    }
}
