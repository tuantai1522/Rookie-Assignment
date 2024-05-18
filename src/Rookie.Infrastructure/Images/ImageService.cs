using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Images.ViewModels;

namespace Rookie.Infrastructure.Images
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        public ImageService(IConfiguration config)
        {
            var cloudinaryAccount = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(cloudinaryAccount);
        }
        public async Task<ImageVm> AddPhoto(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                        Folder = "EcommerceWebsite"
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }
            var newImageDto = new ImageVm();
            //success
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                newImageDto.Url = uploadResult.Url.AbsoluteUri;
                newImageDto.PublicId = uploadResult.PublicId;
            }
            //error
            return newImageDto;
        }

        public async Task<int> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams); // remove from cloud

            //success
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return 1;

            //error
            return 0;
        }
    }
}