using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class RatingErrors
    {
        public static readonly Error CreateRatingInvalidData = new Error(
            "Rating.CreateRatingInvalidData",
            "Please provide me full information of rating");

        public static readonly Error NotFindUser = new Error(
            "Rating.NotFindUser",
            "Cant' find user");

        public static readonly Error NotFindProduct = new Error(
            "Rating.NotFindProduct",
            "Cant' find product");

        public static readonly Error NotFindOrderItem = new Error(
            "Rating.NotFindOrderItem",
            "Cant' find order item");
        public static readonly Error QueryRatingInvalidData = new Error(
            "Rating.QueryRatingInvalidData",
            "Info you provide to query ratings is not valid");

        public static readonly Error AlreadyRated = new Error(
            "Rating.AlreadyRated",
            "This order item is already rated");
    }
}