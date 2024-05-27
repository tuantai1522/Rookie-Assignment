using MediatR;
using Rookie.Application.Carts.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Carts.Queries.GetCartByUserNameQuery
{
    public class GetCartByUserNameQuery : IRequest<Result<CartVm>>
    {
        public string UserName { get; set; }
    }
}