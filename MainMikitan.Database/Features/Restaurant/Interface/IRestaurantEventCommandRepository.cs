using MainMikitan.Domain.Models.Events;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantEventCommandRepository
{
    public Task<bool> CreateOrUpdateEvent(EventEntity eventData);
    public Task<bool> CreateOrUpdateEventDetails(EventDetailsEntity eventDetails);
    public Task<int> SaveChangesAsync();
}