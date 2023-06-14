using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NewsBackgroudService.Models;
using NewsBackgroudService.Services.Contracts;
using NewsBackgroudService.Settings;

namespace NewsBackgroudService.Services
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

        public async Task CreateAsync(TradingNews tradingNews) =>
           await _tradingNewsCollection.InsertOneAsync(tradingNews);

        public async Task CreateAsync(List<TradingNews> tradingNews) =>
            await _tradingNewsCollection.InsertManyAsync(tradingNews);
    }
}
