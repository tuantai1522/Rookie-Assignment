
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Mvc.Areas.Identity.Models.Register;
using Rookie.Mvc.Utils;

namespace Rookie.Mvc.Areas.Identity.Controllers.Register
{
    [Area("Identity")]
    public class RegisterController : Controller
    {
        private readonly HttpClient _client;

        public RegisterController()
        {
            _client = new HttpClient();
            _client.BaseAddress = Utilities.BASE_ADDRESS;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterDto data)
        {
            if (ModelState.IsValid)
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(data),
                                                                Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/user/RegisterUser",
                                                                            stringContent);

                string info = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(info))
                {
                    //username or password is not correct
                    if (doc.RootElement.TryGetProperty("error", out JsonElement errorElement))
                    {
                        ViewBag.ErrorMessage = errorElement.GetString();
                        return View("Index", data); // Return to the login view with the current data and validation errors
                    }

                }

                ViewBag.SuccessMessage = "Register successfully";

                return View("Index");
            }

            ViewBag.ErrorMessage = "Please provide full infomration";
            return View("Index", data); // Return to the login view with the current data and validation errors

        }

    }
}