namespace Rookie.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> Get();
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task<T> Delete(T entity);
    }
}