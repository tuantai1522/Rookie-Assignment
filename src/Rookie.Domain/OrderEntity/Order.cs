using System.Text.Json.Serialization;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;

namespace Rookie.Domain.OrderEntity
{
    public class Order : BaseEntity
    {
        public OrderId Id { get; set; } = new OrderId(Guid.NewGuid());
        public string? UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser? ApplicationUser { get; set; }
        public Address? ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; } = [];
    }
}