using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Rookie.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected IMediator _mediator { get; set; }

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    }
}