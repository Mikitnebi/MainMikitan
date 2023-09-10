using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Domain.Requests.Customer;
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
        [HttpPost]
        [Route("customer-login")]
        public async Task<IActionResult> CustomerLogin(CustomerLoginRequest request) {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new CustomerLoginCommand(request));
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
