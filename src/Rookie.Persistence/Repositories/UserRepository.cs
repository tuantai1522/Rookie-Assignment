using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Users.Extensions;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.Common;

namespace Rookie.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddToRole(ApplicationUser user, string RoleName) => await _userManager.AddToRoleAsync(user, RoleName);

        public bool CheckEmailExisted(string email) => !_userManager.Users.All(u => u.Email != email);
        public bool CheckUserNameExisted(string userName) => !_userManager.Users.All(u => u.UserName != userName);

        public async Task<bool> CheckPasswordValid(ApplicationUser user, string passWord) => await _userManager.CheckPasswordAsync(user, passWord);

        public async Task<IdentityResult> CreateUser(ApplicationUser user, string passWord) => await _userManager.CreateAsync(user, passWord);

        public async Task<PagedList<ApplicationUser>> GetAll(ApplicationUserParams applicationUserParams, string includeProperties = null)
        {
            var userList = await this._userManager.Users
                                .FilterByRolesAsync(_userManager, applicationUserParams.Role);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                //there are multiple includes
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    userList = userList.Include(includeProp);
                }
            }

            //Pagination
            var users = await PagedList<ApplicationUser>.ToPagedList(userList, applicationUserParams.PageNumber,
                                            applicationUserParams.PageSize);

            return users;
        }

        public async Task<ApplicationUser> GetOne(Expression<Func<ApplicationUser, bool>> filter, string includeProperties = null)
        {
            IQueryable<ApplicationUser> query = this._userManager.Users;

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

        public Task<IList<string>> GetRoles(ApplicationUser user) => this._userManager.GetRolesAsync(user);


    }
}