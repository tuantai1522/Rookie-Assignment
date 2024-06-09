using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Ratings.Commands.CreateRatingCommand;
using Rookie.WebApi.Controllers.Base;
using Rookie.WebApi.Controllers.Ratings.Request;

namespace Rookie.WebApi.Controllers.Ratings
{
    [Route("api/product")]
    [ApiController]
    public class RatingController : BaseApiController
    {
        [HttpPost("CreateRating")]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> CreateRating([FromForm] CreateRequest request)
        {
            var result = await Mediator.Send(new CreateRatingCommand
            {
                UserName = User.Identity!.Name,
                ProductId = request.ProductId,
                Comment = request.Comment,
                Rating = request.Rating,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}