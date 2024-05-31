using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.MainImages.Commands.UpdateMainImageCommand;

namespace Rookie.WebApi.Controllers
{
    [Route("api/main-image")]
    [ApiController]
    public class MainImageController : BaseApiController
    {
        [HttpPut]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateMainImage([FromForm] UpdateMainImageCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}