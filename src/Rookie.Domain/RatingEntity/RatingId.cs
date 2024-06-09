using CSharpFunctionalExtensions;

namespace Rookie.Domain.RatingEntity
{
    public sealed class RatingId : ValueObject
    {
        public Guid Value { get; set; }

        public RatingId()
        {

        }
        public RatingId(Guid value) => Value = value;
        public RatingId(string value) => Value = new Guid(value);

        public static RatingId Create(Guid value) => new RatingId(value);
        public static RatingId CreateUnique() => new RatingId(Guid.NewGuid());
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}