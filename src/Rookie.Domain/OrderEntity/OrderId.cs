using CSharpFunctionalExtensions;

namespace Rookie.Domain.OrderEntity
{
    public sealed class OrderId : ValueObject
    {
        public Guid Value { get; set; }

        public OrderId()
        {

        }
        public OrderId(Guid value) => Value = value;
        public OrderId(string value) => Value = new Guid(value);

        public static OrderId Create(Guid value) => new OrderId(value);
        public static OrderId CreateUnique() => new OrderId(Guid.NewGuid());
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}