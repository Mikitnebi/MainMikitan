using MainMikitan.Application.Services.Filter;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Filter;
using MainMikitan.Domain.Templates;
using Microsoft.EntityFrameworkCore.Query;

namespace MainMikitan.Application.Features.Filter;

public class RestaurantByDefaultByCustomerQuery(int customerId, string ipAddress, int page, int size) : IQuery<List<RestaurantInfoResponse>>
{
    public readonly int CustomerId = customerId;
    public readonly string IpAddress = ipAddress;
    public readonly int Page = page;
    public readonly int Size = size;
}

public class RestaurantByDefaultByCustomerQueryHandler(
    IFilterService filterService
    ) :
    ResponseMaker<List<RestaurantInfoResponse>>,
    IQueryHandler<RestaurantByDefaultByCustomerQuery, List<RestaurantInfoResponse>>
{
    public async Task<ResponseModel<List<RestaurantInfoResponse>>> Handle(RestaurantByDefaultByCustomerQuery request, CancellationToken cancellationToken)
    {
        var filterResponse = await filterService
            .Search(request.IpAddress, request.CustomerId, request.Size, 
                request.Page, cancellationToken: cancellationToken);
        return Success(filterResponse);
    }
}