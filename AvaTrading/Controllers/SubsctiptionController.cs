using AvaTrading.Entities;
using AvaTrading.Repositories;
using AvaTrading.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaTrading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubsctiptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubsctiptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("subscribe")]
        [Authorize]
        public async Task<ActionResult> Subscribe([FromForm]string email)
        {
            bool result = _subscriptionService.Subscribe(email);

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
            var result = _subscriptionService.Unsubscribe(email);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
