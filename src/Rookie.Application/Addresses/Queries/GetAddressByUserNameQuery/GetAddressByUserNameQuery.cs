using MediatR;
using Rookie.Application.Addresses.ViewModels;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Addresses.Queries.GetAddressByUserNameQuery
{
    public class GetAddressByUserNameQuery : IRequest<Result<IEnumerable<ApplicationUserAddressVm>>>
    {
        public string UserName { get; set; }
    }
}