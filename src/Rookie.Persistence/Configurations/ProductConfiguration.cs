using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.MainImageEntity;
using Rookie.Domain.ProductEntity;

namespace Rookie.Persistence.Configurations
{
    public class ProductConfiguration : BaseConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);

            builder.Property(e => e.Id)
                .HasConversion(
                    id => id.Value,
                    x => new ProductId(x));

            //One product belongs to one Category
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            //One Product has many Image
            builder.HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .IsRequired();


            //One Product has only One MainImage
            builder.HasOne(p => p.MainImage)
                   .WithOne(mi => mi.Product)
                   .HasForeignKey<MainImage>(mi => mi.ProductId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);


        }
    }
}