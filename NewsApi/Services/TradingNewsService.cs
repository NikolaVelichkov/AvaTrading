using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NewsApi.Models;
using NewsApi.Services.Contracts;
using NewsApi.Settings;

namespace NewsApi.Services
{
    public class TradingNewsService : ITradingNewsService
    {
        private readonly IMongoCollection<TradingNews> _tradingNewsCollection;

        public TradingNewsService(
            IOptions<TradingNewsDatabaseSettings> tradingNewsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                tradingNewsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                tradingNewsDatabaseSettings.Value.DatabaseName);

            _tradingNewsCollection = mongoDatabase.GetCollection<TradingNews>(
                tradingNewsDatabaseSettings.Value.TradingNewsCollectionName);
        }

        public async Task<List<TradingNews>> GetAsync() =>
            await _tradingNewsCollection.Find(_ => true).ToListAsync();

        public async Task<TradingNews?> GetAsync(string id) =>
            await _tradingNewsCollection.Find(x => x._id == id).FirstOrDefaultAsync();        

        public async Task<List<TradingNews>> GetNewsFromToday(int days)
        {
            var startDate = DateTime.UtcNow.Date.AddDays(-days);
            var filteredNews = await _tradingNewsCollection.FindAsync(n => DateTime.Parse(n.published_utc) >= startDate);
            return filteredNews.ToList();
        }


        public async Task<List<TradingNews>> GetNewsByInstrument(string instrumentName, int limit)
        {
            var latestNewsResult = await _tradingNewsCollection.FindAsync(n => n.tickers.Contains(instrumentName));
            var filteredNews = latestNewsResult.ToList().OrderByDescending(x => x.published_utc).Take(limit);
            return filteredNews.ToList();
        }

        public async Task<List<TradingNews>> SearchNews(string text)
        {
            var filter = Builders<TradingNews>.Filter.Text(text);
            var filteredNews = await _tradingNewsCollection.FindAsync(filter);

            return filteredNews.ToList();
        }
    }
}
