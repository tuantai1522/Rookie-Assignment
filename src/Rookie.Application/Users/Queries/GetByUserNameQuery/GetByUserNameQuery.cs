using MediatR;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Users.Queries.GetByUserNameQuery
{
    public class GetByUserNameQuery : IRequest<Result<UserLoginVm>>
    {
        public string UserName { get; set; }
    }
}