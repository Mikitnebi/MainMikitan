using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantInfoCommandRepository
{
    Task<bool> Create(RestaurantInfoEntity entity, CancellationToken cancellationToken = default);
    bool Update(RestaurantInfoEntity entity);
    Task<bool> SaveChanges(CancellationToken cancellationToken = default);
}