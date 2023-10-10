using FluentEmail.Core;
using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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

        /*  [HttpPost]
          [Route("registration")]
          [EnableCors("AllowSpecificOrigin")]
          public async Task<IActionResult> RestaurantRegistrationFinal(RestaurantRegistrationFinalRequest request) {
              if (ModelState.IsValid) {
                  var response = await _mediator.Send(new RestaurantRegistrationFinalCommand(request));
                  if (response.HasError) return BadRequest(response);
                  return Ok(response);
              }
              return BadRequest(ModelState);
          }*/

        #endregion
    }
}
