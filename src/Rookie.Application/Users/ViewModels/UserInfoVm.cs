using Rookie.Application.Orders.ViewModels;

namespace Rookie.Application.Users.ViewModels
{
    public class UserInfoVm
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<OrderVm> Orders { get; set; } = [];
    }
}