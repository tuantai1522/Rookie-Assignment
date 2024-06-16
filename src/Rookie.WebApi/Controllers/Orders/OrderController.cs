using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Orders.Commands.CreateOrderCommand;
using Rookie.Application.Orders.Queries.GetByIdQuery;
using Rookie.Application.Orders.Queries.GetListByIdQuery;
using Rookie.Application.Orders.Queries.GetListQuery;
using Rookie.Domain.OrderEntity;
using Rookie.WebApi.Controllers.Base;
using Rookie.WebApi.Controllers.Orders.Request;
namespace Rookie.WebApi.Controllers.Orders
{
    [Route("api/order")]
    [ApiController]
    public class OrderController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet("GetAllOrders")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderParams OrderParams)
        {
            var result = await _mediator.Send(new GetListQuery { OrderParams = OrderParams });
            if (result.IsSuccess)
            {
                Response.Headers.Append("pagination", JsonSerializer.Serialize(result.Value.MetaData));
                return Ok(result.Value);
            }
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpGet("GetListByIdQuery")]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> GetListByIdQuery([FromQuery] GetListByIdRequest request)
        {
            var result = await _mediator.Send(new GetListByIdQuery { UserName = User.Identity!.Name, OrderParams = request.OrderParams });

            if (result.IsSuccess)
            {
                Response.Headers.Append("pagination", JsonSerializer.Serialize(result.Value.MetaData));
                return Ok(result.Value);
            }
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpGet("GetByIdQuery")]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> GetByIdQuery([FromQuery] GetByIdRequest request)
        {
            var result = await _mediator.Send(new GetByIdQuery { UserName = User.Identity!.Name, OrderId = request.OrderId });

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost("CreateOrder")]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> CreateOrder(CreateRequest request)
        {
            var result = await _mediator.Send(new CreateOrderCommand
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