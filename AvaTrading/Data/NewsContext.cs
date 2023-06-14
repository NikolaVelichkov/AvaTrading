using AvaTrading.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

namespace AvaTrading.Data
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options) : base(options) { }        

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<SubscriptionEntity> Subscriptions { get; set; }
    }

}
