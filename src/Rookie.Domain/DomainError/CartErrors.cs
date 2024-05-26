using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class CartErrors
    {
        public static readonly Error ChangeCartQuantityInvalidData = new Error(
            "Cart.ChangeCartQuantityInvalidData",
            "Please provide me full information when adding product to cart");

        public static readonly Error CanNotFindUser = new Error(
            "Cart.CanNotFindUser",
            "Can not find user to add cart");

        public static readonly Error CanNotFindProduct = new Error(
            "Cart.CanNotFindProduct",
            "Can not find product to add cart");
    }
}