using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Event;
using MainMikitan.Domain.Templates;
using Microsoft.IdentityModel.Tokens;
using IMapper = AutoMapper.IMapper;

namespace MainMikitan.Application.Features.Restaurant.Events.Query;

public class GetEventInfoQuery(int restaurantId) : IQuery<List<GetEventInfoResponse>>
{
    public int RestaurantId { get; set; } = restaurantId;
}

public class GetEventInfoQueryHandler(IRestaurantEventRepository eventRepository,
    IMapper mapper) : ResponseMaker<List<GetEventInfoResponse>>,
    IQueryHandler<GetEventInfoQuery, List<GetEventInfoResponse>>
{
    public async Task<ResponseModel<List<GetEventInfoResponse>>> Handle(GetEventInfoQuery query,
        CancellationToken cancellationToken)
    {
        var restaurantId = query.RestaurantId;
        var response = new List<GetEventInfoResponse>();

        var eventData = await eventRepository.GetEventsByRestaurantId(restaurantId);
        if (eventData.IsNullOrEmpty())
        {
            return Success(new List<GetEventInfoResponse>());
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