using AutoMapper;
using MainMikitan.Application.Services.AutoMapper;
using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Events;
using MainMikitan.Domain.Requests.RestaurantRequests.Event;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Restaurant.Events.Command;

public class CreateOrUpdateEventCommand(int restaurantId, int staffId,CreateOrUpdateEventRequest eventData, IEnumerable<int> permissionIds, string userRole) : ICommand<bool>
{
    public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
    public string UserRole { get; set; } = userRole;
    public int RestaurantId { get; set; } = restaurantId;
    public int StaffId { get; set; } = staffId;
    public CreateOrUpdateEventRequest EventData { get; set; } = eventData;
}

public class CreateOrUpdateEventCommandHandler(IRestaurantEventCommandRepository eventCommandRepository,
    IPermissionService permissionService,
    IMapper mapper) 
    : ResponseMaker, ICommandHandler<CreateOrUpdateEventCommand, bool>
{
    public async Task<ResponseModel<bool>> Handle(CreateOrUpdateEventCommand request,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.StaffId, request.PermissionIds, request.UserRole,
                cancellationToken, request.RestaurantId, 3))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var eventData = request.EventData;
        
        var eventEntity = mapper.Map<EventEntity>(eventData);
        eventEntity.RestaurantId = request.RestaurantId;
        eventEntity.CreationDate = DateTime.Now;
        
        var commandResponse = await eventCommandRepository.CreateOrUpdateEvent(eventEntity);
        var result = await eventCommandRepository.SaveChangesAsync();

        return result > 0 ? Success() : Fail(ErrorResponseType.Event.ErrorCreate);
    }
}
