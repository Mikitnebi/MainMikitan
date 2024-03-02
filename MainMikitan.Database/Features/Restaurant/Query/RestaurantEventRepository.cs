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
    
    public async Task<List<EventEntity>?> GetEventsByRestaurantId(int restaurantId)
    {
        return await mikDbContext.Event.Where(e => e.RestaurantId == restaurantId).AsNoTracking().ToListAsync();
    }
    
    public async Task<EventDetailsEntity?> GetEventDetailsByEventId(int eventId)
    {
        return await mikDbContext.EventDetails.FirstOrDefaultAsync(e => e.EventId == eventId);
    }
}