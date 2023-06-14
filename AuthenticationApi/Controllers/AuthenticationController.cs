using AuthenticationApi.DTOs;
using AuthenticationApi.Entities;
using AuthenticationApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
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
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] UserDTO userLoginDTO)
        {
            var userLogin = new UserEntity()
            {
                Username = userLoginDTO.Username,
                Password = userLoginDTO.Password
            };

            var user = await _authenticationService.Authenticate(userLogin);
            if (user != null)
            {
                var token = _authenticationService.GenerateToken(user);
                return Ok(token);
            }

            return NotFound("user not found");
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserDTO userDTO)
        {
            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Username = userDTO.Username,
                Password = userDTO.Password
            };

            var result = await _authenticationService.Register(user);
            if (result)
            {

                return NoContent();
            }

            return Conflict("user exists");
        }
    }
}
