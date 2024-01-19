using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Login;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthController : ControllerBase {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("customer-login")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> CustomerLogin(CustomerLoginRequest request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _mediator.Send(new CustomerLoginCommand(request));
            if (response.HasError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("restaurant-login")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> RestaurantLogin(RestaurantLoginRequest request) {
            if (ModelState.IsValid) {

                var response = await _mediator.Send(new RestaurantLoginCommand(request));
                if (response.HasError)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }

            return BadRequest(ModelState);
        }
    }
}
