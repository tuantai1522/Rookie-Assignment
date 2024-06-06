using MediatR;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Addresses.Queries.GetAddressByUserNameQuery
{
    public class GetAddressByUserNameQuery : IRequest<Result<ICollection<UserAddressVm>>>
    {
        public string UserName { get; set; }
    }
}