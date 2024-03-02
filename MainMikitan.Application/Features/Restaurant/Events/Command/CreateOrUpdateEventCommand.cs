using MainMikitan.Application.Services.AutoMapper;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Events;
using MainMikitan.Domain.Requests.RestaurantRequests.Event;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Restaurant.Events.Command;

public class CreateOrUpdateEventCommand(int restaurantId, CreateOrUpdateEventRequest eventData, CancellationToken cancellationToken) : ICommand<bool>
{
    public int RestaurantId { get; set; } = restaurantId;
    public CreateOrUpdateEventRequest EventData { get; set; } = eventData;
    public CancellationToken CancellationToken { get; set; } = cancellationToken;
}

public class CreateOrUpdateEventCommandHandler(IRestaurantEventCommandRepository eventCommandRepository,
    IMapperConfig mapperConfig) 
    : ResponseMaker, ICommandHandler<CreateOrUpdateEventCommand, bool>
{
    public Task<ResponseModel<bool>> Handle(CreateOrUpdateEventCommand request,
        CancellationToken cancellationToken)
    {
        var eventData = request.EventData;
        
        var eventEntity = new EventEntity();
        eventEntity = mapperConfig.Map(eventData, eventEntity);
        eventEntity.RestaurantId = request.RestaurantId;
        eventEntity.CreationDate = DateTime.Now;
        
        var commandResponse = eventCommandRepository.CreateOrUpdateEvent(eventEntity);

        if (commandResponse)
        {
            return Task.FromResult(Success());
        }

        return Task.FromResult(Fail("ივენთის შექმნა ვერ მოხერხდა"));
    }
}
