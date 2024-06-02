using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Users.Commands.LoginCommand;
using Rookie.Application.Users.Commands.RegisterCommand;
using Rookie.Application.Users.Queries.GetByUserNameQuery;

namespace Rookie.WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await Mediator.Send(new GetByUserNameQuery { UserName = User.Identity!.Name });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}