using System.Linq.Expressions;
using Rookie.Application.Contracts.Persistence;

namespace Rookie.Domain.CategoryEntity
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> Update(Category entity);
        Task<Category> GetOne(Expression<Func<Category, bool>> filter, string includeProperties = null);
        Task<IEnumerable<Category>> GetAll(Expression<Func<Category, bool>> filter, string includeProperties = null);

    }
}