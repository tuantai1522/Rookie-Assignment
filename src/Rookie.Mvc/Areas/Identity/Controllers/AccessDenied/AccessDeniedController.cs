using Microsoft.AspNetCore.Mvc;

namespace Rookie.Mvc.Areas.Identity.Controllers.AccessDenied
{
    [Area("Identity")]
    public class AccessDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}