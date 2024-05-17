using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class ProductErrors
    {
        public static readonly Error CreateProductInvalidData = new Error(
            "Product.CreateProductInvalidData",
            "Please provide me full information of product");
        public static readonly Error NotProvidingId = new Error(
            "Product.NotProvidingId",
            "Please provide me id of this product");
        public static readonly Error NotFindProduct = new Error(
            "Product.NotFindProduct",
            "Can not find product");
        public static readonly Error NotFindCategory = new Error(
            "Product.NotFindCategory",
            "Can not find category of current product");
        public static readonly Error UpdateProductInvalidData = new Error(
            "Product.UpdateProductInvalidData",
            "Info you provide to update product is not valid");
        public static readonly Error QueryProductInvalidData = new Error(
            "Product.QueryProductInvalidData",
            "Info you provide to query products is not valid");
        public static readonly Error DeleteProductInvalidData = new Error(
            "Product.DeleteProductInvalidData",
            "Info you provide to delete product is not valid");

        public static readonly Error UploadImageFailed = new Error(
            "Product.UploadImageFailed",
            "Upload image failed, please try again!");

        public static readonly Error DeleteImageFailed = new Error(
            "Product.DeleteImageFailed",
            "Delete image failed, please try again!");
    }
}