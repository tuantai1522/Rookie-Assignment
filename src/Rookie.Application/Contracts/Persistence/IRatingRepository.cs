using System.Linq.Expressions;
using Rookie.Domain.Common;
using Rookie.Domain.RatingEntity;

namespace Rookie.Application.Contracts.Persistence
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<PagedList<Rating>> GetRatingBasedOnProduct(Expression<Func<Rating, bool>> filter,
                                                        RatingParams ratingParams,
                                                        string includeProperties = null);
    }
}