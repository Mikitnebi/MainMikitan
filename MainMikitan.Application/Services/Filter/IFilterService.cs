using MainMikitan.Domain.Responses.Filter;

namespace MainMikitan.Application.Services.Filter;

public interface IFilterService
{
    Task<List<RestaurantInfoResponse>> Search(string ipAddress, int customerId, int size, int page = 1,
        List<int>? customerInterests = null,
        int? regionId = null, int? businessTypeId = null, bool? hasCoupe = null, bool? hasTerrace = null,
        int? isActiveInSomeHour = null, int? isActiveKitchenInSomeHour = null, int? isActiveMusicInSomeHour = null,
        int? priceTypeId = null, int? rate = null, int? priorityTypeId = null,
        CancellationToken cancellationToken = default);
}