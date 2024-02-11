using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantStaffCommandRepository
{
    Task<bool> Add(RestaurantStaffEntity entity, CancellationToken cancellationToken = default);
    Task<bool> SaveChanges(CancellationToken cancellationToken = default);
}