
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.ImageEntity;

namespace Rookie.Persistence.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<Image> GetOne(Expression<Func<Image, bool>> filter, string includeProperties = null)
        {
            IQueryable<Image> query = this._context.Images;

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