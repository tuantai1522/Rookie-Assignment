using MediatR;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.Common;

namespace Rookie.Application.Orders.Queries.GetByIdQuery
{
    public class GetByIdQuery : IRequest<Result<OrderVm>>
    {
        public string UserName { get; set; }
        public string OrderId { get; set; }
    }
}