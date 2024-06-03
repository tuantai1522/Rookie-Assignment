using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Application.Users.ViewModels;
using Rookie.Mvc.Areas.Customer.Controllers.Common;
using Rookie.Mvc.Areas.Customer.Models.Cart;
using Rookie.Mvc.Areas.Customer.Models.Order;
using Rookie.Mvc.Interface;
using Rookie.Mvc.ViewModels;

namespace Rookie.Mvc.Areas.Customer.Controllers.Order
{
    [Area("Customer")]
    public class OrderController : BaseController
    {
        private readonly IVnPayService _vnPayService;
        public OrderController(IVnPayService vnPayService)
        {
            this._vnPayService = vnPayService;
        }

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

            //call current address
            List<UserAddressVm> addressList = new List<UserAddressVm>();


            // Create a new HttpClient and set the authorization header with the bearer token
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Make a GET request to the Web API endpoint
            HttpResponseMessage responseAddress = await _client.GetAsync(_client.BaseAddress + "/user/GetAddressUser");
            if (responseAddress.IsSuccessStatusCode)
            {
                // Read the responseAddress content and deserialize it into a CartVm object
                string data = await responseAddress.Content.ReadAsStringAsync();
                addressList = JsonConvert.DeserializeObject<List<UserAddressVm>>(data);
            }

            ViewData["addressList"] = addressList;

            return View(cart);
        }

        [HttpPost]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> MakeOrder()
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

                var vnPayModel = new VnPaymentRequest
                {
                    OrderId = new Random().Next(1000, 10000),
                    FullName = $"{User.FindFirstValue(JwtRegisteredClaimNames.UniqueName)}",
                    Description = "Make an order",
                    Total = (double)cart.TotalPrice,
                    CreatedDate = DateTime.Now,

                };
                return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
            }

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "RequireCustomerRole")]
        public IActionResult PaymentFail()
        {
            return View();
        }

        [Authorize(Policy = "RequireCustomerRole")]
        public IActionResult PaymentSuccess()
        {
            return View();
        }

        [Authorize(Policy = "RequireCustomerRole")]
        public IActionResult PaymentBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["ErrorMessage"] = $"Lỗi thanh toán VN Pay";
                return RedirectToAction("PaymentFail");
            }

            TempData["SuccessMessage"] = $"Thanh toán VN Pay thành công";
            return RedirectToAction("PaymentSuccess");

        }
    }
}