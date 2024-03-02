using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantInfoQueryRepository
{
    Task<List<RestaurantInfoEntity>> GetRestaurantsByCustom(
        int regionId, int? businessTypeId = null, bool? hasCoupe = null, bool? hasTerrace = null,
        int? isActiveInSomeHour = null, int? isActiveKitchenInSomeHour = null, int? isActiveMusicInSomeHour = null,
        int? priceTypeId = null, int? rate = null, CancellationToken cancellationToken = default);

    Task<RestaurantInfoEntity?> GetByRestaurantId(int restaurantId, CancellationToken cancellationToken = default);
}