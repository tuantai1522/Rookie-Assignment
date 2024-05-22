using MediatR;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Users.Queries.GetByEmailQuery
{
    public class GetByEmailQuery : IRequest<Result<UserLoginVm>>
    {
        public string UserName { get; set; }
    }
}