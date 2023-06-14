namespace SubscriptionApi.Services.Contracts
{
    public interface ISubscriptionService
    {
        Task<bool> Subscribe(string email);
        Task<bool> Unsubscribe(string email);
    }
}