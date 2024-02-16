using MainMikitan.Cache.Cache;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Cache;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

namespace MainMikitan.Application.Services.CacheServices;

public class CustomerCacheService(
    IMemCacheManager memCacheManager,
    ICustomerInfoQueryRepository customerInfoQueryRepository,
    ICustomerInterestQueryRepository customerInterestQueryRepository,
    //ICustomerQueryRepository customerQueryRepository,
    IS3Adapter s3Adapter) : ICustomerCacheService
{
    public async Task<CustomerInfoEntity?> GetCustomerInfo(int customerId)
    {
        var cacheKey = CacheKeys.CustomerInfo(customerId);
        var cacheValue = memCacheManager.Get<CustomerInfoEntity>(cacheKey);
        if (cacheValue is not null) return cacheValue;
        var imageResponse = await customerInfoQueryRepository.Get(customerId);
        memCacheManager.Set(cacheKey, imageResponse);
        return imageResponse;
    }

    public async Task<List<CustomerInterestEntity>?> GetCustomerInterests(int customerId)
    {
        var cacheKey = CacheKeys.CustomerInterests(customerId);
        var cacheValue = memCacheManager.Get<List<CustomerInterestEntity>>(cacheKey);
        if (cacheValue is not null) return cacheValue;
        var interestResponse = await customerInterestQueryRepository.GetByCustomerId(customerId);
        memCacheManager.Set(cacheKey, interestResponse);
        return interestResponse;
    }

    public async Task<GetImageResponse?> GetCustomerProfileImage(int customerId)
    {
        var cacheKey = CacheKeys.CustomerProfileImageUrl(customerId);
        var cacheValue = memCacheManager.Get<GetImageResponse>(cacheKey);
        if (cacheValue is not null) return cacheValue;
        var imageResponse = await s3Adapter.GetCustomerProfileImage(customerId);
        memCacheManager.Set(cacheKey, imageResponse.Result);
        return imageResponse.Result;
    }

}