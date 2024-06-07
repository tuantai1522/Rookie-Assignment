using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Mvc.Areas.Customer.Controllers.Common;
using Rookie.Mvc.Areas.Customer.Models.Order;

namespace Rookie.Mvc.Areas.Customer.Controllers.Info
{
    [Area("Customer")]
    public class InfoController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> AddNewAddress(Address Address)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = $"Please provide address to add";
                return RedirectToAction("Index");
            }

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(Address),
                                                Encoding.UTF8, "application/json");

            string accessToken = Request.Cookies["Jwt"];

            // Create a new HttpClient and set the authorization header with the bearer token
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/address/CreateAddressUser", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                return View("Index", Address); // Return to the login view with the current data and validation errors
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
                        return View("Index", Address); // Return to the login view with the current data and validation errors
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error when registering: {ex.Message}";
                return View("Index", Address);
            }

            ViewBag.SuccessMessage = "Registered successfully";
            return View("Index");
        }
    }
}