using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Images.ViewModels;
using Rookie.Application.Products.Commands.CreateProductCommand;
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
    public class CreateProductCommandTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<CreateProductCommand>()
                      .With(r => r.CategoryId, "")
                      .Create();

            var handler = _fixture.Create<CreateProductCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.CreateProductInvalidData);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryDoesNotExist()
        {
            // Arrange
            var request = _fixture.Build<CreateProductCommand>()
                  .With(r => r.CategoryId, Guid.NewGuid().ToString())
                  .Create();

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as Category);

            var handler = new CreateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object,
                _mockMainImageRepository.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.NotFindCategory);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenUploadImageFailed()
        {
            // Arrange

            var category = _fixture.Build<Category>()
                  .With(r => r.Id, new CategoryId(Guid.NewGuid()))
                  .Create();

            var request = _fixture.Build<CreateProductCommand>()
                  .With(r => r.CategoryId, Guid.NewGuid().ToString())
                  .Create();

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);


            var imageVm = _fixture.Build<ImageVm>()
                  .With(r => r.Url, "")
                  .With(r => r.PublicId, "")
                  .Create();

            _mockImageService.Setup(service => service.AddPhoto(It.IsAny<IFormFile>()))
                .ReturnsAsync(imageVm);

            var handler = new CreateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object,
                _mockMainImageRepository.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(ProductErrors.UploadImageFailed);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenProductIsCreated()
        {
            // Arrange
            var category = _fixture.Build<Category>()
                  .With(r => r.Id, new CategoryId(Guid.NewGuid()))
                  .Create();

            var request = _fixture.Build<CreateProductCommand>()
                  .With(r => r.CategoryId, Guid.NewGuid().ToString())
                  .Create();

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(category);

            var imageVm = _fixture.Create<ImageVm>();

            _mockImageService.Setup(service => service.AddPhoto(It.IsAny<IFormFile>()))
                .ReturnsAsync(imageVm);

            var handler = new CreateProductCommandHandler(
                _mockProductRepository.Object,
                _mockCategoryRepository.Object,
                _mockImageService.Object,
                _mockImageRepository.Object,
                _mockMainImageRepository.Object
                );
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
        }
    }
}
