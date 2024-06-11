
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.Common;
using Rookie.Domain.RatingEntity;

namespace Rookie.Persistence.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private ApplicationDbContext _context;
        public RatingRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Rating> GetOne(Expression<Func<Rating, bool>> filter, string includeProperties = null)
        {
            IQueryable<Rating> query = this._context.Ratings;

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

            //Lấy object Product
            query = query
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.Product);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PagedList<Rating>> GetRatingBasedOnProduct(Expression<Func<Rating, bool>> filter, RatingParams ratingParams, string includeProperties = null)
        {
            IQueryable<Rating> query = this._context.Ratings;

            if (filter != null)
                query = query.Where(filter);

            var ratingList = query.AsQueryable();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                //there are multiple includes
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ratingList = ratingList.Include(includeProp);
                }
            }

            //Lấy object Product
            ratingList = ratingList
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.Product);

            //Pagination
            var ratings = await PagedList<Rating>.ToPagedList(ratingList,
                                                            ratingParams.PageNumber,
                                                            ratingParams.PageSize);

            return ratings;
        }
    }
}