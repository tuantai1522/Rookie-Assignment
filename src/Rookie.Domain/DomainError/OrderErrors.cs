
using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class OrderErrors
    {
        public static readonly Error CreateInvalidData = new Error(
            "Order.CreateInvalidData",
            "Please provide me full information of order");

        public static readonly Error NotFindUser = new Error(
            "Order.NotFindUser",
            "Can't find user to order");

        public static readonly Error CartEmpty = new Error(
            "Order.CartEmpty",
            "Cart is empty. Please add more to order");
    }
}