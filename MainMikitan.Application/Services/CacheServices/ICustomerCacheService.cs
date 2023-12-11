using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Responses.S3Response;

namespace MainMikitan.Application.Services.CacheServices;

public interface ICustomerCacheService
{
    Task<GetImageResponse?> GetCustomerProfileImage(int customerId);
    Task<List<CustomerInterestEntity>?> GetCustomerInterests(int customerId);
    Task<CustomerInfoEntity?> GetCustomerInfo(int customerId);
}