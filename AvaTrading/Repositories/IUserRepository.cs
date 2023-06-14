using AvaTrading.Entities;

namespace AvaTrading.Repositories
{
    public interface IUserRepository
    {
        void Delete(UserEntity entity);
        UserEntity Get(Guid id);
        UserEntity Get(UserEntity user);
        IEnumerable<UserEntity> GetAll();
        void Insert(UserEntity entity);
        void Update(UserEntity entity);
    }
}