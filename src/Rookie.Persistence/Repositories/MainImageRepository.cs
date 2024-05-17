using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.MainImageEntity;

namespace Rookie.Persistence.Repositories
{
    public class MainImageRepository : GenericRepository<MainImage>, IMainImageRepository
    {
        private ApplicationDbContext _context;
        public MainImageRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<MainImage> GetOne(Expression<Func<MainImage, bool>> filter, string includeProperties = null)
        {
            IQueryable<MainImage> query = this._context.MainImages;

            if (filter != null)
                query = query.Where(filter);

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
    }
}