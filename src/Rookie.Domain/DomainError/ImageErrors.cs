
using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class ImageErrors
    {
        public static readonly Error CreateImageInvalidData = new Error(
            "Image.CreateImageInvalidData",
            "Please provide me full information of image");
    }
}