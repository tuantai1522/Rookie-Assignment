using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Users.Commands.LoginCommand;

namespace Rookie.WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}