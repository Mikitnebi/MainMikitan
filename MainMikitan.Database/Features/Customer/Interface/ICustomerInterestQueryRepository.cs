using MainMikitan.Domain.Responses.Customer;

namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerInterestQueryRepository
{
    public Task<IEnumerable<CustomerInterestResponse>> GetAllCustomerInterest(int CustomerId);
}