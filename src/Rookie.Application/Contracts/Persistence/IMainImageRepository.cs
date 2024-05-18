using System.Linq.Expressions;
using Rookie.Domain.MainImageEntity;

namespace Rookie.Application.Contracts.Persistence
{
    public interface IMainImageRepository : IGenericRepository<MainImage>
    {
        Task<MainImage> GetOne(Expression<Func<MainImage, bool>> filter, string includeProperties = null);
        Task<bool> Update(MainImage entity);

    }
}