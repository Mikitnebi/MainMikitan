using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.S3Requests;
using MainMikitan.Domain.Responses.S3Response;
using Microsoft.AspNetCore.Http;

namespace MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

public interface IS3Adapter
{
    Task<bool> CreateBucket();
    Task<ResponseModel<GetImageResponse>> GetCustomerProfileImage(int customerId);
    Task<ResponseModel<bool>> DeleteCustomerProfileImage(int customerId); 
    Task<bool> AddOrUpdateCustomerProfileImage(IFormFile file, int customerId);
    Task<bool> AddOrUpdateCompanyProfileImage(IFormFile file, int companyId);
    Task<ResponseModel<GetImageResponse>> GetCompanyProfileImage(int companyId);
    Task<bool> AddOrUpdateRestaurantProfileImage(IFormFile file, int restaurantId);
    Task<ResponseModel<GetImageResponse>> GetRestaurantProfileImage(int restaurantId);

    Task<bool> AddRestaurantEnvironmentImage(List<IFormFile> files, int restaurantId);
    Task<ResponseModel<GetImagesResponse>> GetRestaurantEnvironmentImages(int restaurantId);
    Task<bool> DeleteRestaurantEnvironmentImage(List<DeleteImageRequest> files, int restaurantId);

    Task<bool> AddOrUpdateDishImage(IFormFile file, int restaurantId, int categoryId, int dishId);
    Task<ResponseModel<GetImagesResponse>> GetRestaurantDishCategoryImages(int restaurantId, int categoryId);
    Task<ResponseModel<GetImageResponse>> GetRestaurantDishCategoryImage(int restaurantId, int categoryId, int dishId);
    Task<ResponseModel<bool>> DeleteAllContentWithKey(string baseKey);
}