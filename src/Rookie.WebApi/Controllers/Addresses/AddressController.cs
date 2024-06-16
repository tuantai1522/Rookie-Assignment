using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Addresses.Commands.CreateAddressCommand;
using Rookie.Application.Addresses.Queries.GetAddressByUserNameQuery;
using Rookie.WebApi.Controllers.Addresses.Request;
using Rookie.WebApi.Controllers.Base;

namespace Rookie.WebApi.Controllers.Addresses
{
    [Route("api/address")]
    public class AddressController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet("GetAddressUser")]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> GetAddressUser()
        {
            var result = await _mediator.Send(new GetAddressByUserNameQuery { UserName = User.Identity!.Name });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost("CreateAddressUser")]
        [Authorize(Policy = "RequireCustomerRole")]
        public async Task<IActionResult> CreateAddressUser(CreateAddressRequest request)
        {
            var result = await _mediator.Send(new CreateAddressCommand { UserName = User.Identity!.Name, Address = request.Address });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}