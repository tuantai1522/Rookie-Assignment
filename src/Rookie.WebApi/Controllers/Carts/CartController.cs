using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Carts.Commands.ChangeCartQuantityCommand;
using Rookie.Application.Carts.Queries.GetCartByUserNameQuery;
using Rookie.WebApi.Controllers.Carts.Request;

namespace Rookie.WebApi.Controllers.Carts
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : BaseApiController
    {
        [HttpGet("GetCurrentCart")]
        [Authorize(Policy = "RequireCustomerRole")]

        public async Task<IActionResult> GetCurrentCart()
        {
            var result = await Mediator.Send(new GetCartByUserNameQuery { UserName = User.Identity!.Name });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> ChangeCartQuantity(ChangeCartQuantityRequest command)
        {
            var result = await Mediator.Send(new ChangeCartQuantityCommand
            {
                UserName = User.Identity!.Name,
                ProductId = command.ProductId,
                Quantity = command.Quantity,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}