using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class CategoryErrors
    {
        public static readonly Error CreateCategoryInvalidData = new Error(
            "Category.CreateCategoryInvalidData",
            "Please provide me full information of category");

        public static readonly Error NotProvidingId = new Error(
            "Category.NotProvidingId",
            "Please provide me id of this category");

        public static readonly Error NotFindCategory = new Error(
            "Category.NotFindCategory",
            "Can not find id of this category");

        public static readonly Error UpdateCategoryInvalidData = new Error(
            "Category.UpdateCategoryInvalidData",
            "Info you provide to update category is not valid");

        public static readonly Error DeleteCategoryInvalidData = new Error(
            "Category.DeleteCategoryInvalidData",
            "Info you provide to delete category is not valid");

        public static readonly Error GetCategoryByIdInvalidData = new Error(
            "Category.GetCategoryByIdInvalidData",
            "Info you provide to query category is not valid");
    }
}