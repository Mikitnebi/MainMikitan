using MainMikitan.Domain.Models.Events;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantEventCommandRepository
{
    public bool CreateOrUpdateEvent(EventEntity eventData);
    public bool CreateOrUpdateEventDetails(EventDetailsEntity eventDetails);
}