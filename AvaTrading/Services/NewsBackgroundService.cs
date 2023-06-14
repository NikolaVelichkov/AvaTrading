using AvaTrading.Data;
using AvaTrading.Entities;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AvaTrading.Services
{
    public class NewsBackgroundService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly ITradingNewsService _newsService;
        //private readonly NewsContext _context;

        public NewsBackgroundService(IHttpClientFactory httpClientFactory, ITradingNewsService tradingNewsService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _newsService = tradingNewsService;
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GetAndSaveNews();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task GetAndSaveNews()
        {
            var apiKey = "6idzlclXq6ppoTu3fzq_xYjzbLOUGqjZ";
            var apiUrl = $"https://api.polygon.io/v2/reference/news?limit=10&order=descending&sort=published_utc&apiKey={apiKey}";

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                TradingNews[] news = ParseNews(content);
                await _newsService.CreateAsync(news);
                //await _context.TradingNews.AddRangeAsync(news);
                //await _context.SaveChangesAsync();
            }
            else
            {
                // Handle error response
            }
        }

        private TradingNews[] ParseNews(string content)
        {
            TradingNews[] news = { };
            try
            {
                Dictionary<string, dynamic> keyValuePairs = JsonSerializer.Deserialize <Dictionary<string, dynamic>>(content);
                news = JsonSerializer.Deserialize<TradingNews[]>(keyValuePairs["results"]);
                //news = result["result"];
            }
            catch
            {

                return news;
            }


            return news;

        }
    }

}
