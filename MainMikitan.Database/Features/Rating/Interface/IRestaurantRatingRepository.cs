using MainMikitan.Domain.Models.Rating;

namespace MainMikitan.Database.Features.Rating.Interface;

public interface IRestaurantRatingRepository
{
    public Task<List<RestaurantRatingEntity>> GetRestaurantRatings(int restaurantId, CancellationToken cancellationToken);
    public Task<List<RestaurantRatingEntity>> GetAllActiveCustomersRatings(CancellationToken cancellationToken);
}