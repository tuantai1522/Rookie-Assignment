using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Categories.Commands.CreateCategoryCommand;
using Rookie.Application.Categories.Commands.DeleteCategoryCommand;
using Rookie.Application.Categories.Commands.UpdateCategoryCommand;
using Rookie.Application.Categories.Queries.GetByIdQuery;
using Rookie.Application.Categories.Queries.GetListQuery;
using Rookie.Domain.CategoryEntity;

namespace Rookie.WebApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await Mediator.Send(new GetListQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var result = await Mediator.Send(new GetByIdQuery { Id = id });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryById(string id)
        {
            var result = await Mediator.Send(new DeleteCategoryCommand { CategoryId = id });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategoryById(UpdateCategoryCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}