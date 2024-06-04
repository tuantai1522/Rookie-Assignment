using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.ApplicationUserEntity;

namespace Rookie.Application.Users.ViewModels
{
    public class UserLoginVm
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ApplicationUserAddress> ApplicationUserAddresses { get; set; } = [];
        public ICollection<OrderVm> Orders { get; set; } = [];
        public string Token { get; set; }
    }
}