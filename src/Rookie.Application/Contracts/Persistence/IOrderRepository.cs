using System.Linq.Expressions;
using Rookie.Domain.Common;
using Rookie.Domain.OrderEntity;

namespace Rookie.Application.Contracts.Persistence
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetOne(Expression<Func<Order, bool>> filter, string includeProperties = null);
        Task<PagedList<Order>> GetAll(OrderParams orderParams, string includeProperties = null);
        Task<PagedList<Order>> GetListById(Expression<Func<Order, bool>> filter, OrderParams orderParams, string includeProperties = null);
        Task<bool> CheckOrderItemExists(OrderItemId orderItemId);
    }
}