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

        public static readonly Error UserNameExisted = new Error(
            "User.UserNameExisted",
            "This user name has already existed");

        public static readonly Error QueryUserInvalidData = new Error(
            "User.QueryUserInvalidData",
            "Info you provide to query users is not valid");

        // Method to create custom error messages
        public static Error CreateCustomRegisterError(string customMessage)
        {
            return new Error("User.RegisterError", customMessage);
        }
    }
}