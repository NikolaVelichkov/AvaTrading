using AvaTrading.Entities;

namespace AvaTrading.Repositories
{
    public interface ISubscriptionRepository
    {
        void Delete(SubscriptionEntity entity);
        SubscriptionEntity Get(Guid id);
        SubscriptionEntity Get(string email);
        IEnumerable<SubscriptionEntity> GetAll();
        void Insert(SubscriptionEntity entity);
        void Update(SubscriptionEntity entity);
    }
}