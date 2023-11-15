using FluentEmail.Core;
using MainMikitan.API.Extentions;
using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static MainMikitan.Domain.Enums;
using static MainMikitan.Domain.ErrorType;
using MainMikitan.API.Controllers;

namespace MainMikitan.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    [Authorized(RoleId.Restaurant)]
    public class RestaurantController : ControllerBase {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator) {
            _mediator = mediator;
        }
        #region Registration
        [HttpPost("registration")]
        public async Task<IActionResult> RestaurantRegistration(RestaurantRegistrationIntroRequest request) {
            if (ModelState.IsValid) {
                var response = await _mediator.Send(new RestaurantRegistrationIntroCommand(request));
                if (response.HasError) return BadRequest(response);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("intro-email-validation")]
        public async Task<IActionResult> RestaurantIntroVerifyOtp(GeneralRegistrationVerifyOtpRequest model) {
            if (ModelState.IsValid) {
                var result = await _mediator.Send(new RestaurantIntroVerifyOtpCommand(new Domain.Requests.GeneralRequests.GeneralRegistrationVerifyOtpRequest {
                    Email = model.Email,
                    Otp = model.Otp
                }));
                if (result.HasError) return BadRequest(result);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login-Info-Generation")]
        public async Task<IActionResult> LoginInfoGeneratiron(LoginInfoGeneratironRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new LoginInfoGeneratironCommand(request));
                if (result.HasError) return BadRequest(result);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("registration/StarterInfo")]
          public async Task<IActionResult> RestaurantRegistrationFinal(RestaurantRegistrationStarterInfoRequest request) {
              if (ModelState.IsValid) {
                  var response = await _mediator.Send(new RestaurantRegistrationFinalCommand(request, UserId));
                  if (response.HasError) return BadRequest(response);
                  return Ok(response);
              }
              return BadRequest(ModelState);
          }

        #endregion

        [HttpPost("CreateOrUpdateEnvironments")]
        public async Task<IActionResult> CreateOrUpdateRestaurantEnvironment(RestaurantRegistrationEnvironmentRequest request) {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateEnvironmentCommand(request, UserId)));
        }

    }
}
