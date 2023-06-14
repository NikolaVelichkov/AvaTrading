namespace AvaTrading.Services
{
    public interface ISubscriptionService
    {
        bool Subscribe(string email);
        bool Unsubscribe(string email);
    }
}