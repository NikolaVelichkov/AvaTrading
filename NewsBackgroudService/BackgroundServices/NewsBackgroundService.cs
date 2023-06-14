using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NewsBackgroudService.Models;
using NewsBackgroudService.Services.Contracts;
using System.Text;
using System.Text.Json;

namespace AvaTrading.Services
{
    public class NewsBackgroundService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly ITradingNewsService _newsService;
        private readonly IConfiguration _configuration;
        public NewsBackgroundService(IHttpClientFactory httpClientFactory, ITradingNewsService tradingNewsService, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _newsService = tradingNewsService;
            _configuration = configuration;

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
            var apiKey = _configuration.GetValue<string>("Polygon:ApiKey");
            var apiUrl = _configuration.GetValue<string>("Polygon:ApiUrl");
            StringBuilder stringBuilder = new StringBuilder();
            var endpoint = stringBuilder.Append(apiUrl).Append(apiKey).ToString();

            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<TradingNews> news = ParseNews(content);
                await _newsService.CreateAsync(news);               
            }
            else
            {
                // Handle error response
            }
        }

        private List<TradingNews> ParseNews(string content)
        {
            List<TradingNews> news = new List<TradingNews>();
            try
            {
                Dictionary<string, dynamic> keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(content);
                news = JsonSerializer.Deserialize<List<TradingNews>>(keyValuePairs["results"]);
            }
            catch
            {
                
            }

            return news;

        }
    }

}
