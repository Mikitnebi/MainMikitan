using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Events;

namespace MainMikitan.Database.Features.Restaurant.Command;

public class RestaurantEventCommandRepository(MikDbContext mikDbContext) : IRestaurantEventCommandRepository
{
    public async Task<bool> CreateOrUpdateEvent(EventEntity eventData)
    {
        var response = eventData.Id <= 0 ? await mikDbContext.Event.AddAsync(eventData) : mikDbContext.Event.Update(eventData);

        return response.Members.Any();
    }

    public async Task<bool> CreateOrUpdateEventDetails(EventDetailsEntity eventDetails)
    {
        var response = eventDetails.Id <= 0 ? await mikDbContext.EventDetails.AddAsync(eventDetails) : mikDbContext.EventDetails.Update(eventDetails);

        return response.Members.Any();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await mikDbContext.SaveChangesAsync();
    }
}