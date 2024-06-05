using Rookie.Domain.OrderEntity;

namespace Rookie.Application.Orders.Extensions
{
    public static class OrderExtension
    {
        public static IQueryable<Order> Sort(this IQueryable<Order> query, string OrderBy)
        {
            query = OrderBy switch
            {
                "priceAsc" => query.OrderBy(p => p.SubTotal + p.DeliveryFee),
                _ => query.OrderByDescending(p => p.SubTotal + p.DeliveryFee)
            };

            return query;
        }

        public static IQueryable<Order> FilterTotal(this IQueryable<Order> query,
                                           decimal MinTotal,
                                           decimal MaxTotal)
        {
            return query.Where(p => (p.SubTotal + p.DeliveryFee) >= MinTotal && (p.SubTotal + p.DeliveryFee) <= MaxTotal);
        }
    }
}