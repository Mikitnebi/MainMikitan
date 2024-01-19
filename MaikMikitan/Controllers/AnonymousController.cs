using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;


    public class AnonymousController : MainController
    {
        public AnonymousController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("Customer/Registration")]
        public async Task<IActionResult> CustomerRegistration(CustomerRegistrationRequest model)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CustomerRegistrationCommand(model)));
        }
        [HttpPost("Customer/Registration/VerifyOtp")]
        public async Task<IActionResult> CustomerRegistrationVerifyOtp(GeneralRegistrationVerifyOtpRequest model)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CustomerRegistrationVerifyOtpCommand(
                    new GeneralRegistrationVerifyOtpRequest
                    {
                        Email = model.Email,
                        Otp = model.Otp
                    })));
        }

    #region restaurant
    [HttpPost("Restaurant/Registration")]
    public async Task<IActionResult> RestaurantRegistration(RestaurantRegistrationIntroRequest request) {
        return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new RestaurantRegistrationIntroCommand(request)));
    }

    [HttpPost("Restaurant/Registration/VerifyOTP")]
    public async Task<IActionResult> RestaurantIntroVerifyOtp(GeneralRegistrationVerifyOtpRequest model) {
        if (ModelState.IsValid) {
            var result = await Mediator.Send(new RestaurantIntroVerifyOtpCommand(new Domain.Requests.GeneralRequests.GeneralRegistrationVerifyOtpRequest {
                Email = model.Email,
                Otp = model.Otp
            }));
            if (result.HasError) return BadRequest(result);
            return Ok(result);
        }
        return BadRequest(ModelState);
    }
    [HttpPost("Restaurant/Login-Info-Generation")]
    public async Task<IActionResult> LoginInfoGeneratiron(LoginInfoGeneratironRequest request) {
        if (ModelState.IsValid) {
            var result = await Mediator.Send(new LoginInfoGeneratironCommand(request));
            if (result.HasError) return BadRequest(result);
            return Ok(result);
        }
        return BadRequest(ModelState);
    }
    #endregion


}