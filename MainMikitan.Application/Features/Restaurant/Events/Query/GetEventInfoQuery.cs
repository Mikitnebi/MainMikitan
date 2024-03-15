using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Event;
using MainMikitan.Domain.Templates;
using Microsoft.IdentityModel.Tokens;
using IMapper = AutoMapper.IMapper;

namespace MainMikitan.Application.Features.Restaurant.Events.Query;

public class GetEventInfoQuery(int restaurantId, int userId, IEnumerable<int> permissionIds, string userRole) : IQuery<List<GetEventInfoResponse>>
{
    public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
    public string UserRole { get; set; } = userRole;
    public int StaffId { get; set; } = userId;
    public int RestaurantId { get; set; } = restaurantId;
}

public class GetEventInfoQueryHandler(IRestaurantEventRepository eventRepository,
    IPermissionService permissionService,
    IMapper mapper) : ResponseMaker<List<GetEventInfoResponse>>,
    IQueryHandler<GetEventInfoQuery, List<GetEventInfoResponse>>
{
    public async Task<ResponseModel<List<GetEventInfoResponse>>> Handle(GetEventInfoQuery query,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(query.StaffId, query.PermissionIds, query.UserRole,
                cancellationToken, query.RestaurantId, 3))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var restaurantId = query.RestaurantId;
        var response = new List<GetEventInfoResponse>();

        var eventData = await eventRepository.GetEventsByRestaurantId(restaurantId);
        if (eventData.IsNullOrEmpty())
        {
            return Success([]);
        }

        var activeEventData = eventData!.Where(ed => ed.EndDate < DateTime.Now).OrderBy(ed => ed.StartDate);

        for (var i = 0; i < activeEventData.Count(); i++)
        {
            var eventDetails = await eventRepository.GetEventDetailsByEventId(activeEventData.ElementAt(i).Id);
            if (eventDetails is null)
            {
                continue;
            }
            var eventEntity = mapper.Map<GetEventInfoResponse>(eventDetails);
            eventEntity = mapper.Map<GetEventInfoResponse>(activeEventData);
            eventEntity.EventId = eventDetails.EventId;
            eventEntity.EventDetailsId = eventDetails.Id;
            
            response.Add(eventEntity);
        }

        return Success(response);
    }
}