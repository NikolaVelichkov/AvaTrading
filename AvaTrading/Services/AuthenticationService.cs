using AvaTrading.Entities;
using AvaTrading.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AvaTrading.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;

        }

        // To generate token
        public string GenerateToken(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //To authenticate user
        public UserEntity Authenticate(UserEntity? userLogin)
        {
            var currentUser = _userRepository.Get(userLogin);

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

        public bool Register(UserEntity user)
        {
            // Check if the user already exists (you may use a database query or any other check)
            UserEntity userEntity = _userRepository.Get(user);

            if (userEntity != null)
            {
                return false;
            }
            // Generate a unique salt for password hashing
            byte[] salt = GenerateSalt();

            // Hash the user's password using the generated salt
            byte[] hashedPassword = HashPassword(user.Password, salt);

            // Store the user in the database with the hashed password and salt

            _userRepository.Insert(user);

            // Return true to indicate successful registration
            return true;
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            const int iterations = 10000;
            const int bytesRequested = 32;
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: iterations,
                numBytesRequested: bytesRequested));

            return Encoding.UTF8.GetBytes(hashedPassword);
        }
    }
}
