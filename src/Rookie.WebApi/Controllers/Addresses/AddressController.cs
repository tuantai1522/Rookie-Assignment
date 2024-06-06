using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Addresses.Queries.GetAddressByUserNameQuery;
using Rookie.WebApi.Controllers.Base;

namespace Rookie.WebApi.Controllers.Addresses
{
    [Route("api/address")]
    public class AddressController : BaseApiController
    {
        [HttpGet("GetAddressUser")]
        [Authorize(Policy = "RequireCustomerRole")]
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