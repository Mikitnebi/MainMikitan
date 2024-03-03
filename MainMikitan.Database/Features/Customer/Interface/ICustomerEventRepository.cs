using MainMikitan.Domain.Models.Events;

namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerEventRepository
{
    public Task<List<EventEntity>?> GetEvents(CancellationToken cancellationToken);
    public Task<EventDetailsEntity?> GetEventDetailsByEventId(int eventId);
}