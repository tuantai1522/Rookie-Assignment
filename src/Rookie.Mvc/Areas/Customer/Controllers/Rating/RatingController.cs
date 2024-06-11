using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Mvc.Areas.Customer.Controllers.Common;

namespace Rookie.Mvc.Areas.Customer.Controllers.Rating
{
    [Area("Customer")]

    public class RatingController : BaseController
    {
        [HttpPost]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> AddRating(string OrderItemId, int rate, string Comment)
        {
            return View();
        }
    }
}