﻿using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Domain.Requests.Customer;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers {
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class AuthController : ControllerBase {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("customer-login")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> CustomerLogin(CustomerLoginRequest request) {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new CustomerLoginCommand(request));
                if (response.HasError)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            return BadRequest(ModelState);
        }
   //TODO
        [HttpPost]
        [Route("restaurant-login")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> RestaurantLogin() {
            if (ModelState.IsValid) {
                
                return Ok("aq response chasvit");
            }
            return BadRequest(ModelState);
        }
    }
}
