namespace Rookie.Domain.CategoryEntity;
public partial struct CategoryId : IEquatable<CategoryId>
{
    public Guid Value { get; set; }
    public CategoryId(Guid value) => this.Value = value;

    public bool Equals(CategoryId other) => Value.Equals(other.Value);

}
