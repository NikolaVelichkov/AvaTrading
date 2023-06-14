using NewsBackgroudService.Models;

namespace NewsBackgroudService.Services.Contracts
{
    public interface ITradingNewsService
    {
        Task CreateAsync(TradingNews tradingNews);
        Task CreateAsync(List<TradingNews> tradingNews);
    }
}