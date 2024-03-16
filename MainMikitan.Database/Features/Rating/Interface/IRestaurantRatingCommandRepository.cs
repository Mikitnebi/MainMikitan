using MainMikitan.Domain.Models.Rating;

namespace MainMikitan.Database.Features.Rating.Interface;

public interface IRestaurantRatingCommandRepository
{
    public Task SaveRating(RestaurantRatingEntity ratingEntity);
    public Task SaveReservationRatings(ReservationRatingsEntity reservationRatings);
    public Task<bool> SaveChangesAsync();
}