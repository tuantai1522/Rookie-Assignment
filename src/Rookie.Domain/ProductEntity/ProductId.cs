using CSharpFunctionalExtensions;

namespace Rookie.Domain.ProductEntity;

public sealed class ProductId : ValueObject
{
  public Guid Value { get; }
  public ProductId(Guid value)
  {
    Value = value;
  }
  protected override IEnumerable<IComparable> GetEqualityComponents()
  {
    yield return Value;
  }
}