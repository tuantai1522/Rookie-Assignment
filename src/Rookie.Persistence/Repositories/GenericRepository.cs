using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Persistence;

namespace Rookie.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            this.dbSet.Add(entity);
            this._db.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.dbSet.Remove(entity);
            this._db.SaveChanges();
        }
    }
}