using MainMikitan.Cache.Cache;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Cache;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

namespace MainMikitan.Application.Services.CacheServices;

public class CustomerCacheService : ICustomerCacheService
{
    private readonly IMemCacheManager _memCacheManager;
    private readonly ICustomerInfoRepository _customerInfoRepository;
    private readonly ICustomerInterestRepository _customerInterestRepository;
    private readonly IS3Adapter _s3Adapter;
    
    public CustomerCacheService(IMemCacheManager memCacheManager, ICustomerInfoRepository customerInfoRepository, ICustomerQueryRepository customerQueryRepository, ICustomerInterestRepository customerInterestRepository, ICustomerQueryRepository dustomerQueryRepository, IS3Adapter s3Adapter)
    {
        _memCacheManager = memCacheManager;
        _customerInfoRepository = customerInfoRepository;
        _customerInterestRepository = customerInterestRepository;
        _s3Adapter = s3Adapter;
    }

    public async Task<CustomerInfoEntity?> GetCustomerInfo(int customerId)
    {
        var cacheKey = CacheKeys.CustomerInfo(customerId);
        var cacheValue = _memCacheManager.Get<CustomerInfoEntity>(cacheKey);
        if (cacheValue is not null) return cacheValue;
        var imageResponse = await _customerInfoRepository.Get(customerId);
        _memCacheManager.Set(cacheKey, imageResponse);
        return imageResponse;
    }
    
    public async Task<List<CustomerInterestEntity>?> GetCustomerInterests(int customerId)
    {
        var cacheKey = CacheKeys.CustomerInterests(customerId);
        var cacheValue = _memCacheManager.Get<List<CustomerInterestEntity>>(cacheKey);
        if (cacheValue is not null) return cacheValue;
        var interestResponse = await _customerInterestRepository.Get(customerId);
        _memCacheManager.Set(cacheKey, interestResponse);
        return interestResponse;
    }

    public async Task<GetImageResponse?> GetCustomerProfileImage(int customerId)
    {
        var cacheKey = CacheKeys.CustomerProfileImageUrl(customerId);
        var cacheValue = _memCacheManager.Get<GetImageResponse>(cacheKey);
        if (cacheValue is not null) return cacheValue;
        var imageResponse = await _s3Adapter.GetCustomerProfileImage(customerId);
        _memCacheManager.Set(cacheKey, imageResponse);
        return imageResponse;
    }
}