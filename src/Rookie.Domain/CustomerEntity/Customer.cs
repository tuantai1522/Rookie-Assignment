using Rookie.Domain.Common;

namespace Rookie.Domain.CustomerEntity
{
    public class Customer : BaseEntity
    {
        public CustomerId Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}