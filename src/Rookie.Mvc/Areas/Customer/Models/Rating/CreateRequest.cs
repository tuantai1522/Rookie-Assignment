using System.ComponentModel.DataAnnotations;

namespace Rookie.Mvc.Areas.Customer.Models.Rating
{
    public class CreateRequest
    {
        [Required(ErrorMessage = "Rate value is required")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
        public string OrderItemId { get; set; }
        public string OrderId { get; set; }
    }
}