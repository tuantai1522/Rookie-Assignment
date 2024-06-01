using CSharpFunctionalExtensions;

namespace Rookie.Domain.OrderEntity
{
    public sealed class OrderItemId : ValueObject
    {
        public Guid Value { get; set; }

        public OrderItemId()
        {

        }
        public OrderItemId(Guid value) => Value = value;
        public OrderItemId(string value) => Value = new Guid(value);

        public static OrderItemId Create(Guid value) => new OrderItemId(value);
        public static OrderItemId CreateUnique() => new OrderItemId(Guid.NewGuid());
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}