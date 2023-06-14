using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionApi.Services.Contracts;

namespace SubscriptionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("subscribe")]
        [Authorize]
        public async Task<ActionResult> Subscribe([FromForm] string email)
        {
            bool result = await _subscriptionService.Subscribe(email);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("unsubcribe")]
        [Authorize]
        public async Task<ActionResult> Unsubscribe([FromForm] string email)
        {
            var result = await _subscriptionService.Unsubscribe(email);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
