using MainMikitan.Domain.Models.Rating;

namespace MainMikitan.Database.Features.Rating.Interface;

public interface ICustomerRatingRepository
{
    public Task<List<CustomerRatingEntity>> GetCustomerRatings(int customerId, CancellationToken cancellationToken);
    public Task<List<CustomerRatingEntity>> GetAllActiveCustomersRatings(CancellationToken cancellationToken);
}