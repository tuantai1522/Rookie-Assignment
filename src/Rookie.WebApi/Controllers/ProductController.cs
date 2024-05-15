using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Products.Commands.CreateProductCommand;
using Rookie.Application.Products.Commands.DeleteProductCommand;
using Rookie.Application.Products.Commands.UpdateProductCommand;
using Rookie.Application.Products.Queries.GetByIdQuery;
using Rookie.Application.Products.Queries.GetListQuery;
using Rookie.Domain.ProductEntity;

namespace Rookie.WebApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductParams ProductParams)
        {
            var result = await Mediator.Send(new GetListQuery { ProductParams = ProductParams });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var result = await Mediator.Send(new GetByIdQuery { ProductId = id });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            var result = await Mediator.Send(new DeleteProductCommand { ProductId = id });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductById(UpdateProductCommand command)
        {
            var result = await Mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}