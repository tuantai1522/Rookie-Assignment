using Microsoft.AspNetCore.Mvc;
using Rookie.Application.MainImages.Commands.UpdateMainImageCommand;

namespace Rookie.WebApi.Controllers
{
    [Route("api/main-image")]
    [ApiController]
    public class MainImageController : BaseApiController
    {
        [HttpPut]
        public async Task<IActionResult> CreateCategory(UpdateMainImageCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}