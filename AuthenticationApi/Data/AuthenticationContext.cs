using AuthenticationApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Data
{
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options) { }
        public DbSet<UserEntity> Users { get; set; }

    }
}
