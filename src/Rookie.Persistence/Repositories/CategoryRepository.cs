using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rookie.Domain.CategoryEntity;

namespace Rookie.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Category>> GetAll(Expression<Func<Category, bool>> filter, string includeProperties = null)
        {
            IQueryable<Category> query = this._context.Categories;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                //there are multiple includes
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<Category> GetOne(Expression<Func<Category, bool>> filter, string includeProperties = null)
        {
            IQueryable<Category> query = this._context.Categories;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                //there are multiple includes
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Category entity)
        {
            Category cate = await this._context.Categories.Where(x => x.Id.Equals(entity.Id)).FirstOrDefaultAsync();
            if (cate != null)
            {
                cate.Name = entity.Name;
                cate.Description = entity.Description;
                cate.UpdatedDate = DateTime.Now;

                await this._context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}