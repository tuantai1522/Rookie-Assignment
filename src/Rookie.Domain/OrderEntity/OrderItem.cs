using System.Text.Json.Serialization;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Domain.OrderEntity
{
    public class OrderItem : BaseEntity
    {
        public OrderItemId Id { get; set; } = new OrderItemId(Guid.NewGuid());
        public ProductId? ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }

        public OrderId? OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}