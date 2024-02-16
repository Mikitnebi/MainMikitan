using MainMikitan.Application.Services.Filter;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Filter;
using MainMikitan.Domain.Responses.Filter;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Filter;

public class RestaurantByFilterByCustomerQuery(
    int customerId,
    string ipAddress,
    int page,
    int size,
    FilterRequestModel filterModel
) : IQuery<List<RestaurantInfoResponse>>
{
    public readonly int CustomerId = customerId;
    public readonly string IpAddress = ipAddress;
    public readonly int Page = page;
    public readonly int Size = size;
    public readonly FilterRequestModel FilterModel = filterModel;
}

public class RestaurantByFilterByCustomerQueryHandler(
    IFilterService filterService
) :
    ResponseMaker<List<RestaurantInfoResponse>>,
    IQueryHandler<RestaurantByFilterByCustomerQuery, List<RestaurantInfoResponse>>
{
    public async Task<ResponseModel<List<RestaurantInfoResponse>>> Handle(
        RestaurantByFilterByCustomerQuery request, CancellationToken cancellationToken)
    {
        var filterResponse = await filterService
            .Search(request.IpAddress, request.CustomerId, request.Size,
                request.Page, request.FilterModel.CustomerInterests, request.FilterModel.RegionId,
                request.FilterModel.BusinessTypeId, request.FilterModel.HasCoupe,
                request.FilterModel.HasTerrace, request.FilterModel.IsActiveInSomeHour,
                request.FilterModel.IsActiveKitchenInSomeHour, request.FilterModel.IsActiveMusicInSomeHour,
                request.FilterModel.PriceTypeId, request.FilterModel.RegionId,
                request.FilterModel.PriorityTypeId, cancellationToken: cancellationToken);
        return Success(filterResponse);
    }
}