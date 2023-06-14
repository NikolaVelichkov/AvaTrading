using NewsApi.Models;

namespace NewsApi.Services.Contracts
{
    public interface ITradingNewsService
    {
        Task<List<TradingNews>> GetAsync();
        Task<TradingNews?> GetAsync(string id);
        Task<List<TradingNews>> GetNewsByInstrument(string instrumentName, int limit = 10);
        Task<List<TradingNews>> GetNewsFromToday(int days);
        Task<List<TradingNews>> SearchNews(string text);

    }
}