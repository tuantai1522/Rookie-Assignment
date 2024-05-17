using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.ImageEntity;

namespace Rookie.Persistence.Configurations
{
    public class ImageConfiguration : BaseConfiguration<Image>
    {
        public override void Configure(EntityTypeBuilder<Image> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);

            builder.Property(e => e.Id)
                .HasConversion(
                    id => id.Value,
                    x => new ImageId(x));

            //One image only belongs to One product - One product has many images
            builder.HasOne(p => p.Product)
                .WithMany(c => c.Images)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}