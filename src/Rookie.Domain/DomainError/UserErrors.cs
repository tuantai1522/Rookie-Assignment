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

        public static readonly Error EmailExisted = new Error(
            "User.EmailExisted",
            "This email has already existed");

        // Method to create custom error messages
        public static Error CreateCustomRegisterError(string customMessage)
        {
            return new Error("User.RegisterError", customMessage);
        }
    }
}