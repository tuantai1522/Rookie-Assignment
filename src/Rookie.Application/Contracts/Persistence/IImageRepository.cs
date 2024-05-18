using System.Linq.Expressions;
using Rookie.Domain.ImageEntity;

namespace Rookie.Application.Contracts.Persistence
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        Task<Image> GetOne(Expression<Func<Image, bool>> filter, string includeProperties = null);
        Task<IEnumerable<Image>> GetAll(Expression<Func<Image, bool>> filter, string includeProperties = null);
    }
}