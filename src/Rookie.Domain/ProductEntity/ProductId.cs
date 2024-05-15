using CSharpFunctionalExtensions;

namespace Rookie.Domain.ProductEntity;

public sealed class ProductId : ValueObject
{
  public Guid Value { get; set; }

  public ProductId()
  {

  }
  public ProductId(Guid value) => Value = value;
  public ProductId(string value) => Value = new Guid(value);

  public static ProductId Create(Guid value) => new ProductId(value);
  public static ProductId CreateUnique() => new ProductId(Guid.NewGuid());
  protected override IEnumerable<IComparable> GetEqualityComponents()
  {
    yield return Value;
  }

  public override string ToString() => Value.ToString();
}