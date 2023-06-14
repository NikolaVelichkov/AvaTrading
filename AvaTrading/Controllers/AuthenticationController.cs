using AvaTrading.DTOs;
using AvaTrading.Entities;
using AvaTrading.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvaTrading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public ActionResult Login([FromBody] UserDTO userLoginDTO)
        {
            var userLogin = new UserEntity()
            {
                Username = userLoginDTO.Username,
                Password = userLoginDTO.Password
            };

            var user = _authenticationService.Authenticate(userLogin);
            if (user != null)
            {
                var token = _authenticationService.GenerateToken(user);
                return Ok(token);
            }

            return NotFound("user not found");
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public ActionResult Register([FromBody] UserDTO userDTO)
        {
            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Username = userDTO.Username,
                Password = userDTO.Password
            };

            var result = _authenticationService.Register(user);
            if (result)
            {
                
                return Ok(result);
            }

            return NotFound("user exists");
        }
    }
}
