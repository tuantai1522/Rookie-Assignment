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
                   .IsRequired();

        }
        private static IEnumerable<Product> GetSampleData()
        {
            IEnumerable<Category> cates = CategoryConfiguration.GetSampleData();
            //Category 1
            yield return new Product()
            {
                ProductName = "Plaid cropped shirt",
                Price = 49.90M,
                Description = "Shirt for men",
                CategoryId = new CategoryId(Guid.Parse("1F76E0B5-214F-4744-A592-E02AAA188494"))
            };
            yield return new Product()
            {
                ProductName = "Watercolor print shirt",
                Price = 59.90M,
                Description = "Shirt for men",
                CategoryId = new CategoryId(Guid.Parse("1F76E0B5-214F-4744-A592-E02AAA188494"))
            };
            yield return new Product()
            {
                ProductName = "Abstract print shirt",
                Price = 49.90M,
                Description = "Shirt for men and women",
                CategoryId = new CategoryId(Guid.Parse("1F76E0B5-214F-4744-A592-E02AAA188494"))
            };
            yield return new Product()
            {
                ProductName = "Linen shirt",
                Price = 49.90M,
                Description = "Shirt for kids",
                CategoryId = new CategoryId(Guid.Parse("1F76E0B5-214F-4744-A592-E02AAA188494"))
            };
            yield return new Product()
            {
                ProductName = "Tie-dye print shirt",
                Price = 69.90M,
                Description = "Shirt for men",
                CategoryId = new CategoryId(Guid.Parse("1F76E0B5-214F-4744-A592-E02AAA188494"))
            };

            //Category 2
            yield return new Product()
            {
                ProductName = "Haggar Men's Cool 18",
                Price = 30.48M,
                Description = "Pants for men",
                CategoryId = new CategoryId(Guid.Parse("55201118-A7DD-45C2-906B-E8515EBFC494")),
            };
            yield return new Product()
            {
                ProductName = "HUDSON Men's Blake Slim Straight",
                Price = 67.49M,
                Description = "Pants for men",
                CategoryId = new CategoryId(Guid.Parse("55201118-A7DD-45C2-906B-E8515EBFC494")),

            };
            yield return new Product()
            {
                ProductName = "Ergodyne Men's Standard Lightweight Base Layer",
                Price = 22.49M,
                Description = "Pants for men",
                CategoryId = new CategoryId(Guid.Parse("55201118-A7DD-45C2-906B-E8515EBFC494")),

            };
            yield return new Product()
            {
                ProductName = "LAPCOFR unisex adult",
                Price = 66.37M,
                Description = "Pants for men",
                CategoryId = new CategoryId(Guid.Parse("55201118-A7DD-45C2-906B-E8515EBFC494")),

            };
            yield return new Product()
            {
                ProductName = "Essentials Men's Classic-Fit Wrinkle-Resistant",
                Price = 8.70M,
                Description = "Pants for men",
                CategoryId = new CategoryId(Guid.Parse("55201118-A7DD-45C2-906B-E8515EBFC494")),

            };

            //Category 3
            yield return new Product()
            {
                ProductName = "Amazon Essentials Women's Loafer Flat",
                Price = 22.80M,
                Description = "Shoes for women",
                CategoryId = new CategoryId(Guid.Parse("ED4B2B06-EE12-44F3-BFC9-54096597C2E9")),
            };
            yield return new Product()
            {
                ProductName = "Ringside Diablo Wrestling Boxing Shoes",
                Price = 67.12M,
                Description = "Shoes for adults",
                CategoryId = new CategoryId(Guid.Parse("ED4B2B06-EE12-44F3-BFC9-54096597C2E9")),

            };
            yield return new Product()
            {
                ProductName = "MOZO Men's Slip Resistant Chef Natural Shoes",
                Price = 107.48M,
                Description = "Shoes for men",
                CategoryId = new CategoryId(Guid.Parse("ED4B2B06-EE12-44F3-BFC9-54096597C2E9")),

            };
            yield return new Product()
            {
                ProductName = "Merrell Men's Moab 3 Tactical Industrial Shoe",
                Price = 119.95M,
                Description = "Shoes for men",
                CategoryId = new CategoryId(Guid.Parse("ED4B2B06-EE12-44F3-BFC9-54096597C2E9")),

            };
            yield return new Product()
            {
                ProductName = "Skechers Men's Afterburn M. Fit",
                Price = 47.99M,
                Description = "Shoes for men",
                CategoryId = new CategoryId(Guid.Parse("ED4B2B06-EE12-44F3-BFC9-54096597C2E9")),

            };

            //Category 4
            yield return new Product()
            {
                ProductName = "Carve Designs Women's Dundee Crushable",
                Price = 47.00M,
                Description = "Hat for women",
                CategoryId = new CategoryId(Guid.Parse("DCEBD3E6-0BAA-4DA2-9374-D08E3D421F09")),
            };
            yield return new Product()
            {
                ProductName = "Eddie Bauer Women's Casual Fashion Leather Belt",
                Price = 32.71M,
                Description = "Belt for women",
                CategoryId = new CategoryId(Guid.Parse("DCEBD3E6-0BAA-4DA2-9374-D08E3D421F09")),
            };
            yield return new Product()
            {
                ProductName = "French Connection Flex Sunglasses For Women",
                Price = 11.95M,
                Description = "Sunglasses for women",
                CategoryId = new CategoryId(Guid.Parse("DCEBD3E6-0BAA-4DA2-9374-D08E3D421F09")),
            };
            yield return new Product()
            {
                ProductName = "K. Bell Women's Fun Sport",
                Price = 8.95M,
                Description = "Socks for kids",
                CategoryId = new CategoryId(Guid.Parse("DCEBD3E6-0BAA-4DA2-9374-D08E3D421F09")),
            };
            yield return new Product()
            {
                ProductName = "Betsey Johnson Skull Earrings",
                Price = 26.99M,
                Description = "Earrings for kids",
                CategoryId = new CategoryId(Guid.Parse("DCEBD3E6-0BAA-4DA2-9374-D08E3D421F09")),
            };
        }
    }
}