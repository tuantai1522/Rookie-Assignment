using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Categories.Commands.CreateCategoryCommand;
using Rookie.Application.Categories.Commands.DeleteCategoryCommand;
using Rookie.Application.Categories.Commands.UpdateCategoryCommand;
using Rookie.Application.Categories.Queries.GetByIdQuery;
using Rookie.Application.Categories.Queries.GetListQuery;
using Rookie.WebApi.Controllers.Base;
using Rookie.WebApi.Controllers.Categories.Request;

namespace Rookie.WebApi.Controllers.Categories
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await Mediator.Send(new GetListQuery());
            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById([FromQuery] GetByIdRequest request)
        {
            var result = await Mediator.Send(new GetByIdQuery { Id = request.CategoryId });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost("CreateCategory")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateRequest request)
        {
            var result = await Mediator.Send(new CreateCategoryCommand
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }


        [HttpDelete("DeleteCategoryById")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteCategoryById([FromQuery] DeleteRequest request)
        {
            var result = await Mediator.Send(new DeleteCategoryCommand { CategoryId = request.CategoryId });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPut("UpdateCategoryById")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateCategoryById([FromForm] UpdateRequest request)
        {
            var result = await Mediator.Send(new UpdateCategoryCommand
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
                Id = request.Id,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}