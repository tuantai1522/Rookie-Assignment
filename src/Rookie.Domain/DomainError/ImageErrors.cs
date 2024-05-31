
using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class ImageErrors
    {
        public static readonly Error DeleteImageInvalidData = new Error(
            "Image.DeleteImageInvalidData",
            "Please provide me full information of image to delete");
        public static readonly Error CreateImageInvalidData = new Error(
            "Image.CreateImageInvalidData",
            "Please provide me full information of image");

        public static readonly Error UploadImageFailed = new Error(
            "Image.UploadImageFailed",
            "Error when uploading image. Please try again!");

        public static readonly Error NotFindProduct = new Error(
            "Image.NotFindProduct",
            "Can't find product to add image");

        public static readonly Error NotFindImage = new Error(
            "Image.NotFindImage",
            "Can't find image");

        public static readonly Error InvalidMainImage = new Error(
            "Image.InvalidMainImage",
            "This image is main image so can't delete");
    }
}