using SubscriptionApi.Entities;

namespace SubscriptionApi.Repositories.Contracts
{
    public interface ISubscriptionRepository
    {
        Task Delete(SubscriptionEntity entity);
        Task<SubscriptionEntity?> Get(Guid id);
        Task<SubscriptionEntity?> Get(string email);
        Task<List<SubscriptionEntity>> GetAll();
        Task Insert(SubscriptionEntity entity);
        Task Update(SubscriptionEntity entity);
    }
}