using AvaTrading.Data;
using AvaTrading.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AvaTrading.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NewsContext context;
        private DbSet<UserEntity> users;
        string errorMessage = string.Empty;
        public UserRepository(NewsContext context)
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

        public UserEntity Get(UserEntity user)
        {
            return users.FirstOrDefault(x => x.Username.ToLower() ==
                user.Username.ToLower() && x.Password == user.Password);
        }
        public void Insert(UserEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            users.Add(entity);
            context.SaveChangesAsync();
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
