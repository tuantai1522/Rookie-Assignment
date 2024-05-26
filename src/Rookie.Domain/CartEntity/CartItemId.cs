using CSharpFunctionalExtensions;

namespace Rookie.Domain.CartEntity
{
    public sealed class CartItemId : ValueObject
    {
        public Guid Value { get; set; }

        public CartItemId()
        {

        }
        public CartItemId(Guid value) => Value = value;
        public CartItemId(string value)
        {
            if (Guid.TryParse(value, out var guid))
                Value = guid;
            else
                throw new ArgumentException("Invalid GUID format", nameof(value));
        }

        public static CartItemId Create(Guid value) => new CartItemId(value);
        public static CartItemId CreateUnique() => new CartItemId(Guid.NewGuid());
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}