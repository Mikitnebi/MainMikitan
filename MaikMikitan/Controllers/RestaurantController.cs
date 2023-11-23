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

namespace MainMikitan.API.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class RestaurantController : ControllerBase {
    private readonly IMediator _mediator;

    public RestaurantController(IMediator mediator) {
        _mediator = mediator;
    }
    #region Registration
    [HttpPost]
    [Route("registration")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> RestaurantRegistration(RestaurantRegistrationIntroRequest request) {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new RestaurantRegistrationIntroCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("intro-email-validation")]
    [EnableCors("AllowSpecificOrigin")]
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
    [EnableCors("AllowSpecificOrigin")]
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

    [HttpPost]
      [Authorized(RoleId.Restaurant)]
      [Route("registration/StarterInfo")]
      [EnableCors("AllowSpecificOrigin")]
      public async Task<IActionResult> RestaurantRegistrationFinal(RestaurantRegistrationStarterInfoRequest request) {
          if (ModelState.IsValid) {
              var response = await _mediator.Send(new RestaurantRegistrationFinalCommand(request,User.GetId()));
              if (response.HasError) return BadRequest(response);
              return Ok(response);
          }
          return BadRequest(ModelState);
      }

    #endregion
}
