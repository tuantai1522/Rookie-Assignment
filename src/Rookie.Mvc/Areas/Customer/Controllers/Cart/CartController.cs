using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rookie.Mvc.Areas.Customer.Controllers.Cart
{
    [Area("Customer")]
    public class CartController : Controller
    {
        public CartController()
        {
        }

        [HttpGet]
        [Authorize(Policy = "RequireCustomerRole")]
        public IActionResult Index()
        {
            return View();
        }
    }
}