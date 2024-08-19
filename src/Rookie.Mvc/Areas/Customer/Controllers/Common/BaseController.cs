using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Rookie.Mvc.Areas.Customer.Models.Cart;
using Rookie.Mvc.Utils;

namespace Rookie.Mvc.Areas.Customer.Controllers.Common
{
    public class BaseController : Controller
    {
        protected readonly HttpClient _client;

        public BaseController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = Utilities.BASE_ADDRESS;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                CartVm cart = new CartVm();

                string accessToken = context.HttpContext.Request.Cookies["Jwt"];

                // Create a new HttpClient and set the authorization header with the bearer token
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Make a GET request to the Web API endpoint
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Cart/GetCurrentCart");
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content and deserialize it into a CartVm object
                    string data = await response.Content.ReadAsStringAsync();
                    cart = JsonConvert.DeserializeObject<CartVm>(data);

                    context.HttpContext.Items["CartQuantity"] = cart.CartItems.Sum(x => x.Quantity);
                }
            }

            await next(); // Call the next delegate/middleware in the pipeline
        }
    }
}