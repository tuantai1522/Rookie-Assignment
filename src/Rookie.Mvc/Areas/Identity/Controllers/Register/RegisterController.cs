
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(data.Email), "Email");
                    content.Add(new StringContent(data.FirstName), "FirstName");
                    content.Add(new StringContent(data.LastName), "LastName");
                    content.Add(new StringContent(data.PassWord), "Password");
                    content.Add(new StringContent(data.UserName), "UserName");

                    HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/user/RegisterUser", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        ViewBag.ErrorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                        return View("Index", data); // Return to the login view with the current data and validation errors
                    }

                    string info = await response.Content.ReadAsStringAsync();

                    try
                    {
                        using (JsonDocument doc = JsonDocument.Parse(info))
                        {
                            // Check if there is an error property in the response JSON
                            if (doc.RootElement.TryGetProperty("error", out JsonElement errorElement))
                            {
                                ViewBag.ErrorMessage = errorElement.GetString();
                                return View("Index", data); // Return to the login view with the current data and validation errors
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = $"Error when registering: {ex.Message}";
                        return View("Index", data);
                    }

                    ViewBag.SuccessMessage = "Registered successfully";
                    return View("Index");
                }
            }

            ViewBag.SuccessMessage = "Register successfully";

            return View("Index");
        }

    }
}