namespace Rookie.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
    }
}