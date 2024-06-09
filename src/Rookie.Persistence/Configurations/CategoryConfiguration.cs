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
    }
}