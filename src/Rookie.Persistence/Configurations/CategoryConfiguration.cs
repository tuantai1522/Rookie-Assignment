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

            builder.Property(x => x.Id)
                    .HasConversion(x => x.Value, x => new CategoryId(x));

            // builder.HasData(GetSampleData());

        }
        public static IEnumerable<Category> GetSampleData()
        {
            yield return new Category()
            {
                Name = "Laptop",
                Description = "Laptop for students and business man"
            };
            yield return new Category()
            {
                Name = "Phone",
                Description = "Phone for calling or texting"
            };
            yield return new Category()
            {
                Name = "Tablet",
                Description = "Tablet to surf website"
            };
            yield return new Category()
            {
                Name = "Player",
                Description = "Listen to music or watch video"
            };
            yield return new Category()
            {
                Name = "Smartwatch",
                Description = "Watch for tracking sports"
            };
        }
    }

}