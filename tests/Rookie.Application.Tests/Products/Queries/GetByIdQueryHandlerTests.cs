using Rookie.Domain.ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rookie.Application.Products.ViewModels;
using AutoMapper;
using Moq;
using Rookie.Domain.Common;
using Rookie.Application.Products.Queries.GetByIdQuery;
using System.Linq.Expressions;
using Rookie.Domain.DomainError;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.Tests;

namespace Rookie.Application.Tests.Products.Queries
{
    public class GetByIdQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsSuccessResult_WhenProductIsFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var query = new GetByIdQuery { ProductId = productId.ToString() };

            var fakeProduct = new Product
            {
                Id = new ProductId(productId),
                ProductName = "Product 1"
            };

            var fakeProductVm = new ProductVm
            {
                Id = productId.ToString(),
                ProductName = "Product 1"
            };

            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(fakeProduct);

            _mockMapper.Setup(mapper => mapper.Map<Product, ProductVm>(fakeProduct))
                   .Returns(fakeProductVm);

            var handler = new GetByIdQueryHandler(_mockProductRepository.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(fakeProductVm.Id, result.Value.Id);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenProductIsNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var query = new GetByIdQuery { ProductId = productId.ToString() };

            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((Product)null);


            var handler = new GetByIdQueryHandler(_mockProductRepository.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.NotFindProduct, result.Error);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var productId = "123"; // Invalid Product Id
            var query = new GetByIdQuery { ProductId = productId.ToString() };

            var handler = new GetByIdQueryHandler(_mockProductRepository.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ProductErrors.QueryProductInvalidData, result.Error);
        }
    }
}
