using CSharpFunctionalExtensions;

namespace Rookie.Domain.ImageEntity
{
    public class ImageId : ValueObject
    {
        public Guid Value { get; set; }

        public ImageId()
        {

        }
        public ImageId(Guid value) => Value = value;
        public ImageId(string value)
        {
            if (Guid.TryParse(value, out var guid))
                Value = guid;
            else
                throw new ArgumentException("Invalid GUID format", nameof(value));
        }

        public static ImageId Create(Guid value) => new ImageId(value);
        public static ImageId CreateUnique() => new ImageId(Guid.NewGuid());
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}