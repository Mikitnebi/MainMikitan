using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantStaffQueryRepository
{
    Task<RestaurantStaffEntity?> GetActive(string hashUserName, string hashPassword,
        CancellationToken cancellationToken = default);
}