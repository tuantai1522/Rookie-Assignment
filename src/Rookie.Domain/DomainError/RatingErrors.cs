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
    }
}