using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands;

public class FillRestaurantInfoIntroCommand : IRequest<ResponseModel<bool>>
{
    public FillRestaurantInfoIntroCommand(RestaurantRegistrationInfoIntroRequest request, int id)
    {
        
    }
    
    public Task Handle()
    {

        return Task.CompletedTask;
    }
}
