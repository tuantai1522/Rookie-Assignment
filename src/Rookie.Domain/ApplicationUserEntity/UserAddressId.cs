using CSharpFunctionalExtensions;

namespace Rookie.Domain.ApplicationUserEntity
{
    public class UserAddressId : ValueObject
    {
        public Guid Value { get; set; }

        public UserAddressId()
        {

        }
        public UserAddressId(Guid value) => Value = value;
        public UserAddressId(string value)
        {
            if (Guid.TryParse(value, out var guid))
                Value = guid;
            else
                throw new ArgumentException("Invalid GUID format", nameof(value));
        }

        public static UserAddressId Create(Guid value) => new UserAddressId(value);
        public static UserAddressId CreateUnique() => new UserAddressId(Guid.NewGuid());
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}