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
            return Ok(await Mediator.Send(new GetListQuery { ProductParams = ProductParams }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return Ok(await Mediator.Send(new GetByIdQuery { ProductId = new ProductId(id) }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductById(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteProductCommand { ProductId = new ProductId(id) }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductById(UpdateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}