using FluentEmail.Core;
using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MainMikitan.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator) {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RestaurantRegistration(RestaurantRegistrationIntroRequest request) {
            if (ModelState.IsValid) {
                var response = await _mediator.Send(new RestaurantRegistrationIntroCommand(request));
                if (response.HasError) return BadRequest(response);
                return Ok(response);
            }
            return BadRequest("Urod");
        }

        [HttpPost]
        [Route("email-validation")]
        public async Task<IActionResult> RestaurantValidation(string email) {
            if (ModelState.IsValid) {

            }
            return BadRequest(ModelState);
        }
    }
}
