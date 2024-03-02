using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Events;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Query;

public class RestaurantEventRepository(MikDbContext mikDbContext) : IRestaurantEventRepository
{
    public async Task<EventEntity?> GetEventById(int eventId)
    {
        return await mikDbContext.Event.FirstOrDefaultAsync(e => e.Id == eventId);
    }
    
    public async Task<EventEntity?> GetEventRestaurantIdByEventId(int restaurantId)
    {
        return await mikDbContext.Event.FirstOrDefaultAsync(e => e.RestaurantId == restaurantId);
    }
}