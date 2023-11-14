using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Requests.Customer;
using static MainMikitan.Domain.Enums;
using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Queries;
using Microsoft.AspNetCore.Authorization;

namespace MainMikitan.API.Controllers
{
    
    [Authorized(RoleId.Customer)]

    public class CustomerController : MainController
    {
        public CustomerController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Registration")]
        public async Task<IActionResult> CustomerRegistration(CustomerRegistrationRequest model)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CustomerRegistrationCommand(model)));
        }

        [HttpPost("Registration/VerifyOtp")]
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
        
        [HttpPost("CreateOrUpdateInterest")]
        public async Task<IActionResult> CreateOrUpdateCustomerInterest(FillCustomerInterestRequest request)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateCustomerInterestCommand(request, UserId)));
        }
        
        [HttpGet("GetInterests")]
        public async Task<IActionResult> GetInterests()
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new GetCustomerInterestsQuery(UserId)));
        }
        
        [HttpPost("CreateOrUpdateInfo")]
        public async Task<IActionResult> CreateOrUpdateInfo(CreateOrUpdateCustomerInfoRequest request)
        {   return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateCustomerInfoCommand(request, UserId)));
        }
        
        [HttpGet("GetInfo")]
        public async Task<IActionResult> GetInfo()
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new GetCustomerInfoQuery(UserId)));
        }
        
        [HttpPost("AddOrUpdateProfilePhoto")]
        public async Task<IActionResult> AddOrUpdateProfilePhoto(IFormFile formFile)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) : 
                CheckResponse(await Mediator.Send(new AddOrUpdateProfilePhotoCommand(formFile, UserId)));
        }

        [HttpPost("GetProfilePhoto")]
        public async Task<IActionResult> GetProfilePhoto()
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetProfilePhotoQuery(UserId)));
        }

        [HttpPost("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CustomerDeleteAccountCommand(UserId,UserEmail)));
        }

        [HttpDelete("DeleteAccount/VerifyOtp")]
        public async Task<IActionResult> DeleteAccountVerifyOtp(string otp)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CustomerDeleteAccountVerifyOtpCommand(UserId,UserEmail,otp)));
        }
        
    }
}
