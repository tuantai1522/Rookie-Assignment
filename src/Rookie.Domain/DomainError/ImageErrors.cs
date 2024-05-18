
using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class ImageErrors
    {
        public static readonly Error CreateImageInvalidData = new Error(
            "Image.CreateImageInvalidData",
            "Please provide me full information of image");

        public static readonly Error UploadImageFailed = new Error(
            "Image.UploadImageFailed",
            "Error when uploading image. Please try again!");

        public static readonly Error NotFindProduct = new Error(
            "Image.NotFindProduct",
            "Can't find product to add image");
    }
}