using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MainMikitan.Application.Features.Customer.Commands;

namespace MainMikitan.API.Controllers {
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
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("customer-email-validation")]
        public async Task<IActionResult> CustomerEmailValidation(string email)
        {
            //???
            if(ModelState.IsValid)
            {
                var result = await _mediator.Send(new CustomerEmailSenderRegistrationCommand(email));
                if (result.HasError) return BadRequest(result);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        #endregion
    }
}
