using MainMikitan.Domain.Requests.S3Requests;
using MainMikitan.Domain.Responses.S3Response;
using Microsoft.AspNetCore.Http;

namespace MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

public interface IS3Adapter
{
    Task<bool> CreateBucket();
    Task<GetImageResponse?> GetCustomerProfileImage(int customerId);
    Task<bool> DeleteCustomerProfileImage(int customerId); 
    Task<bool> AddOrUpdateCustomerProfileImage(IFormFile file, int customerId);
    Task<bool> AddOrUpdateCompanyProfileImage(IFormFile file, int companyId);
    Task<GetImageResponse?> GetCompanyProfileImage(int companyId);
    Task<bool> AddOrUpdateRestaurantProfileImage(IFormFile file, int restaurantId);
    Task<GetImageResponse?> GetRestaurantProfileImage(int restaurantId);

    Task<bool> AddRestaurantEnvironmentImage(List<IFormFile> files, int restaurantId);
    Task<GetImagesResponse?> GetRestaurantEnvironmentImages(int restaurantId);
    Task<bool> DeleteRestaurantEnvironmentImage(List<DeleteImageRequest> files, int restaurantId);

    Task<bool> AddOrUpdateDishImage(IFormFile file, int restaurantId, int categoryId, int dishId);
    Task<GetImagesResponse?> GetRestaurantDishCategoryImages(int restaurantId, int categoryId);
    Task<GetImageResponse?> GetRestaurantDishCategoryImage(int restaurantId, int categoryId, int dishId);
    Task<bool> DeleteAllContentWithKey(string baseKey);
}