using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.S3Requests;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.InternalServiceAdapterService.Exceptions;
using MainMikitan.InternalServicesAdapter.Validations;
using Microsoft.AspNetCore.Http;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ErrorType = MainMikitan.Domain.ErrorType;

namespace MainMikitan.ExternalServicesAdapter.S3ServiceAdapter
{
    public class S3Adapter : IS3Adapter
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        public S3Adapter(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
            _bucketName = "samikitno";
        }
        public async Task<ResponseModel<bool>> CreateBucket()
        {
            var response = new ResponseModel<bool>();
            var bucketName = _bucketName;
            var bucketExist = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExist)
            {
                var bucketRequest = new PutBucketRequest()
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };
                var bucketResponse = await _s3Client.PutBucketAsync(bucketRequest);
                CheckStatusCode(bucketRequest, bucketResponse);
                response.Result = true;
            }
            response.ErrorType = ErrorType.S3.BucketAlreadyExisted;
            return response;
        }
        public async Task<ResponseModel<string>> GetCustomerProfileImage(int customerId)
        {
            var baseKey = $"/Customer/{customerId}/Profile/";
            return await GetImage(baseKey);
        }
        public async Task<ResponseModel<bool>> AddOrUpdateCustomerProfileImage(IFormFile file, int customerId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImage(file);
            if (validationResponse.HasError) return validationResponse;
            var baseKey = $"/Customer/{customerId}/Profile/";
            await DeleteAllContentWithKey(baseKey);
            var putRequest = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = $"{baseKey}{DateTime.Now:yyyy-MM-dd-HH-mm}",
                InputStream = file.OpenReadStream(),
            };
            var putResponse = await _s3Client.PutObjectAsync(putRequest);
            CheckStatusCode(putRequest, putResponse);
            return new ResponseModel<bool>
            {
                Result = true
            };
        }
        public async Task<ResponseModel<bool>> AddOrUpdateCompanyProfileImage(IFormFile file, int companyId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImage(file);
            if (validationResponse.HasError) return validationResponse;
            var baseKey = $"/Company/{companyId}/Profile/";
            await DeleteAllContentWithKey(baseKey);
            var putRequest = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = $"{baseKey}{DateTime.Now:yyyy-MM-dd-HH-mm}",
                InputStream = file.OpenReadStream(),
            };
            var putResponse = await _s3Client.PutObjectAsync(putRequest);
            CheckStatusCode(putRequest, putResponse);
            return new ResponseModel<bool>
            {
                Result = true
            };
        }
        public async Task<ResponseModel<string>> GetCompanyProfileImage(int companyId)
        {
            var baseKey = $"/Company/{companyId}/Profile/";
            return await GetImage(baseKey);
        }
        public async Task<ResponseModel<bool>> AddOrUpdateRestaurantProfileImage(IFormFile file, int restaurantId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImage(file);
            if (validationResponse.HasError) return validationResponse;
            var baseKey = $"/Restaurant/{restaurantId}/Profile/";
            await DeleteAllContentWithKey(baseKey);
            var putRequest = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = $"{baseKey}{DateTime.Now:yyyy-MM-dd-HH-mm}",
                InputStream = file.OpenReadStream(),
            };
            var putResponse = await _s3Client.PutObjectAsync(putRequest);
            CheckStatusCode(putRequest, putResponse);
            return new ResponseModel<bool>
            {
                Result = true
            };
        }
        public async Task<ResponseModel<string>> GetRestaurantProfileImage(int restaurantId)
        {
            var baseKey = $"/Resutaurant/{restaurantId}/Profile/";
            return await GetImage(baseKey);
        }
        public async Task<ResponseModel<bool>> AddRestaurantEnvironmentImage(List<IFormFile> files, int restaurantId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImages(files);
            if (validationResponse.HasError) return validationResponse;
            var baseKey = $"/Restaurant/{restaurantId}/Environment/";
            int counter = 0;
            foreach (var file in files)
            {
                counter++;
                var putRequest = new PutObjectRequest()
                {
                    BucketName = _bucketName,
                    Key = $"{baseKey}{counter}-{DateTime.Now:yyyy-MM-dd-HH-mm}",
                    InputStream = file.OpenReadStream(),
                };
                var putResponse = await _s3Client.PutObjectAsync(putRequest);
                CheckStatusCode(putRequest, putResponse);
            }
            return new ResponseModel<bool>
            {
                Result = true
            };
        }
        public async Task<ResponseModel<List<string>>> GetRestaurantEnvironmnetImages(int restaurantId)
        {
            var baseKey = $"/Restaurant/{restaurantId}/Environment/";
            return await GetImages(baseKey);
        }
        public async Task<ResponseModel<bool>> DeleteRestaurantEnvironmentImage(List<DeleteImageRequest> files, int restaurantId)
        {
            var baseKey = $"/Restaurant/{restaurantId}/Environment/";
            foreach (var file in files)
            {
                var deleteRequest = new DeleteObjectRequest()
                {
                    BucketName = _bucketName,
                    Key = $"{baseKey}{file.Key}"
                };
                var deleteResponse = await _s3Client.DeleteObjectAsync(deleteRequest);
                CheckStatusCode(deleteRequest, deleteResponse);
            }
            return new ResponseModel<bool>
            {
                Result = true
            };
        }
        public async Task<ResponseModel<bool>> AddOrUpdateDishImage(IFormFile file, int restaurantId, int categoryId, int dishId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImage(file);
            if (validationResponse.HasError) return validationResponse;
            var baseKey = $"/Restaurant/{restaurantId}/Dish/{categoryId}/{dishId}";
            await DeleteAllContentWithKey(baseKey);
            var putRequest = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = $"{baseKey}-{DateTime.Now:yyyy-MM-dd-HH-mm}",
                InputStream = file.OpenReadStream(),
            };
            var putResponse = await _s3Client.PutObjectAsync(putRequest);
            CheckStatusCode(putRequest, putResponse);
            return new ResponseModel<bool>
            {
                Result = true
            };
        }
        public async Task<ResponseModel<List<string>>> GetRestaurantDishCategoryImages(int restaurantId, int categoryId)
        {
            var baseKey = $"/Restaurant/{restaurantId}/Dish/{categoryId}/";
            return await GetImages(baseKey);
        }
        public async Task<ResponseModel<string>> GetRestaurantdishCategoryImage(int restaurantId, int categoryId, int dishId)
        {
            var baseKey = $"/Restaurant/{restaurantId}/Dish/{categoryId}/{dishId}";
            return await GetImage(baseKey);
        }
        public async Task<ResponseModel<bool>> DeleteAllContentWithKey (string baseKey)
        {
            var requestForListCount = new ListObjectsV2Request
            {
                BucketName = _bucketName,
                Prefix = baseKey
            };
            var responseForListCount = await _s3Client.ListObjectsV2Async(requestForListCount);
            if (responseForListCount.KeyCount > 0)
            {
                foreach (var s3Object in responseForListCount.S3Objects)
                {
                    var deleteRequest = new DeleteObjectRequest
                    {
                        BucketName = _bucketName,
                        Key = s3Object.Key
                    };

                   var deleteReponse = await _s3Client.DeleteObjectAsync(deleteRequest);
                    CheckStatusCode(deleteRequest, deleteReponse);
                }
            }
            return new ResponseModel<bool>
            {
                Result = true
            }; 
        }
        private void CheckStatusCode(AmazonWebServiceRequest request, AmazonWebServiceResponse response)
        {
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK) {
                var jsonRequestModel = JsonSerializer.Serialize(request);
                var jsonReqsponseModel = JsonSerializer.Serialize(response);
                throw new MainMikitanException(message: $"Error In S3 : {nameof(request)} : Request : {jsonRequestModel} : Reqspnse : {jsonReqsponseModel}");
             }
        }
        private async Task<ResponseModel<string>> GetImage(string baseKey)
        {
            var requestForList = new ListObjectsV2Request
            {
                BucketName = _bucketName,
                Prefix = baseKey
            };
            var responseForList = await _s3Client.ListObjectsV2Async(requestForList);
            CheckStatusCode(requestForList, responseForList);
            var imageResponse = responseForList.S3Objects.FirstOrDefault();
            if (imageResponse == null) return new ResponseModel<string> { Result = null };
            var imageUrl = _s3Client.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = _bucketName,
                Key = imageResponse.Key,
                Expires = DateTime.Now.AddHours(1)
            });
            return new ResponseModel<string>
            {
                Result = imageUrl
            };
        }
        private async Task<ResponseModel<List<string>>> GetImages(string baseKey)
        {
            var requestForList = new ListObjectsV2Request
            {
                BucketName = _bucketName,
                Prefix = baseKey
            };
            var responseForList = await _s3Client.ListObjectsV2Async(requestForList);
            CheckStatusCode(requestForList, responseForList);
            var imageResponse = responseForList.S3Objects;
            if (imageResponse == null) return new ResponseModel<List<string>> { Result = null };
            var urls = imageResponse.Select(o =>
            {
                var getUrlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = _bucketName,
                    Key = o.Key,
                    Expires = DateTime.Now.AddHours(1)
                };
                return _s3Client.GetPreSignedURL(getUrlRequest);
            });
            return new ResponseModel<List<string>>
            {
                Result = urls.ToList()
            };
        }
    }
}
