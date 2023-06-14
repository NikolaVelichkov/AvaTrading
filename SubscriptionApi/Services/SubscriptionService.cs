using SubscriptionApi.Entities;
using SubscriptionApi.Repositories.Contracts;
using SubscriptionApi.Services.Contracts;

namespace SubscriptionApi.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<bool> Subscribe(string email)
        {

            var emailSearch = await _subscriptionRepository.Get(email);

            if (emailSearch is not null)
            {
                return false;
            }

            var subscription = new SubscriptionEntity
            {
                Id = Guid.NewGuid(),
                Email = email,
                CreatedAt = DateTime.UtcNow
            };

            await _subscriptionRepository.Insert(subscription);
            return true;
        }

        public async Task<bool> Unsubscribe(string email)
        {
            var emailSearch = await _subscriptionRepository.Get(email);

            if (emailSearch is null)
            {
                return false;
            }

            await _subscriptionRepository.Delete(emailSearch);

            return true;
        }
    }
}
