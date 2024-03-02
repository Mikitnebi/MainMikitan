using MainMikitan.Domain.Models.Events;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantEventRepository
{
    public Task<EventEntity?> GetEventById(int eventId);
    public Task<List<EventEntity>?> GetEventsByRestaurantId(int restaurantId);
    public Task<EventDetailsEntity?> GetEventDetailsByEventId(int eventId);
}