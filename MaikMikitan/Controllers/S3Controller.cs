using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class S3Controller : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;
        public S3Controller(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }
        [HttpPost(Name = "Upload")]
        public async Task<IActionResult> UploadObject(IFormFile formFile)
        {

            var bucketName = "samikitno";
            var bucketExist = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExist)
            {
                var bucketRequest = new PutBucketRequest()
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };
                await _s3Client.PutBucketAsync(bucketRequest);
            }

            var objectRequest = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = $"/5/menuPicture/-{formFile.FileName}",
                InputStream = formFile.OpenReadStream(),
            };
            var response = await _s3Client.PutObjectAsync(objectRequest);
            return Ok(formFile);
        }
    }
}
