using CSharpFunctionalExtensions;

namespace Rookie.Domain.CategoryEntity;
public sealed class CategoryId : ValueObject
{
  public Guid Value { get; set; }

  public CategoryId()
  {

  }
  public CategoryId(Guid value) => Value = value;
  public CategoryId(string value)
  {
    if (Guid.TryParse(value, out var guid))
      Value = guid;
    else
      throw new ArgumentException("Invalid GUID format", nameof(value));
  }

  public static CategoryId Create(Guid value) => new CategoryId(value);
  public static CategoryId CreateUnique() => new CategoryId(Guid.NewGuid());
  protected override IEnumerable<IComparable> GetEqualityComponents()
  {
    yield return Value;
  }

  public override string ToString() => Value.ToString();
}
