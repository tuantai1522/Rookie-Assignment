using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Mvc.Areas.Customer.Controllers.Common;
using Rookie.Mvc.Areas.Customer.Models.Rating;

namespace Rookie.Mvc.Areas.Customer.Controllers.Rating
{
    [Area("Customer")]

    public class RatingController : BaseController
    {
        public RatingController(HttpClient client) : base(client)
        {
        }

        [HttpPost]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> AddRating(CreateRequest data)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = $"Please provide full information to rate this product";

                return RedirectToAction("GetOrderDetails", "Order", new { area = "Customer", OrderId = data.OrderId });

            }
            string accessToken = Request.Cookies["Jwt"];

            // Create a new HttpClient and set the authorization header with the bearer token
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Create form data

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(data.OrderItemId ?? string.Empty), "OrderItemId");
            formData.Add(new StringContent(data.Comment ?? string.Empty), "Comment");
            formData.Add(new StringContent(data.Rating.ToString()), "Rating");

            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/rating/CreateRating",
                                                                        formData);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = $"Rating product successfully";
                return RedirectToAction("GetOrderDetails", "Order", new { area = "Customer", OrderId = data.OrderId });
            }

            TempData["ErrorMessage"] = $"Error when rating product";

            return RedirectToAction("GetOrderDetails", "Order", new { area = "Customer", OrderId = data.OrderId });

        }
    }
}