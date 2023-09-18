using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MainMikitan.Application.Features.Customer.Commands;
using Microsoft.AspNetCore.Authorization;
using MainMikitan.Domain.Requests.GeneralRequests;
using Microsoft.AspNetCore.Cors;

namespace MainMikitan.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]

    public class CustomerController : ControllerBase 
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpOptions]
        [EnableCors("AllowSpecificOrigin")] // Apply the CORS policy to this OPTIONS request
        public IActionResult Options() {
            return Ok();
        }

        #region Registration
        [HttpPost]
        [Route("registration")]
        [EnableCors("AllowSpecificOrigin")]
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
        [EnableCors("AllowSpecificOrigin")]
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

        #region CustomerInfo
        /*public async Task<IActionResult> FillCustomerInfo(FillCustomerInfoRequest request)
        {

        }*/
        #endregion
    }
}
