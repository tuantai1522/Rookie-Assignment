using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace Rookie.Mvc.Areas.Identity.Controllers.Logout
{
    [Area("Identity")]

    public class LogoutController : Controller
    {
        public async Task<IActionResult> LogoutUser()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            string cookieName = "Jwt";
            // Check if the cookie exists
            if (Request.Cookies[cookieName] != null)
            {
                Response.Cookies.Append("Jwt", "", new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true, // Set to true if using HTTPS
                    SameSite = SameSiteMode.Strict // Adjust as needed
                });
            }


            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}