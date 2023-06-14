using AvaTrading.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System.Text.Json;

namespace AvaTrading.Services
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

        public async Task CreateAsync(TradingNews tradingNews) =>
            await _tradingNewsCollection.InsertOneAsync(tradingNews);

        public async Task CreateAsync(TradingNews[] tradingNews) =>
            await _tradingNewsCollection.InsertManyAsync(tradingNews);

        public async Task UpdateAsync(string id, TradingNews updatedBook) =>
            await _tradingNewsCollection.ReplaceOneAsync(x => x._id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _tradingNewsCollection.DeleteOneAsync(x => x._id == id);

        public async Task<List<TradingNews>> GetNewsFromToday(int days)
        {
            var startDate = DateTime.UtcNow.Date.AddDays(-days);
            var filteredNews = await _tradingNewsCollection.FindAsync(n => DateTime.Parse(n.published_utc) >= startDate);
            return filteredNews.ToList();
        }

        
        public async Task<List<TradingNews>> GetNewsByInstrument(string instrumentName, int limit = 10)
        {
            var latestNewsResult = await _tradingNewsCollection.FindAsync(n => n.tickers.Contains(instrumentName));
            return (List<TradingNews>) latestNewsResult.ToList().OrderByDescending(x => x.published_utc).Take(limit);
        }

        public async Task<List<TradingNews>> SearchNews(string text)
        {
            var filter = Builders<TradingNews>.Filter.Text(text);
            var filteredNews = await _tradingNewsCollection.FindAsync(filter);
            
            return filteredNews.ToList();
        }

        //[HttpPost("subscribe")]
        //[Authorize]
        //public async Task<List<TradingNews>> Subscribe(string email)
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
        
    }
}
