using System.Linq.Expressions;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.CategoryEntity;

namespace Rookie.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> Update(Category entity);
        Task<Category> GetOne(Expression<Func<Category, bool>> filter, string includeProperties = null);
        Task<IEnumerable<Category>> GetAll(Expression<Func<Category, bool>> filter, string includeProperties = null);
    }
}