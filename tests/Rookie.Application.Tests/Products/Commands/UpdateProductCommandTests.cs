using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Rookie.Application.Products.Commands.DeleteProductCommand;
using Rookie.Application.Products.Commands.UpdateProductCommand;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Products.Commands
{
    public class UpdateProductCommandTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange

            var request = _fixture.Build<UpdateProductCommand>()
              .With(r => r.Id, "")
              .Create();

            var handler = _fixture.Create<UpdateProductCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.UpdateProductInvalidData);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryDoesNotExist()
        {
            // Arrange
            var request = _fixture.Build<UpdateProductCommand>()
              .With(r => r.Id, Guid.NewGuid().ToString())
              .With(r => r.CategoryId, Guid.NewGuid().ToString())
              .Create();

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as Category);

            var handler = new UpdateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockMapper.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.NotFindCategory);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenProductDoesNotExist()
        {
            // Arrange
            var request = _fixture.Build<UpdateProductCommand>()
              .With(r => r.Id, Guid.NewGuid().ToString())
              .With(r => r.CategoryId, Guid.NewGuid().ToString())
              .Create();

            var category = _fixture.Build<Category>()
              .With(r => r.Id, new CategoryId(Guid.NewGuid().ToString()))
              .Create();
            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);

            _mockProductRepository.Setup(repo => repo.Update(It.IsAny<Product>()))
                .ReturnsAsync(false);

            var handler = new UpdateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockMapper.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.NotFindProduct);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenProductIsUpdated()
        {
            // Arrange
            var request = _fixture.Build<UpdateProductCommand>()
                  .With(r => r.Id, Guid.NewGuid().ToString())
                  .With(r => r.CategoryId, Guid.NewGuid().ToString())
                  .Create();

            var category = _fixture.Build<Category>()
                  .With(r => r.Id, new CategoryId(Guid.NewGuid().ToString()))
                  .Create();
            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);



            var product = _fixture.Build<Product>()
                  .With(r => r.Id, new ProductId(category.Id.ToString()))
                  .Create();
            _mockProductRepository.Setup(repo => repo.Update(It.IsAny<Product>()))
                .ReturnsAsync(true);

            var productVm = _fixture.Build<ProductVm>()
                  .With(r => r.Id, request.Id)
                  .Create();
            _mockMapper.Setup(m => m.Map<Product, ProductVm>(It.IsAny<Product>()))
                .Returns(productVm);

            var handler = new UpdateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockMapper.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
        }
    }
}
