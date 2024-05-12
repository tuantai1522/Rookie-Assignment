using Rookie.Application.Contracts.Persistence;

namespace Rookie.Domain.CategoryEntity
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> Update(Category entity);
    }
}