using Rookie.Domain.Common;

namespace Rookie.Domain.OrderEntity
{
    public class OrderParams : PaginationParams
    {
        public decimal MinTotal { get; set; } = 1;
        public decimal MaxTotal { get; set; } = 20000;

        public string OrderBy { get; set; } = "priceAsc";
    }
}