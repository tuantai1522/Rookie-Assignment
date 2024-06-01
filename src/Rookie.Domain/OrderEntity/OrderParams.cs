using Rookie.Domain.Common;

namespace Rookie.Domain.OrderEntity
{
    public class OrderParams : PaginationParams
    {
        public DateTime DateStart { get; set; } = DateTime.MinValue;
        public DateTime DateEnd { get; set; } = DateTime.Now;

        public decimal MinTotal { get; set; } = 0;
        public decimal MaxTotal { get; set; }
    }
}