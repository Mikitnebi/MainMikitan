using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.S3Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainMikitan.InternalServiceAdapterService.Exceptions;

namespace MainMikitan.InternalServicesAdapter.Validations
{
    public class S3ConteinerValidation
    {
        public static bool ValidateImages(List<IFormFile> file)
        {
            foreach(IFormFile fileItem in file)
                ValidateImage(fileItem);
            return true;
        }
        public static bool ValidateImage(IFormFile file)
        {
            if (file == null)
                throw new MainMikitanException($"{ErrorResponseType.S3.FileIsEmpty} : {file?.FileName}");
            long maxSizeInKB = 200;
            long fileSizeInKB = file.Length / 1024;
            if (fileSizeInKB > maxSizeInKB)
                throw new MainMikitanException($"{ErrorResponseType.S3.FileSizeIsMore200Kb} :  {file?.FileName}");
            string[] allowedImageContentTypes = { "image/jpeg", "image/png", "image/bmp" };
            if (!allowedImageContentTypes.Contains(file.ContentType.ToLower()))
                throw new MainMikitanException($"{ErrorResponseType.S3.FileTypeIsNotImage} : {file?.FileName}");
            return true;
        }
    }
}
