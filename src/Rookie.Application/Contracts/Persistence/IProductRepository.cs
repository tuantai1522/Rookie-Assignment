using System.Linq.Expressions;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.Common;

namespace Rookie.Domain.ProductEntity
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetOne(Expression<Func<Product, bool>> filter, string includeProperties = null);
        Task<PagedList<Product>> GetAll(ProductParams productParams, string includeProperties = null);
        Task<bool> Update(Product entity);
    }
}