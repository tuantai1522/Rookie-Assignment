using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rookie.Domain.Common;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(e => e.UpdatedDate)
            .HasDefaultValue(DateTime.UtcNow);

    }
}