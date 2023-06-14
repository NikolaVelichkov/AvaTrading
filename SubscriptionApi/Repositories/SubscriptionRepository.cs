using Microsoft.EntityFrameworkCore;
using SubscriptionApi.Data;
using SubscriptionApi.Entities;
using SubscriptionApi.Repositories.Contracts;
using System.Collections.Generic;

namespace SubscriptionApi.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly SubscriptionContext context;
        private DbSet<SubscriptionEntity> subscriptions;

        public SubscriptionRepository(SubscriptionContext context)
        {
            this.context = context;
            subscriptions = context.Set<SubscriptionEntity>();
        }
        public async Task<List<SubscriptionEntity>> GetAll()
        {
            return await subscriptions.ToListAsync();
        }
        public async Task<SubscriptionEntity?> Get(Guid id)
        {
            return await subscriptions.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<SubscriptionEntity?> Get(string email)
        {
            return await subscriptions.FirstOrDefaultAsync(x => x.Email == email);
            ;
        }
        public async Task Insert(SubscriptionEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await subscriptions.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(SubscriptionEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await context.SaveChangesAsync();
        }
        public async Task Delete(SubscriptionEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            subscriptions.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
