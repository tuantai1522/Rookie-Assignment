using Rookie.Application.Contracts.Persistence;

namespace Rookie.Domain.ProductEntity
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> Update(Product entity);
    }
}