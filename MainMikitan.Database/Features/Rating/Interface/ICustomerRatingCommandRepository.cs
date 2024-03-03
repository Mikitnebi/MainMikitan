using MainMikitan.Domain.Models.Rating;

namespace MainMikitan.Database.Features.Rating.Interface;

public interface ICustomerRatingCommandRepository
{
    public Task SaveRating(CustomerRatingEntity ratingEntity);
    public Task<bool> SaveChangesAsync();
}