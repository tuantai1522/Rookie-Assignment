using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Images.Commands.CreateImageCommand;
using Rookie.Application.Images.Commands.DeleteImageCommand;
using Rookie.WebApi.Controllers.Base;
using Rookie.WebApi.Controllers.Images.Request;

namespace Rookie.WebApi.Controllers.Images
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : BaseApiController
    {
        [HttpPost("CreateImage")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateImage([FromForm] CreateRequest request)
        {
            var result = await Mediator.Send(new CreateImageCommand
            {
                FileImage = request.FileImage,
                ProductId = request.ProductId,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpDelete("DeleteImage")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteImage([FromQuery] DeleteRequest request)
        {
            var result = await Mediator.Send(new DeleteImageCommand { ImageId = request.ImageId });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}