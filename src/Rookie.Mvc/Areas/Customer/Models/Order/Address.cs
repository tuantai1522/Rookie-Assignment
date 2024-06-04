using System.ComponentModel.DataAnnotations;

namespace Rookie.Mvc.Areas.Customer.Models.Order
{
    public class Address
    {
        [Required(ErrorMessage = "Value is required.")]
        public string Value { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "ZipCode is required.")]
        public string ZipCode { get; set; }
    }
}