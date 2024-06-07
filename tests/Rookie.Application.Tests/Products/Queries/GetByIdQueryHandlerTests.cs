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
using AutoFixture;
using Rookie.Application.Products.Commands.CreateProductCommand;
using FluentAssertions;

namespace Rookie.Application.Tests.Products.Queries
{
    public class GetByIdQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsSuccessResult_WhenProductIsFound()
        {
            // Arrange

            // Arrange
            var request = _fixture.Build<GetByIdQuery>()
              .With(x => x.ProductId, Guid.NewGuid().ToString())
              .Create();

            var handler = _fixture.Create<GetByIdQueryHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenProductIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<GetByIdQuery>()
                      .With(r => r.ProductId, Guid.NewGuid().ToString())
                      .Create();

            _mockProductRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as Product);


            var handler = new GetByIdQueryHandler(
                _mockProductRepository.Object, 
                _mockMapper.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.NotFindProduct);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Create<GetByIdQuery>();

            var handler = new GetByIdQueryHandler(_mockProductRepository.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.QueryProductInvalidData);
        }
    }
}
