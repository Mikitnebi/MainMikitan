using MainMikitan.Application.Features.Customer.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MainMikitan.Domain.Requests.Customer;
using static MainMikitan.Domain.Enums;
using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Queries;
using MainMikitan.Domain.Requests.Customer.Feature;

namespace MainMikitan.API.Controllers
{
    [Authorized(RoleId.Customer)]
    public class CustomerController(IMediator mediator) : MainController(mediator)
    {
        [HttpPost("UpdateInterest")]
        public async Task<IActionResult> CreateOrUpdateCustomerInterest(FillCustomerInterestRequest request, CancellationToken cancellationToken)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateCustomerInterestCommand(request, UserId), cancellationToken));
        }
        
        [HttpGet("Interests")]
        public async Task<IActionResult> GetInterests(CancellationToken cancellationToken)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new GetCustomerInterestsQuery(UserId), cancellationToken));
        }
        
        [HttpPost("UpdateInfo")]
        public async Task<IActionResult> CreateOrUpdateInfo(CreateOrUpdateCustomerInfoRequest request, CancellationToken cancellationToken)
        {   return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateCustomerInfoCommand(request, UserId), cancellationToken));
        }
        
        [HttpGet("Info")]
        public async Task<IActionResult> GetInfo(CancellationToken cancellationToken)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new GetCustomerInfoQuery(UserId), cancellationToken));
        }
        
        [HttpPost("UpdateProfilePhoto")]
        public async Task<IActionResult> AddOrUpdateProfilePhoto(IFormFile formFile, CancellationToken cancellationToken)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) : 
                CheckResponse(await Mediator.Send(new AddOrUpdateProfilePhotoCommand(formFile, UserId), cancellationToken));
        }

        [HttpPost("ProfilePhoto")]
        public async Task<IActionResult> GetProfilePhoto(CancellationToken cancellationToken)
        {
            return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetProfilePhotoQuery(UserId), cancellationToken));
        }

        [HttpPost("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new CustomerDeleteAccountCommand(UserId), cancellationToken));
        }
        
        [HttpPost("DeleteAccount/VerifyOtp")]
        public async Task<IActionResult> DeleteAccountVerifyOtp(string otp, CancellationToken cancellationToken)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new CustomerDeleteAccountVerifyOtpCommand(UserId, otp), cancellationToken));
        }
        
        [HttpPost("PasswordReset")]
        public async Task<IActionResult> PasswordReset(CancellationToken cancellationToken)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new CustomerPasswordResetCommand(UserId), cancellationToken));
        }
        [HttpPost("PasswordReset/VerifyOtp")]
        public async Task<IActionResult> PasswordResetVerifyOtp(CustomerPasswordResetVerifyOtpModel model, CancellationToken cancellationToken)
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new CustomerPasswordResetVerifyOtpCommand(model, UserId), cancellationToken));
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
