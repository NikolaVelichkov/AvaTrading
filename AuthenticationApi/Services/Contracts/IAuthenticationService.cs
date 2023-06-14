using AuthenticationApi.Entities;

namespace AuthenticationApi.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<UserEntity> Authenticate(UserEntity? userLogin);
        string GenerateToken(UserEntity user);
        Task<bool> Register(UserEntity user);
    }
}