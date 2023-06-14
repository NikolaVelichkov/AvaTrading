using AvaTrading.Data;
using AvaTrading.Entities;
using Microsoft.EntityFrameworkCore;

namespace AvaTrading.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly NewsContext context;
        private DbSet<SubscriptionEntity> subscriptions;

        public SubscriptionRepository(NewsContext context)
        {
            this.context = context;
            subscriptions = context.Set<SubscriptionEntity>();
        }
        public IEnumerable<SubscriptionEntity> GetAll()
        {
            return subscriptions.AsEnumerable();
        }
        public SubscriptionEntity Get(Guid id)
        {
            return subscriptions.SingleOrDefault(s => s.Id == id);
        }

        public SubscriptionEntity Get(string email)
        {
            return subscriptions.FirstOrDefault(x => x.Email == email);
            ;
        }
        public void Insert(SubscriptionEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            subscriptions.Add(entity);
            context.SaveChangesAsync();
        }
        public void Update(SubscriptionEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }
        public void Delete(SubscriptionEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            subscriptions.Remove(entity);
            context.SaveChanges();
        }
    }
}
