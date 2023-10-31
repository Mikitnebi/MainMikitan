using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MainMikitan.Domain.Requests.GeneralRequests;
using Microsoft.AspNetCore.Cors;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.API.Extentions;
using static MainMikitan.Domain.Enums;
using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Queries;

namespace MainMikitan.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]

    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private int UserId => User.GetCustomerId();
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("registration")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> CustomerRegistration(CustomerRegistrationRequest model)
        {
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

        [HttpPost("email-validation")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> CustomerRegistationVerifyOtp(GeneralRegistrationVerifyOtpRequest model)
        {
            //???
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _mediator.Send(new CustomerRegistrationVerifyOtpCommand(
                new GeneralRegistrationVerifyOtpRequest
                {
                    Email = model.Email,
                    Otp = model.Otp
                }));
            if (result.HasError) return BadRequest(result);
            return Ok(result);

        }

        [Authorized(RoleId.Customer)]
        [HttpPost("CreateOrUpdateInterest")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> CreateOrUpdateCustomerInterest(FillCustomerInterestRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result =
                await _mediator.Send(new CreateOrUpdateCustomerInterestCommand(request, User.GetCustomerId()));
            if (result.HasError)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }
        
        [Authorized(RoleId.Customer)]
        [HttpGet("GetInterests")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetInterests()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result =
                await _mediator.Send(new GetCustomerInterestsQuery(UserId));
            if (result.HasError)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        
        [Authorized(RoleId.Customer)]
        [HttpPost("CreateOrUpdateInfo")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> CreateOrUpdateInfo(CreateOrUpdateCustomerInfoRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _mediator.Send(new CreateOrUpdateCustomerInfoCommand(request, User.GetCustomerId()));
            if (result.HasError)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [Authorized(RoleId.Customer)]
        [HttpGet("GetInfo")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> GetInfo()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _mediator.Send(new GetCustomerInfoQuery(UserId));
            if (result.HasError)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }
        
    }
}
