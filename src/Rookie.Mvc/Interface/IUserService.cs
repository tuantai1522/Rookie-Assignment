using Rookie.Application.Users.ViewModels;

namespace Rookie.Mvc.Interface
{
    public interface IUserService
    {
        string IsUserLoggedIn();
        UserLoginVm GetUserInfo(string info);
    }
}