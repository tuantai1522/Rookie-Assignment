using System.Linq.Expressions;
using Rookie.Domain.ApplicationUserEntity;

namespace Rookie.Application.Contracts.Persistence
{
    public interface IAddressRepository : IGenericRepository<ApplicationUserAddress>
    {
        Task<IEnumerable<ApplicationUserAddress>> GetAll(Expression<Func<ApplicationUserAddress, bool>> filter, string includeProperties = null);
    }
}