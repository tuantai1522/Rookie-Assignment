using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Users.Commands.LoginCommand;
using Rookie.Application.Users.Commands.RegisterCommand;
using Rookie.Application.Users.Queries.GetAddressByUserNameQuery;
using Rookie.Application.Users.Queries.GetByUserNameQuery;
using Rookie.Application.Users.Queries.GetListQuery;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.WebApi.Controllers.Base;
using Rookie.WebApi.Controllers.Users.Request;

namespace Rookie.WebApi.Controllers.Users
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseApiController
    {
        [HttpGet("GetAllUsers")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAllUsers([FromQuery] ApplicationUserParams ApplicationUserParams)
        {
            var result = await Mediator.Send(new GetListQuery { ApplicationUserParams = ApplicationUserParams });
            if (result.IsSuccess)
            {
                Response.Headers.Append("pagination", JsonSerializer.Serialize(result.Value.MetaData));
                return Ok(result.Value);
            }
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginUserRequest request)
        {
            var result = await Mediator.Send(new LoginCommand
            {
                Password = request.Password,
                UserName = request.UserName,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserRequest request)
        {
            var result = await Mediator.Send(new RegisterCommand
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                UserName = request.UserName,
                Role = request.Role,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await Mediator.Send(new GetByUserNameQuery { UserName = User.Identity!.Name });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpGet("GetAddressUser")]
        [Authorize]
        public async Task<IActionResult> GetAddressUser()
        {
            var result = await Mediator.Send(new GetAddressByUserNameQuery { UserName = User.Identity!.Name });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}