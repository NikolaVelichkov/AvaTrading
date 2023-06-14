using AvaTrading.Entities;

namespace AvaTrading.Services
{
    public interface ITradingNewsService
    {
        Task CreateAsync(TradingNews tradingNews);
        Task CreateAsync(TradingNews[] tradingNews);
        Task<List<TradingNews>> GetAsync();
        Task<TradingNews?> GetAsync(string id);
        Task<List<TradingNews>> GetNewsFromToday(int days);
        Task RemoveAsync(string id);
        Task<List<TradingNews>> SearchNews(string text);
        Task UpdateAsync(string id, TradingNews updatedBook);
        Task<List<TradingNews>> GetNewsByInstrument(string instrumentName, int limit = 10);
    }
}