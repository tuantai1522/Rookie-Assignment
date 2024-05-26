using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Carts.Commands.ChangeCartQuantityCommand;
using Rookie.WebApi.Controllers.Carts.Request;

namespace Rookie.WebApi.Controllers.Carts
{
    public class CartController : BaseApiController
    {
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