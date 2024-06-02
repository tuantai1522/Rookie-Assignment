using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Mvc.Areas.Customer.Controllers.Common;
using Rookie.Mvc.Areas.Customer.Models.Cart;

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
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/cart/GetCurrentCart");
            if (response.IsSuccessStatusCode)
            {
                // Read the response content and deserialize it into a CartVm object
                string data = await response.Content.ReadAsStringAsync();
                cart = JsonConvert.DeserializeObject<CartVm>(data);
            }

            return View(cart);
        }

        [HttpPost]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> ChangeCart(string productId, string action, int quantity)
        {
            ChangeCartDto changeCartDto = new()
            {
                ProductId = productId
            };

            if (action == "remove")
                changeCartDto.Quantity = -1;
            else if (action == "add")
                changeCartDto.Quantity = 1;
            else
                changeCartDto.Quantity = quantity;

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(changeCartDto),
                                                            Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/cart/ChangeCartQuantity",
                                                                        stringContent);

            string info = await response.Content.ReadAsStringAsync();

            return RedirectToAction("Index", "Cart", new { area = "Customer" });
        }
    }
}