using Moq;
using Rookie.Application.Categories.Commands.CreateCategoryCommand;
using Rookie.Application.Categories.Commands.UpdateCategoryCommand;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.ProductEntity;
using AutoFixture;
using Rookie.Application.Categories.Commands.DeleteCategoryCommand;
using FluentAssertions;
using Azure.Core;

namespace Rookie.Application.Tests.Categories.Commands
{
    public class UpdateCategoryCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange

            var request = _fixture.Build<UpdateCategoryCommand>()
              .With(x => x.CategoryName, "")
              .Create();

            var handler = _fixture.Create<UpdateCategoryCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CategoryErrors.UpdateCategoryInvalidData);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryIsNotUpdated()
        {
            // Arrange
            var request = _fixture.Build<UpdateCategoryCommand>()
              .With(x => x.Id, Guid.NewGuid().ToString())
              .Create();

            _mockCategoryRepository.Setup(repo => repo.Update(It.IsAny<Category>()))
                .ReturnsAsync(false);

            var handler = new UpdateCategoryCommandHandler(
                _mockCategoryRepository.Object,
                _mockMapper.Object
                );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CategoryErrors.NotFindCategory);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCategoryIsUpdated()
        {
            // Arrange
            var request = _fixture.Build<UpdateCategoryCommand>()
              .With(x => x.Id, Guid.NewGuid().ToString())
              .Create();


            _mockCategoryRepository.Setup(repo => repo.Update(It.IsAny<Category>()))
                .ReturnsAsync(true);

            var category = _fixture.Create<Category>();
            var categoryVm = _fixture.Create<CategoryVm>();

            _mockMapper.Setup(m => m.Map<Category, CategoryVm>(It.IsAny<Category>()))
                .Returns(categoryVm);

            var handler = new UpdateCategoryCommandHandler(
                _mockCategoryRepository.Object,
                _mockMapper.Object
            );

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();
        }

    }
}
