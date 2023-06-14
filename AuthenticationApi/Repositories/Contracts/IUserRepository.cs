using AuthenticationApi.Entities;

namespace AuthenticationApi.Repositories.Contracts
{
    public interface IUserRepository
    {
        void Delete(UserEntity entity);
        UserEntity Get(Guid id);
        Task<UserEntity> Get(UserEntity user);
        IEnumerable<UserEntity> GetAll();
        Task Insert(UserEntity entity);
        void Update(UserEntity entity);
    }
}