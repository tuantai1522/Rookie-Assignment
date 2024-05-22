using MediatR;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Users.Commands.LoginCommand
{
    public class LoginCommand : IRequest<Result<UserLoginVm>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}