using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Login;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers {
    public class AuthController(IMediator mediator) : MainController(mediator)
    {
        [HttpPost("CustomerLogin")]
        public async Task<IActionResult> CustomerLogin(CustomerLoginRequest request) {
            return !ModelState.IsValid ? BadRequest(ModelState):
                CheckResponse(await Mediator.Send(new CustomerLoginCommand(request)));
        }

        [HttpPost("StaffLogin")]
        public async Task<IActionResult> StaffLogin(StaffLoginRequest request) {
           return ModelState.IsValid ? BadRequest(ModelState):
                CheckResponse(await Mediator.Send(new StaffLoginCommand(request)));
        }
    }
}
