using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;


    public class AnonymousController(IMediator mediator) : MainController(mediator)
    {
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
    [HttpPost("RestaurantIntro")]
    public async Task<IActionResult> RestaurantIntro(RestaurantRegistrationIntroRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var response = await mediator.Send(new RestaurantRegistrationIntroCommand(request));
        if (response.HasError) return BadRequest(response);
        return Ok(response);
    }

    [HttpPost("RestaurantIntro/VerifyOtp/Email")]
    public async Task<IActionResult> RestaurantIntroVerifyOtpEmail(GeneralRegistrationVerifyOtpRequest model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await mediator.Send(new RestaurantIntroVerifyOtpCommand(new GeneralRegistrationVerifyOtpRequest
        {
            Email = model.Email,
            Otp = model.Otp
        }));
        if (result.HasError) return BadRequest(result);
        return Ok(result);
    }
    [HttpPost("RestaurantIntro/VerifyOtp/Phone")]
    public async Task<IActionResult> RestaurantIntroVerifyOtpOtp(GeneralRegistrationVerifyOtpRequest model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await mediator.Send(new RestaurantIntroVerifyOtpCommand(new GeneralRegistrationVerifyOtpRequest
        {
            Email = model.Email,
            Otp = model.Otp
        }));
        if (result.HasError) return BadRequest(result);
        return Ok(result);
    }

    #endregion


}