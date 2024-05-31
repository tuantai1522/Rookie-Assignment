using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Images.Commands.CreateImageCommand;
using Rookie.Application.Images.Commands.DeleteImageCommand;

namespace Rookie.WebApi.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : BaseApiController
    {
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateImage(CreateImageCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpDelete]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteImage(DeleteImageCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}