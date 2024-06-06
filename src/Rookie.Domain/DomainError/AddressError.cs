using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class AddressError
    {
        public static readonly Error NotEnoughInfo = new Error(
            "Address.NotEnoughInfo",
            "Please provide me full information of user to add address");

        public static readonly Error NotFindUser = new Error(
            "Address.NotFindUser",
            "Can't find user");
    }
}