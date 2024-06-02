using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;

namespace Rookie.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetOne(Expression<Func<ApplicationUser, bool>> filter, string includeProperties = null);
        Task<PagedList<ApplicationUser>> GetAll(ApplicationUserParams applicationUserParams, string includeProperties = null);
        Task<IList<string>> GetRoles(ApplicationUser user);
        Task<IdentityResult> CreateUser(ApplicationUser user, string passWord);
        Task AddToRole(ApplicationUser user, string role);
        Task<bool> CheckPasswordValid(ApplicationUser user, string passWord);
        bool CheckEmailExisted(string email);
        bool CheckUserNameExisted(string userName);


    }
}