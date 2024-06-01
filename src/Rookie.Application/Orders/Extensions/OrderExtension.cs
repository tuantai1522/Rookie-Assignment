using Rookie.Domain.OrderEntity;

namespace Rookie.Application.Orders.Extensions
{
    public static class OrderExtension
    {
        public static IQueryable<Order> FilterDate(this IQueryable<Order> query,
                                                   DateTime DateStart,
                                                   DateTime DateEnd)
        {
            query = query.Where(p => p.OrderDate >= DateStart && p.OrderDate <= DateEnd);
            return query;
        }

        public static IQueryable<Order> FilterTotal(this IQueryable<Order> query,
                                           decimal MinTotal,
                                           decimal? MaxTotal)
        {
            if (MaxTotal.HasValue)
                return query.Where(p => p.GetTotal() >= MinTotal);

            query = query.Where(p => p.GetTotal() >= MinTotal && p.GetTotal() <= MaxTotal);

            return query;
        }
    }
}