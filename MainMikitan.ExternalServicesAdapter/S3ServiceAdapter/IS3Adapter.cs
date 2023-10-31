using Amazon.Runtime;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.S3Requests;
using Microsoft.AspNetCore.Http;

namespace MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

public interface IS3Adapter
{
    Task<ResponseModel<bool>> CreateBucket();
    Task<ResponseModel<string>> GetCustomerProfileImage(int customerId);
    Task<ResponseModel<bool>> AddOrUpdateCustomerProfileImage(IFormFile file, int customerId);
    Task<ResponseModel<bool>> AddOrUpdateCompanyProfileImage(IFormFile file, int companyId);
    Task<ResponseModel<string>> GetCompanyProfileImage(int companyId);
    Task<ResponseModel<bool>> AddOrUpdateRestaurantProfileImage(IFormFile file, int restaurantId);
    Task<ResponseModel<string>> GetRestaurantProfileImage(int restaurantId);

    Task<ResponseModel<bool>> AddRestaurantEnvironmentImage(List<IFormFile> files, int restaurantId);
    Task<ResponseModel<List<string>>> GetRestaurantEnvironmnetImages(int restaurantId);
    Task<ResponseModel<bool>> DeleteRestaurantEnvironmentImage(List<DeleteImageRequest> files, int restaurantId);

    Task<ResponseModel<bool>> AddOrUpdateDishImage(IFormFile file, int restaurantId, int categoryId, int dishId);
    Task<ResponseModel<List<string>>> GetRestaurantDishCategoryImages(int restaurantId, int categoryId);
    Task<ResponseModel<string>> GetRestaurantdishCategoryImage(int restaurantId, int categoryId, int dishId);
    Task<ResponseModel<bool>> DeleteAllContentWithKey(string baseKey);
}