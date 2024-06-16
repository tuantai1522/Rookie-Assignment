using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Ratings.Commands.CreateRatingCommand;
using Rookie.Application.Ratings.Queries;
using Rookie.WebApi.Controllers.Base;
using Rookie.WebApi.Controllers.Ratings.Request;

namespace Rookie.WebApi.Controllers.Ratings
{
    [Route("api/rating")]
    [ApiController]
    public class RatingController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet("GetAllRatings")]
        public async Task<IActionResult> GetAllRatings([FromQuery] GetListRequest request)
        {
            var result = await _mediator.Send(new GetListQuery
            {
                ProductId = request.ProductId,
                RatingParams = request.RatingParams,
            });
            if (result.IsSuccess)
            {
                Response.Headers.Append("pagination", JsonSerializer.Serialize(result.Value.MetaData));
                return Ok(result.Value);
            }
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost("CreateRating")]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> CreateRating([FromForm] CreateRequest request)
        {
            var result = await _mediator.Send(new CreateRatingCommand
            {
                UserName = User.Identity!.Name,
                OrderItemId = request.OrderItemId,
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