using MainMikitan.Domain.Models.Rating;

namespace MainMikitan.Database.Features.Rating.Interface;

public interface IRestaurantRatingCommandRepository
{
    public Task SaveRating(RestaurantRatingEntity ratingEntity);
    public Task<bool> SaveChangesAsync();
}