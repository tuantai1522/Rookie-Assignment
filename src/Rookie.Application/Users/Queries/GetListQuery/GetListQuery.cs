using MediatR;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;

namespace Rookie.Application.Users.Queries.GetListQuery
{
    public class GetListQuery : IRequest<Result<PagedList<UserInfoVm>>>
    {
        public ApplicationUserParams ApplicationUserParams { get; set; }
    }
}