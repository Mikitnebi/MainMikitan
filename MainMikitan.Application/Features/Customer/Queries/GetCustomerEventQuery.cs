using AutoMapper;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Event;
using MainMikitan.Domain.Templates;
using Microsoft.IdentityModel.Tokens;

namespace MainMikitan.Application.Features.Customer.Queries;

public class GetCustomerEventQuery(int page, int size) : IQuery<List<GetEventInfoResponse>>
{
    public int Page { get; set; } = page;
    public int Size { get; set; } = size;
}

public class GetCustomerEventQueryHandler(ICustomerEventRepository eventRepository,
    IMapper mapper) : ResponseMaker<List<GetEventInfoResponse>>,
    IQueryHandler<GetCustomerEventQuery, List<GetEventInfoResponse>>
{
    public async Task<ResponseModel<List<GetEventInfoResponse>>> Handle(GetCustomerEventQuery query,
        CancellationToken cancellationToken)
    {
        var response = new List<GetEventInfoResponse>();
        var events = await eventRepository.GetEvents(cancellationToken);
        if (events.IsNullOrEmpty())
        {
            return Success([]);
        }

        var activeEventData = events!.Where(ed => ed.EndDate < DateTime.Now).OrderBy(ed => ed.StartDate);

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

        return Success(response.Skip((query.Page - 1) * query.Size).Take(query.Size).ToList());
    }
}