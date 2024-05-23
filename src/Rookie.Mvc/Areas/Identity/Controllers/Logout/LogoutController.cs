using Microsoft.AspNetCore.Mvc;

namespace Rookie.Mvc.Areas.Identity.Controllers.Logout
{
    [Area("Identity")]

    public class LogoutController : Controller
    {
        public IActionResult LogoutUser()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}