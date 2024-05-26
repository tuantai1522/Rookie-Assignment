using MediatR;
using Rookie.Domain.Common;

namespace Rookie.Application.Carts.Commands.ChangeCartQuantityCommand
{
    public class ChangeCartQuantityCommand : IRequest<Result<Dictionary<string, int>>>
    {
        public string UserName { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}