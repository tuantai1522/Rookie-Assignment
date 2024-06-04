using Rookie.Mvc.Areas.Customer.Models.Order;

namespace Rookie.Mvc.ViewModels
{
    public class VnPaymentRequest
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public double Total { get; set; }

        public DateTime CreatedDate { get; set; }
        public Address Address { get; set; }
    }
}