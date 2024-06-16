using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Products.Commands.CreateProductCommand;
using Rookie.Application.Products.Commands.DeleteProductCommand;
using Rookie.Application.Products.Commands.UpdateProductCommand;
using Rookie.Application.Products.Queries.GetByIdQuery;
using Rookie.Application.Products.Queries.GetListQuery;
using Rookie.Domain.ProductEntity;
using Rookie.WebApi.Controllers.Base;
using Rookie.WebApi.Controllers.Products.Request;

namespace Rookie.WebApi.Controllers.Products
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(IMediator mediator) : BaseApiController(mediator)
    {
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductParams ProductParams)
        {
            var result = await _mediator.Send(new GetListQuery { ProductParams = ProductParams });
            if (result.IsSuccess)
            {
                Response.Headers.Append("pagination", JsonSerializer.Serialize(result.Value.MetaData));
                return Ok(result.Value);
            }
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] GetByIdRequest request)
        {
            var result = await _mediator.Send(new GetByIdQuery { ProductId = request.ProductId });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPost("CreateProduct")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateRequest request)
        {
            var result = await _mediator.Send(new CreateProductCommand
            {
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
                FileImage = request.FileImage,
                ProductName = request.ProductName,
                QuantityInStock = request.QuantityInStock,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpDelete("DeleteProductById")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteProductById([FromQuery] DeleteRequest request)
        {
            var result = await _mediator.Send(new DeleteProductCommand { ProductId = request.ProductId });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }

        [HttpPut("UpdateProductById")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateProductById([FromForm] UpdateRequest request)
        {
            var result = await _mediator.Send(new UpdateProductCommand
            {
                CategoryId = request.CategoryId,
                ProductName = request.ProductName,
                Id = request.Id,
                Description = request.Description,
                Price = request.Price,
                QuantityInStock = request.QuantityInStock,
            });

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(new { Error = result.Error.Message });
        }
    }
}