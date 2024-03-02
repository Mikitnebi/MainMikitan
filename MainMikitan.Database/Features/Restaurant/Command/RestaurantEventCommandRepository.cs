using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Events;

namespace MainMikitan.Database.Features.Restaurant.Command;

public class RestaurantEventCommandRepository(MikDbContext mikDbContext) : IRestaurantEventCommandRepository
{
    public bool CreateOrUpdateEvent(EventEntity eventData)
    {
        var response = mikDbContext.Event.Update(eventData);

        return response.Members.Any();
    }

    public bool CreateOrUpdateEventDetails(EventDetailsEntity eventDetails)
    {
        var response = mikDbContext.EventDetails.Update(eventDetails);

        return response.Members.Any();
    }
}