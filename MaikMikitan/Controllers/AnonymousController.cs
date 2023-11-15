using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Requests.GeneralRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;


    public class AnonymousController : MainController
    {
        public AnonymousController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Route("Customer/Registration")]
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
}