using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MainMikitan.Application.Features.Customer.Commands;
using Microsoft.AspNetCore.Authorization;
using MainMikitan.Domain.Requests.GeneralRequests;

namespace MainMikitan.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

       
    public class CustomerController : ControllerBase 
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Registration
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> CustomerRegistration(CustomerRegistrationRequest model)
        {
            //???
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(new CustomerRegistrationCommand(model));
                if (result.HasError)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        [Route("email-validation")]
        public async Task<IActionResult> CustomerRegistationVerifyOtp(GeneralRegistrationVerifyOtpRequest model)
        {
            //???
            if(ModelState.IsValid)
            {
                var result = await _mediator.Send(new CustomerRegistrationVerifyOtpCommand(new GeneralRegistrationVerifyOtpRequest {
                    Email = model.Email,
                    Otp = model.Otp
                }));
                if (result.HasError) return BadRequest(result);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        #endregion
    }
}
