using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.InternalServicesAdapter.Validations;
using Microsoft.AspNetCore.Http;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.ExternalServicesAdapter.S3Adapter
{
    public class S3Adapter
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string BucketName;
        public S3Adapter(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
            BucketName = "samikitno";
        }
        public async Task<ResponseModel<bool>> CreateBucket()
        {
            var response = new ResponseModel<bool>();
            var bucketName = BucketName;
            var bucketExist = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExist)
            {
                var bucketRequest = new PutBucketRequest()
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };
                await _s3Client.PutBucketAsync(bucketRequest);
                response.Result = true;
            }
            response.ErrorType = ErrorType.S3.BucketAlreadyExisted;
            return response;
        }
        private async Task<ResponseModel<bool>> AddOrUpdateCustomerProfileImage(IFormFile file, int customerId)
        {
            var validationResponse = S3ConteinerValidation.ValidateImage(file);
            if (validationResponse.HasError) return validationResponse;

            var objectRequest = new PutObjectRequest()
            {
                BucketName = BucketName,
                Key = $"/Customer/{customerId}/Profile/{DateTime.Now:yyyy-MM-dd-HH-mm}",
                InputStream = file.OpenReadStream(),
            };
            var putResponse = await _s3Client.PutObjectAsync(objectRequest);
            
        }
    }
}
