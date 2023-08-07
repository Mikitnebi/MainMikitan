using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("restaurant")]
        public async Task<IActionResult> RestaurantAuthentification(string restaurant) {
            return null;        
        }
        [HttpGet]
        [Route("customer")]
        public async Task<IActionResult> CustomerAuthentification(string customer) {
            return null;
        }

    }
}
