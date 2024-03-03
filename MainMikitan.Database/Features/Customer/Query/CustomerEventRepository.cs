using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Events;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Customer.Query;

public class CustomerEventRepository(MikDbContext mikDbContext) : ICustomerEventRepository
{
    public async Task<List<EventEntity>?> GetEvents(CancellationToken cancellationToken)
    {
        return await mikDbContext.Event.AsNoTracking().ToListAsync(cancellationToken);
    }
    
    public async Task<EventDetailsEntity?> GetEventDetailsByEventId(int eventId)
    {
        return await mikDbContext.EventDetails.FirstOrDefaultAsync(e => e.EventId == eventId);
    }
}