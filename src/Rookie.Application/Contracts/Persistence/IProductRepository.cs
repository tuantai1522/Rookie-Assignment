using System.Linq.Expressions;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetOne(Expression<Func<Product, bool>> filter, string includeProperties = null);
        Task<PagedList<Product>> GetAll(ProductParams productParams, string includeProperties = null);
        Task<bool> Update(Product entity);
    }
}