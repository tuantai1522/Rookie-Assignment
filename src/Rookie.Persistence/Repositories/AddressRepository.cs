
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.ApplicationUserEntity;

namespace Rookie.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<ApplicationUserAddress>, IAddressRepository
    {
        private ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<ApplicationUserAddress>> GetAll(Expression<Func<ApplicationUserAddress, bool>> filter, string includeProperties = null)
        {
            IQueryable<ApplicationUserAddress> query = this._context.ApplicationUserAddresses;

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
    }
}