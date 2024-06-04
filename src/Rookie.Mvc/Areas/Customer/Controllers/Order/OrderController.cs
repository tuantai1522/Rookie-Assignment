using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.Common;
using Rookie.Mvc.Areas.Customer.Controllers.Common;
using Rookie.Mvc.Areas.Customer.Models.Cart;
using Rookie.Mvc.Areas.Customer.Models.Order;
using Rookie.Mvc.Interface;
using Rookie.Mvc.ViewModels;
using Address = Rookie.Mvc.Areas.Customer.Models.Order.Address;

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

        [HttpGet]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> ListOrder(int CurPage)
        {
            List<OrderVm> orders = new List<OrderVm>();

            if (CurPage == 0)
                CurPage = 1;

            string accessToken = Request.Cookies["Jwt"];

            // Create a new HttpClient and set the authorization header with the bearer token
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Make a GET request to the Web API endpoint
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/order/GetOrdersById?PageNumber={CurPage}");
            if (response.IsSuccessStatusCode)
            {
                // Read the response content and deserialize it into a CartVm object
                string data = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<List<OrderVm>>(data);

                //get info of pagination from current api
                string paginationHeader = response.Headers.GetValues("pagination").FirstOrDefault();
                MetaData paginationData = JsonConvert.DeserializeObject<MetaData>(paginationHeader);
                int curPage = paginationData.CurPage;
                int totalPage = paginationData.TotalPage;
                int pageSize = paginationData.PageSize;
                ViewData["curPage"] = curPage;
                ViewData["totalPage"] = totalPage;
                ViewData["pageSize"] = pageSize;

                if (CurPage == 0 || CurPage == totalPage + 1)
                    return RedirectToAction("ListOrder");

            }

            return View(orders);
        }

        [HttpPost]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> MakeOrder(Address model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = $"Please provide address to order";
                return RedirectToAction("Index");
            }
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
                    Address = model,
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
        public async Task<IActionResult> PaymentBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["ErrorMessage"] = $"Lỗi thanh toán VN Pay";
                return RedirectToAction("PaymentFail");
            }

            var addressJson = response.OrderDescription;
            var address = JsonConvert.DeserializeObject<Address>(addressJson);

            var ShippingAddress = new ShippingAddressVm(address);

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(ShippingAddress),
                                                Encoding.UTF8, "application/json");

            HttpResponseMessage responseCreateOrder = await _client.PostAsync(_client.BaseAddress + $"/order/CreateOrder",
                                                                        stringContent);

            string info = await responseCreateOrder.Content.ReadAsStringAsync();

            dynamic data = JsonConvert.DeserializeObject<dynamic>(info);

            TempData["SuccessMessage"] = $"Order with id {data.value} is made successfully";
            return RedirectToAction("PaymentSuccess");

        }
    }
}