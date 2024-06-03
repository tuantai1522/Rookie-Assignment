using MediatR;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;

namespace Rookie.Application.Users.Queries.GetAddressByUserNameQuery
{
    public class GetAddressByUserNameQuery : IRequest<Result<ICollection<UserAddressVm>>>
    {
        public string UserName { get; set; }
    }
}