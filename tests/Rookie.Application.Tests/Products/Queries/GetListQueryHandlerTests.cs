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
using AutoFixture;
using FluentAssertions;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Tests.Products.Queries
{
    public class GetListQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsSuccessResult_WhenRequestIsValid()
        {
            // Arrange
            var productParams = new ProductParams { PageNumber = 1, PageSize = 6 };

            var request = _fixture.Build<GetListQuery>()
                .With(x => x.ProductParams, productParams)
                .Create();

            _mockProductRepository.Setup(repo => repo.GetAll(It.IsAny<ProductParams>(), It.IsAny<string>()))
                .ReturnsAsync(new PagedList<Product>(GetFakeProducts(), GetFakeProducts().Count, 1, 6));

            _mockMapper.Setup(mapper => mapper.Map<PagedList<ProductVm>>(It.IsAny<PagedList<Product>>()))
                .Returns(new PagedList<ProductVm>(GetFakeProductVms(), GetFakeProductVms().Count, 1, 6));

            var handler = new GetListQueryHandler(_mockProductRepository.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
            result.Value.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var productParams = new ProductParams { PageNumber = -1, PageSize = 6 };

            var request = _fixture.Build<GetListQuery>()
                            .With(x => x.ProductParams, productParams)
                            .Create();

            var handler = _fixture.Create<GetListQueryHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().NotBeNull();
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