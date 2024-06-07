using AutoFixture;
using FluentAssertions;
using Moq;
using Rookie.Application.Categories.Commands.CreateCategoryCommand;
using Rookie.Application.Categories.Commands.DeleteCategoryCommand;
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

namespace Rookie.Application.Tests.Categories.Commands
{
    public class DeleteCategoryCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<DeleteCategoryCommand>()
              .With(x => x.CategoryId, "")
              .Create();

            var handler = _fixture.Create<DeleteCategoryCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CategoryErrors.DeleteCategoryInvalidData);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<DeleteCategoryCommand>()
              .With(x => x.CategoryId, Guid.NewGuid().ToString())
              .Create();

            var handler = new DeleteCategoryCommandHandler(
                _mockCategoryRepository.Object
            );

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as Category);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CategoryErrors.NotFindCategory);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCategoryIsDeleted()
        {
            // Arrange
            var request = _fixture.Build<DeleteCategoryCommand>()
              .With(x => x.CategoryId, Guid.NewGuid().ToString())
              .Create();

            var handler = _fixture.Create<DeleteCategoryCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().Be(1);
        }
    }
}


