using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Images.Commands.CreateImageCommand;

namespace Rookie.WebApi.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateImageCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}