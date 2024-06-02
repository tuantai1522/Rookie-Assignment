using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Orders.Commands.CreateOrderCommand;
using Rookie.Application.Orders.Queries.GetByIdQuery;
using Rookie.WebApi.Controllers.Orders.Request;
namespace Rookie.WebApi.Controllers.Orders
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var result = await Mediator.Send(new GetByIdQuery { OrderId = id, UserName = User.Identity!.Name });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> CreateOrder(CreateRequest request)
        {
            var result = await Mediator.Send(new CreateOrderCommand
            {
                ShippingAddress = request.ShippingAddress,
                UserName = User.Identity!.Name,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}