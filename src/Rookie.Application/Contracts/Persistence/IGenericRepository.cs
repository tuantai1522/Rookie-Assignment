namespace Rookie.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> Get(string? includeProperties = null);
        Task<T> GetById(Guid id, string? includeProperties = null);
        Task<T> Add(T entity);
        Task<T> Delete(T entity);
    }
}