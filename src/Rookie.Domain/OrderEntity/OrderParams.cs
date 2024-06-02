using Rookie.Domain.Common;

namespace Rookie.Domain.OrderEntity
{
    public class OrderParams : PaginationParams
    {
        public DateTime DateStart { get; set; } = DateTime.MinValue;
        public DateTime DateEnd { get; set; } = DateTime.Now;

        public decimal MinTotal { get; set; } = 1;
        public decimal MaxTotal { get; set; } = 20000;

        public string OrderBy { get; set; } = "priceAsc";
    }
}