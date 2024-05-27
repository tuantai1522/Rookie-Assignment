using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Mvc.Areas.Customer.Controllers.Common;
using Rookie.Mvc.Areas.Customer.Models.Cart;
using Rookie.Mvc.Utils;

namespace Rookie.Mvc.Areas.Customer.Controllers.Cart
{
    [Area("Customer")]
    public class CartController : BaseController
    {
        [HttpGet]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> Index()
        {
            CartVm cart = new CartVm();

            string accessToken = Request.Cookies["Jwt"];

            // Create a new HttpClient and set the authorization header with the bearer token
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Make a GET request to the Web API endpoint
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Cart/GetCurrentCart");
            if (response.IsSuccessStatusCode)
            {
                // Read the response content and deserialize it into a CartVm object
                string data = await response.Content.ReadAsStringAsync();
                cart = JsonConvert.DeserializeObject<CartVm>(data);
            }

            return View(cart);
        }
    }
}