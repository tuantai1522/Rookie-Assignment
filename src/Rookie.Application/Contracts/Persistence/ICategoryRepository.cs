using System.Linq.Expressions;
using Rookie.Application.Categories.ViewModels;
using Rookie.Application.Contracts.Persistence;

namespace Rookie.Domain.CategoryEntity
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task Update(Category entity);
        Task<Category> GetOne(Expression<Func<Category, bool>>? filter, string? includeProperties = null);
        Task<IEnumerable<Category>> GetAll(Expression<Func<Category, bool>>? filter, string? includeProperties = null);

    }
}