using AuthenticationApi.Data;
using AuthenticationApi.Entities;
using AuthenticationApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthenticationApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthenticationContext context;
        private DbSet<UserEntity> users;
        string errorMessage = string.Empty;
        public UserRepository(AuthenticationContext context)
        {
            this.context = context;
            users = context.Set<UserEntity>();
        }
        public IEnumerable<UserEntity> GetAll()
        {
            return users.AsEnumerable();
        }
        public UserEntity Get(Guid id)
        {
            return users.SingleOrDefault(s => s.Id == id);
        }

        public async Task<UserEntity>? Get(UserEntity? user)
        {
            return await users.FirstOrDefaultAsync(x => x.Username.ToLower() ==
                user.Username.ToLower() && x.Password == user.Password);
        }
        public async Task Insert(UserEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            users.Add(entity);
            await context.SaveChangesAsync();
        }
        public void Update(UserEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }
        public void Delete(UserEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            users.Remove(entity);
            context.SaveChanges();
        }
    }
}
