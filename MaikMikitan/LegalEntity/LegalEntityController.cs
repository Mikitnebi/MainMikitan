using MainMikitan.API.Controllers;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests.Admin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.LegalEntity
{
    public class LegalEntityRegistrationController(IMediator mediator) : MainController(mediator)
    {
    }
}
