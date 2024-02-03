using MainMikitan.Application.Features.Customer.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MainMikitan.Domain.Requests.Customer;
using static MainMikitan.Domain.Enums;
using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Queries;

namespace MainMikitan.API.Controllers
{
    [Authorized(RoleId.Customer)]
    public class CustomerController(IMediator mediator) : MainController(mediator)
    {
        [HttpPost("UpdateInterest")]
        public async Task<IActionResult> CreateOrUpdateCustomerInterest(FillCustomerInterestRequest request)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateCustomerInterestCommand(request, UserId)));
        }
        
        [HttpGet("Interests")]
        public async Task<IActionResult> GetInterests()
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new GetCustomerInterestsQuery(UserId)));
        }
        
        [HttpPost("UpdateInfo")]
        public async Task<IActionResult> CreateOrUpdateInfo(CreateOrUpdateCustomerInfoRequest request)
        {   return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateCustomerInfoCommand(request, UserId)));
        }
        
        [HttpGet("Info")]
        public async Task<IActionResult> GetInfo()
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new GetCustomerInfoQuery(UserId)));
        }
        
        [HttpPost("UpdateProfilePhoto")]
        public async Task<IActionResult> AddOrUpdateProfilePhoto(IFormFile formFile)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) : 
                CheckResponse(await Mediator.Send(new AddOrUpdateProfilePhotoCommand(formFile, UserId)));
        }

        [HttpPost("ProfilePhoto")]
        public async Task<IActionResult> GetProfilePhoto()
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetProfilePhotoQuery(UserId)));
        }

        [HttpPost("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new CustomerDeleteAccountCommand(UserId)));
        }
        
        [HttpPost("DeleteAccount/VerifyOtp")]
        public async Task<IActionResult> DeleteAccountVerifyOtp(string otp)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new CustomerDeleteAccountVerifyOtpCommand(UserId, otp)));
        }
        
        [HttpPost("PasswordReset")]
        public async Task<IActionResult> PasswordReset()
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new CustomerPasswordResetCommand(UserId)));
        }
        /*[HttpGet("RestaurantInfo")]
        public async Task<IActionResult> GetRestaurantInfo(int restaurantId)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new RestaurantInfoForCustomerQuery(UserId, restaurantId)));
        }*/
    }
}
