using AvaTrading.Entities;

namespace AvaTrading.Services
{
    public interface IAuthenticationService
    {
        UserEntity Authenticate(UserEntity? userLogin);
        string GenerateToken(UserEntity user);
        bool Register(UserEntity user);
    }
}