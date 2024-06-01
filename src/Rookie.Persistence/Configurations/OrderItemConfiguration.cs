using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.OrderEntity;

namespace Rookie.Persistence.Configurations
{
    public class OrderItemConfiguration : BaseConfiguration<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Id)
                    .HasConversion(x => x.Value, x => new OrderItemId(x));


            //One orderItem belongs to one order
            builder.HasOne(p => p.Order)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.SetNull);

            //One orderItem belongs to one product
            builder.HasOne(p => p.Product)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}