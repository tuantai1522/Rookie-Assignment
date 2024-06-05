using MediatR;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Users.Commands.RegisterCommand
{
    public class RegisterCommand : IRequest<Result<UserRegisterVm>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}