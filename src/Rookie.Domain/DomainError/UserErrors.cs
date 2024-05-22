using Rookie.Domain.Common;

namespace Rookie.Domain.DomainError
{
    public static class UserErrors
    {
        public static readonly Error NotEnoughInfo = new Error(
            "User.NotEnoughInfo",
            "Please provide me full information of user to login");

        public static readonly Error NotCorrectInfo = new Error(
            "User.NotCorrectInfo",
            "Username or password is not correct");
    }
}