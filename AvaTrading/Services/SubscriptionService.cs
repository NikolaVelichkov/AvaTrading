using AvaTrading.Entities;
using AvaTrading.Repositories;

namespace AvaTrading.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public bool Subscribe(string email)
        {

            var emailSearch = _subscriptionRepository.Get(email);

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

            _subscriptionRepository.Insert(subscription);
            return true;
        }

        public bool Unsubscribe(string email)
        {
            var emailSearch = _subscriptionRepository.Get(email);

            if (emailSearch is null)
            {
                return false;
            }

            _subscriptionRepository.Delete(emailSearch);

            return true;
        }
    }
}
