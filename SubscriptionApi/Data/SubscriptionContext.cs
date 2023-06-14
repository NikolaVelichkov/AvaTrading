using Microsoft.EntityFrameworkCore;
using SubscriptionApi.Entities;

namespace SubscriptionApi.Data
{
    public class SubscriptionContext : DbContext
    {
        public SubscriptionContext(DbContextOptions<SubscriptionContext> options) : base(options) { }
        public DbSet<SubscriptionEntity> Subscriptions { get; set; }
    }
}
