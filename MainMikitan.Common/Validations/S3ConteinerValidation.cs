using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.InternalServicesAdapter.Validations
{
    public class S3ConteinerValidation
    {
        public static ResponseModel<bool> ValidateImage(IFormFile file)
        {
            var response = new ResponseModel<bool>();
            if (file == null)
            {
                response.ErrorType = ErrorType.S3.FileIsEmpty;
                return response;
            }
            long maxSizeInKB = 200;
            long fileSizeInKB = file.Length / 1024;
            if (fileSizeInKB > maxSizeInKB)
            {
                response.ErrorType = ErrorType.S3.FileSizeIsMore200KB;
                return response;
            }
            string[] allowedImageContentTypes = { "image/jpeg", "image/png", "image/bmp" };
            if (!allowedImageContentTypes.Contains(file.ContentType.ToLower()))
            {
                response.ErrorType = ErrorType.S3.FileTypeIsNotImage;
                return response;
            }
            response.Result = true;
            return response;
        }
    }
}
