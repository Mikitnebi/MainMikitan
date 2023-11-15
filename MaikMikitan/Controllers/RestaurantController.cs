using FluentEmail.Core;
using MainMikitan.API.Extentions;
using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using static MainMikitan.Domain.Enums;
using static MainMikitan.Domain.ErrorType;
using MainMikitan.API.Controllers;
using Amazon.S3.Model.Internal.MarshallTransformations;

namespace MainMikitan.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    
    public class RestaurantController : MainController {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator) : base(mediator) {
        }
        #region Registration

        [HttpPost("registration/StarterInfo")]
        public async Task<IActionResult> RestaurantRegistrationFinal(RestaurantRegistrationStarterInfoRequest request) {
            if (ModelState.IsValid) {
                var response = await _mediator.Send(new RestaurantRegistrationFinalCommand(request, UserId));
                if (response.HasError) return BadRequest(response);
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        #endregion

        [HttpPost("CreateOrUpdateEnvironments")]
        public async Task<IActionResult> CreateOrUpdateRestaurantEnvironment(RestaurantRegistrationEnvironmentRequest request) {
            return !ModelState.IsValid ? BadRequest(ModelState) : 
                CheckResponse(await Mediator.Send(new CreateOrUpdateEnvironmentCommand(request, UserId)));
        }

    }
}
