using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Mvc.Areas.Identity.Models.Login;
using Rookie.Mvc.Utils;

namespace Rookie.Mvc.Areas.Identity.Controllers.Login
{
    [Area("Identity")]
    public class LoginController : Controller
    {
        private readonly HttpClient _client;

        public LoginController()
        {
            _client = new HttpClient();
            _client.BaseAddress = Utilities.BASE_ADDRESS;
        }

        public ActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginDto data)
        {
            if (ModelState.IsValid)
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/user/LoginUser", stringContent);

                string info = await response.Content.ReadAsStringAsync();

                // Parse the JSON string
                using (JsonDocument doc = JsonDocument.Parse(info))
                {
                    //username or password is not correct
                    if (doc.RootElement.TryGetProperty("error", out JsonElement errorElement))
                    {
                        ViewBag.ErrorMessage = errorElement.GetString();
                        return View("Index", data); // Return to the login view with the current data and validation errors
                    }

                    string token = doc.RootElement.GetProperty("token").GetString();
                    HttpContext.Session.SetString("JWT", token);

                    string userName = doc.RootElement.GetProperty("userName").GetString();
                    HttpContext.Session.SetString("userName", userName);
                }
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }

            ViewBag.ErrorMessage = "Please provide full infomration";

            return View("Index", data); // Return to the login view with the current data and validation errors

        }
    }
}