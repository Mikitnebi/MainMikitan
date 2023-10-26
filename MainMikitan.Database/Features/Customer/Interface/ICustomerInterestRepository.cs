namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerInterestRepository
{
    Task<bool> Delete(int customerId);
    Task<bool> Add(List<int> interestIds, int customerId);
    Task<bool> SaveChanges();

}