using AutoFixture;
using FluentAssertions;
using Moq;
using Rookie.Application.Carts.Queries.GetCartByUserNameQuery;
using Rookie.Application.Categories.Commands.CreateCategoryCommand;
using Rookie.Application.Products.Commands.CreateProductCommand;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Categories.Commands
{
    public class CreateCategoryCommandHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<CreateCategoryCommand>()
                  .With(x => x.CategoryName, "")
                  .Create();

            var handler = _fixture.Create<CreateCategoryCommandHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CategoryErrors.CreateCategoryInvalidData);
        }

        [Fact]
        public async Task ReturnsSuccessResult_WhenCategoryIsCreated()
        {
            // Arrange
            var request = _fixture.Create<CreateCategoryCommand>();

            var handler = _fixture.Create<CreateCategoryCommandHandler>();

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
