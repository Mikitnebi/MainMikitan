using MainMikitan.API.Extentions;
using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class RestaurantController : ControllerBase {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator) {
            _mediator = mediator;
        }
        #region Registration
        [HttpPost("registration")]
        public async Task<IActionResult> RestaurantRegistration(RestaurantRegistrationIntroRequest request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _mediator.Send(new RestaurantRegistrationIntroCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("intro-email-validation")]
        public async Task<IActionResult> RestaurantIntroVerifyOtp(GeneralRegistrationVerifyOtpRequest model) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _mediator.Send(new RestaurantIntroVerifyOtpCommand(new Domain.Requests.GeneralRequests.GeneralRegistrationVerifyOtpRequest {
                Email = model.Email,
                Otp = model.Otp
            }));
            if (result.HasError) return BadRequest(result);
            return Ok(result);
        }
        [HttpPost("Login-Info-Generation")]
        public async Task<IActionResult> LoginInfoGeneratiron(LoginInfoGeneratironRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _mediator.Send(new LoginInfoGeneratironCommand(request));
            if (result.HasError) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("registration/StarterInfo")]
        [Authorized(RoleId.Restaurant)]
        public async Task<IActionResult> RestaurantRegistrationFinal(RestaurantRegistrationStarterInfoRequest request) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _mediator.Send(new RestaurantRegistrationFinalCommand(request, User.GetId()));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }

        #endregion
    }
}
