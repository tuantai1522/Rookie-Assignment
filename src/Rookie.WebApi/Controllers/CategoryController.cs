using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Categories.Queries.GetByIdQuery;
using Rookie.Application.Categories.Queries.GetListQuery;
using Rookie.Domain.CategoryEntity;

namespace Rookie.WebApi.Controllers
{
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await Mediator.Send(new GetListQuery()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            return Ok(await Mediator.Send(new GetByIdQuery { Id = new CategoryId(id) }));
        }
    }
}