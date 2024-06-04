using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Orders.Extensions;
using Rookie.Domain.Common;
using Rookie.Domain.OrderEntity;

namespace Rookie.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<PagedList<Order>> GetAll(OrderParams orderParams, string includeProperties = null)
        {
            var orderList = _context.Orders
                                .FilterDate(orderParams.DateStart, orderParams.DateEnd)
                                .FilterTotal(orderParams.MinTotal, orderParams.MaxTotal)
                                .Sort(orderParams.OrderBy)
                                .AsQueryable();


            if (!string.IsNullOrEmpty(includeProperties))
            {
                //there are multiple includes
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    orderList = orderList.Include(includeProp);
                }
            }

            //Lấy object Product
            orderList = orderList
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product);


            //Pagination
            var orders = await PagedList<Order>.ToPagedList(orderList, orderParams.PageNumber,
                                            orderParams.PageSize);

            return orders;
        }

        public async Task<PagedList<Order>> GetListById(Expression<Func<Order, bool>> filter, OrderParams orderParams, string includeProperties = null)
        {
            var orderList = _context.Orders
                    .Where(filter)
                    .FilterDate(orderParams.DateStart, orderParams.DateEnd)
                    .FilterTotal(orderParams.MinTotal, orderParams.MaxTotal)
                    .Sort(orderParams.OrderBy)
                    .AsQueryable();



            if (!string.IsNullOrEmpty(includeProperties))
            {
                //there are multiple includes
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    orderList = orderList.Include(includeProp);
                }
            }

            //Lấy object Product
            orderList = orderList
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product);

            //Pagination
            var orders = await PagedList<Order>.ToPagedList(orderList, orderParams.PageNumber,
                                            orderParams.PageSize);

            return orders;
        }

        public async Task<Order> GetOne(Expression<Func<Order, bool>> filter, string includeProperties = null)
        {
            IQueryable<Order> query = this._context.Orders;

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                //there are multiple includes
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}