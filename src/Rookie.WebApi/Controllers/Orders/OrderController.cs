using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Orders.Commands.CreateOrderCommand;
using Rookie.Application.Orders.Queries.GetByIdQuery;
using Rookie.Application.Orders.Queries.GetListQuery;
using Rookie.Domain.OrderEntity;
using Rookie.WebApi.Controllers.Base;
using Rookie.WebApi.Controllers.Orders.Request;
namespace Rookie.WebApi.Controllers.Orders
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        [HttpGet("GetAllOrders")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderParams OrderParams)
        {
            var result = await Mediator.Send(new GetListQuery { OrderParams = OrderParams });
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpGet("GetOrderById")]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> GetOrderById([FromQuery] GetByIdRequest request)
        {
            var result = await Mediator.Send(new GetByIdQuery { OrderId = request.OrderId, UserName = User.Identity!.Name });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost("CreateOrder")]
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