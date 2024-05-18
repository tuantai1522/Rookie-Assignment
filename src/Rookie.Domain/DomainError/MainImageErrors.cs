using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class MainImageErrors
    {
        public static readonly Error UpdateMainImageFailed = new Error(
            "Image.UpdateMainImageFailed",
            "Please provide me full information of main image");

        public static readonly Error NotFindProduct = new Error(
            "Image.NotFindProduct",
            "Can not find product");

        public static readonly Error NotFindImage = new Error(
            "Image.NotFindImage",
            "Can not find image");

        public static readonly Error InvalidImage = new Error(
            "Image.InvalidImage",
            "This image doesn't belong to this product");
    }
}