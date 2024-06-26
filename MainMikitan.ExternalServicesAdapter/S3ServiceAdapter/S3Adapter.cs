﻿﻿using Amazon.Runtime;
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
using Azure;
using MainMikitan.Domain.Templates;

namespace MainMikitan.ExternalServicesAdapter.S3ServiceAdapter
{
    public class S3Adapter : IS3Adapter
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        public S3Adapter(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
            _bucketName = "mikitanbucket";
        }
        public async Task<bool> CreateBucket()
        {
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
            }
            return true;
        }
        public async Task<ResponseModel<GetImageResponse>> GetCustomerProfileImage(int customerId)
        {
            var baseKey = $"Customer/{customerId}/Profile/";
            return await GetImage(baseKey);
        }

        public async Task<ResponseModel<bool>> DeleteCustomerProfileImage(int customerId)
        {
            var baseKey = $"Customer/{customerId}/Profile/";
            return await DeleteAllContentWithKey(baseKey);
        }
        public async Task<bool> AddOrUpdateCustomerProfileImage(IFormFile file, int customerId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImage(file);
            var baseKey = $"Customer/{customerId}/Profile/";
            await DeleteAllContentWithKey(baseKey);
            var putRequest = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = $"{baseKey}{DateTime.Now:yyyy-MM-dd-HH-mm}",
                InputStream = file.OpenReadStream(),
            };
            var putResponse = await _s3Client.PutObjectAsync(putRequest);
            CheckStatusCode(putRequest, putResponse);
            return true;
        }
        public async Task<bool> AddOrUpdateCompanyProfileImage(IFormFile file, int companyId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImage(file);
            var baseKey = $"Company/{companyId}/Profile/";
            await DeleteAllContentWithKey(baseKey);
            var putRequest = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = $"{baseKey}{DateTime.Now:yyyy-MM-dd-HH-mm}",
                InputStream = file.OpenReadStream(),
            };
            var putResponse = await _s3Client.PutObjectAsync(putRequest);
            CheckStatusCode(putRequest, putResponse);
            return true;
        }
        public async Task<ResponseModel<GetImageResponse>> GetCompanyProfileImage(int companyId)
        {
            var baseKey = $"Company/{companyId}/Profile/";
            return await GetImage(baseKey);
        }
        public async Task<bool> AddOrUpdateRestaurantProfileImage(IFormFile file, int restaurantId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImage(file);
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
            return true;
        }
        public async Task<ResponseModel<GetImageResponse>> GetRestaurantProfileImage(int restaurantId)
        {
            var baseKey = $"Restaurant/{restaurantId}/Profile/";
            return await GetImage(baseKey);
        }
        public async Task<bool> AddRestaurantEnvironmentImage(List<IFormFile> files, int restaurantId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImages(files);
            var baseKey = $"Restaurant/{restaurantId}/Environment/";
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

            return true;
        }
        public async Task<ResponseModel<GetImagesResponse>> GetRestaurantEnvironmentImages(int restaurantId)
        {
            var baseKey = $"Restaurant/{restaurantId}/Environment/";
            return await GetImages(baseKey);
        }
        public async Task<bool> DeleteRestaurantEnvironmentImage(List<DeleteImageRequest> files, int restaurantId)
        {
            var baseKey = $"Restaurant/{restaurantId}/Environment/";
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

            return true;
        }
        public async Task<bool> AddOrUpdateDishImage(IFormFile file, int restaurantId, int categoryId, int dishId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImage(file);
            var baseKey = $"Restaurant/{restaurantId}/Dish/{categoryId}/{dishId}";
            await DeleteAllContentWithKey(baseKey);
            var putRequest = new PutObjectRequest()
            {
                BucketName = _bucketName,
                Key = $"{baseKey}-{DateTime.Now:yyyy-MM-dd-HH-mm}",
                InputStream = file.OpenReadStream(),
            };
            var putResponse = await _s3Client.PutObjectAsync(putRequest);
            CheckStatusCode(putRequest, putResponse);
            return true;
        }
        public async Task<ResponseModel<GetImagesResponse>> GetRestaurantDishCategoryImages(int restaurantId, int categoryId)
        {
            var baseKey = $"Restaurant/{restaurantId}/Dish/{categoryId}/";
            return (await GetImages(baseKey));
        }
        public async Task<ResponseModel<GetImageResponse>> GetRestaurantDishCategoryImage(int restaurantId,
            int categoryId, int dishId)
        {
            var baseKey = $"Restaurant/{restaurantId}/Dish/{categoryId}/{dishId}";
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
            if (responseForListCount.KeyCount <= 0) return SuccessResponse(true);
            foreach (var deleteRequest in responseForListCount.S3Objects.Select(s3Object => new DeleteObjectRequest
                     {
                         BucketName = _bucketName,
                         Key = s3Object.Key
                     }))
            {
                var deleteResponse = await _s3Client.DeleteObjectAsync(deleteRequest);
                var checkResponse = CheckStatusCode<DeleteObjectResponse>(deleteRequest, deleteResponse);
                if (checkResponse.HasError)
                    return FailResponse<bool>(checkResponse.ErrorType!, checkResponse.ErrorMessage!);
            }
            return SuccessResponse(true);
        }
        private ResponseModel<T> CheckStatusCode<T>(AmazonWebServiceRequest request, T response) where T : AmazonWebServiceResponse
        {
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK) return SuccessResponse<T>(response);
            var jsonRequestModel = JsonSerializer.Serialize(request);
            var jsonResponseModel = JsonSerializer.Serialize(response);
            return FailResponse<T>(ErrorResponseType.S3.UnexpectedException,
                $"Error In S3 : {nameof(request)} : Request : {jsonRequestModel} : Response : {jsonResponseModel}");
        }
        private async Task<ResponseModel<GetImageResponse>> GetImage(string baseKey, bool restaurantDish = false)
        {
            var requestForList = new ListObjectsV2Request
            {
                BucketName = _bucketName,
                Prefix = baseKey
            };
            var responseForList = await _s3Client.ListObjectsV2Async(requestForList);
            CheckStatusCode(requestForList, responseForList);
            var imageResponse = responseForList.S3Objects.FirstOrDefault();
            if (imageResponse == null)
                return FailResponse<GetImageResponse>(
                    ErrorResponseType.S3.ImageNotFound,
                    $"Error In S3 : (BaseKey : {baseKey}, Restaurant Dish : {restaurantDish})");
            var imageUrl = _s3Client.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = _bucketName,
                Key = imageResponse.Key,
                Expires = DateTime.Now.AddHours(1)
            });
            if (!restaurantDish)
                return SuccessResponse(new GetImageResponse(
                    Url: imageUrl
                ));
            var key = imageResponse.Key;

            var indexOfDrop = key.LastIndexOf('/');
            var indexOfHipHen = key.IndexOf('-');
                
            if (indexOfDrop == -1 || indexOfHipHen == -1 || indexOfHipHen <= indexOfDrop)
                return FailResponse<GetImageResponse>(
                    ErrorResponseType.S3.ImageNotFound,
                    $"Error In S3 : (BaseKey : {baseKey}, Restaurant Dish : {restaurantDish})");
            var dishIdString = key.Substring(indexOfDrop + 1, indexOfHipHen - indexOfDrop - 1);

            return SuccessResponse(new GetImageResponse(
                Url: imageUrl,
                DishId: int.Parse(dishIdString)
            ));

        }
        private async Task<ResponseModel<GetImagesResponse>> GetImages(string baseKey, bool restaurantDish = false)
        {
            var requestForList = new ListObjectsV2Request
            {
                BucketName = _bucketName,
                Prefix = baseKey
            };
            var responseForList = await _s3Client.ListObjectsV2Async(requestForList);
            CheckStatusCode(requestForList, responseForList);
            var imageResponse = responseForList.S3Objects;
            // TODO : ამასაც ვუშველოთ
            if (imageResponse == null) 
                return FailResponse<GetImagesResponse>(
                    ErrorResponseType.S3.ImageNotFound,
                    $"Error In S3 : (BaseKey : {baseKey}, Restaurant Dish : {restaurantDish})");
            var imagesData = imageResponse.Select<S3Object,GetImageResponse>(o =>
            {
                var getUrlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = _bucketName,
                    Key = o.Key,
                    Expires = DateTime.Now.AddHours(1)
                };
                if (!restaurantDish)
                    return new GetImageResponse(
                        Url: _s3Client.GetPreSignedURL(getUrlRequest)
                    );
                var key = getUrlRequest.Key;

                var indexOfDrop = key.LastIndexOf('/');
                var indexOfHipHen = key.IndexOf('-');

                if (indexOfDrop == -1 || indexOfHipHen == -1 || indexOfHipHen <= indexOfDrop)
                    return null;
                var dishIdString = key.Substring(indexOfDrop + 1, indexOfHipHen - indexOfDrop - 1);

                return new GetImageResponse(
                    Url: _s3Client.GetPreSignedURL(getUrlRequest),
                    DishId: int.Parse(dishIdString)
                );
            });
            return SuccessResponse(new GetImagesResponse(imagesData.ToList()));
        }

        private ResponseModel<T> FailResponse<T>(string errorType, string errorMessage)
        {
            return new ResponseModel<T>()
            {
                ErrorType = errorType,
                ErrorMessage = errorMessage
            };
        }
        private ResponseModel<T> SuccessResponse<T>(T result)
        {
            return new ResponseModel<T>()
            {
                Result = result
            };
        }
    }
}