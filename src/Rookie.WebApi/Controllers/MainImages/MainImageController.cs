using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.MainImages.Commands.UpdateMainImageCommand;
using Rookie.WebApi.Controllers.Base;
using Rookie.WebApi.Controllers.MainImages.Request;

namespace Rookie.WebApi.Controllers.MainImages
{
    [Route("api/main-image")]
    [ApiController]
    public class MainImageController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpPut("UpdateMainImage")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateMainImage([FromForm] UpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateMainImageCommand { ProductId = request.ProductId, ImageId = request.ImageId });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}