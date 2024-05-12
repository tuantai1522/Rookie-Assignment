using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.CategoryEntity;

namespace Rookie.Persistence.Configurations
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
            );

            builder.HasData(GetSampleData());
        }
        private static IEnumerable<Category> GetSampleData()
        {
            yield return new Category()
            {
                Name = "Shirts",
                Description = "Shirts for men and women"
            };
            yield return new Category()
            {
                Name = "Pants",
                Description = "Pants for adults"

            };
            yield return new Category()
            {
                Name = "Shoes",
                Description = "Shoes all sizes"

            };
            yield return new Category()
            {
                Name = "Accessories",
                Description = "Accessories for women"

            };
        }
    }

}