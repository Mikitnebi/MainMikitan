using AutoMapper;
using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Events;
using MainMikitan.Domain.Requests.RestaurantRequests.Event;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Restaurant.Events.Command;


public class CreateOrUpdateEventDetailsCommand(CreateOrUpdateEventDetailsRequest eventData, int restaurantId, IEnumerable<int> permissionIds, string userRole, CancellationToken cancellationToken) : ICommand<bool>
{
    public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
    public string UserRole { get; set; } = userRole;
    public int RestaurantId { get; set; } = restaurantId;
    public CreateOrUpdateEventDetailsRequest EventData { get; set; } = eventData;
    public CancellationToken CancellationToken { get; set; } = cancellationToken;
}

public class CreateOrUpdateEventDetailsCommandHandler(IRestaurantEventCommandRepository eventCommandRepository,
    IRestaurantEventRepository eventRepository,
    IRestaurantInfoQueryRepository restaurantInfoQueryRepository,
    IPermissionService permissionService,
    IMapper mapper) 
    : ResponseMaker, ICommandHandler<CreateOrUpdateEventDetailsCommand, bool>
{
    public async Task<ResponseModel<bool>> Handle(CreateOrUpdateEventDetailsCommand request,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.RestaurantId, request.PermissionIds, request.UserRole,
            cancellationToken))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var eventDetailsData = request.EventData;
        var eventData = eventDetailsData.Id > 0 ? await eventRepository.GetEventById(eventDetailsData.Id) : null;

        if (eventDetailsData.Id > 0 && eventData is null)
        {
            return Fail(ErrorResponseType.EventDetails.ErrorEventNotFound);
        }

        if ((string.IsNullOrEmpty(eventDetailsData.NameGeo) || string.IsNullOrEmpty(eventDetailsData.NameEng)) || (string.IsNullOrEmpty(eventDetailsData.DescriptionGeo) || string.IsNullOrEmpty(eventDetailsData.DescriptionEng)))
        {
            return Fail(ErrorResponseType.EventDetails.ErrorEventNameAndDescription);
        }
        
        var eventEntity = mapper.Map<EventDetailsEntity>(eventDetailsData);
        
        if (!eventDetailsData.TakeManagersRegistrationAddress && (string.IsNullOrEmpty(eventDetailsData.EventAddressGeo) || string.IsNullOrEmpty(eventDetailsData.EventAddressEng)))
        {
            return Fail(ErrorResponseType.EventDetails.ErrorEventAddress);
        }
        else
        {
            var restaurantInfo = await restaurantInfoQueryRepository.GetByRestaurantId(request.RestaurantId, request.CancellationToken);

            if (restaurantInfo is null)
            {
                return Fail(ErrorResponseType.RestaurantInfo.ErrorRestaurantNotFound);
            }
            
            eventEntity.EventAddressGeo = restaurantInfo.Address;
            eventEntity.EventAddressEng = restaurantInfo.AddressEng;
        }

        if (eventDetailsData.HasMusician && string.IsNullOrEmpty(eventDetailsData.Musician))
        {
            return Fail(ErrorResponseType.EventDetails.ErrorMusiciansNotProvided);
        }
        if (eventDetailsData.HasPresenter && string.IsNullOrEmpty(eventDetailsData.Presenter))
        {
            return Fail(ErrorResponseType.EventDetails.ErrorPresenterNotProvided);
        }
        if (eventDetailsData.HasDancer && string.IsNullOrEmpty(eventDetailsData.Dancer))
        {
            return Fail(ErrorResponseType.EventDetails.ErrorDancerNotProvided);
        }
        
        eventEntity.CreationDate = DateTime.Now;
        
        await eventCommandRepository.CreateOrUpdateEventDetails(eventEntity);
        var result = await eventCommandRepository.SaveChangesAsync();

        return result > 0 ? Success() : Fail(ErrorResponseType.EventDetails.ErrorCreate);
    }
}