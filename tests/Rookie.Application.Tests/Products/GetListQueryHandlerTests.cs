using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Rookie.Application.Products.Queries.GetListQuery;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.Tests;

namespace Rookie.Application.Tests.Products
{
    public class GetListQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsSuccessResult_WhenRequestIsValid()
        {
            // Arrange
            var productParams = new ProductParams { PageNumber = 1, PageSize = 6 };

            _mockProductRepository.Setup(repo => repo.GetAll(It.IsAny<ProductParams>(), It.IsAny<string>()))
                .ReturnsAsync(new PagedList<Product>(GetFakeProducts(), this.GetFakeProducts().Count, 1, 6));

            _mockMapper.Setup(mapper => mapper.Map<PagedList<ProductVm>>(It.IsAny<PagedList<Product>>()))
                .Returns(new PagedList<ProductVm>(GetFakeProductVms(), this.GetFakeProductVms().Count, 1, 6));

            var handler = new GetListQueryHandler(_mockProductRepository.Object, _mockMapper.Object);
            var query = new GetListQuery { ProductParams = productParams };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var productParams = new ProductParams { PageNumber = -1, PageSize = 6 };

            _mockProductRepository.Setup(repo => repo.GetAll(It.IsAny<ProductParams>(), It.IsAny<string>()))
                .ReturnsAsync(new PagedList<Product>(GetFakeProducts(), -1, 6, GetFakeProducts().Count));

            _mockMapper.Setup(mapper => mapper.Map<PagedList<ProductVm>>(It.IsAny<PagedList<Product>>()))
                .Returns(new PagedList<ProductVm>(GetFakeProductVms(), -1, 6, GetFakeProductVms().Count));

            var handler = new GetListQueryHandler(_mockProductRepository.Object, _mockMapper.Object);
            var query = new GetListQuery { ProductParams = productParams };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
        }

        private List<Product> GetFakeProducts()
        {
            return
            [
                new Product { Id = new ProductId(Guid.NewGuid()), ProductName = "Product 1" },
                new Product { Id = new ProductId(Guid.NewGuid()), ProductName = "Product 2" },
                new Product { Id = new ProductId(Guid.NewGuid()), ProductName = "Product 3" },
            ];
        }
        private List<ProductVm> GetFakeProductVms()
        {
            return
            [
                new ProductVm { Id = "1", ProductName = "ProductVm 1" },
                new ProductVm { Id = "2", ProductName = "ProductVm 2" },
                new ProductVm { Id = "3", ProductName = "ProductVm 3" },
            ];
        }
    }
}