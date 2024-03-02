using MainMikitan.Domain.Models.Events;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantEventRepository
{
    public Task<EventEntity?> GetEventById(int eventId);
    public Task<EventEntity?> GetEventRestaurantIdByEventId(int restaurantId);
}