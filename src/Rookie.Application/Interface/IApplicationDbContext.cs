using Microsoft.EntityFrameworkCore;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Interface
{
    public interface IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}