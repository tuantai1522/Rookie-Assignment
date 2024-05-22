using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Users.Commands.LoginCommand;
using Rookie.Application.Users.Commands.RegisterCommand;

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

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}