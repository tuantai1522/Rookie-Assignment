namespace Rookie.Domain.ProductEntity;
public partial struct ProductId : IEquatable<ProductId>
{
  public Guid Value { get; set; }

  public ProductId(Guid value) => this.Value = value;

  public bool Equals(ProductId other) => Value.Equals(other.Value);

}