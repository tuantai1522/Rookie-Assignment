using Moq;
using Rookie.Application.Categories.Queries.GetByIdQuery;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.DomainError;
using Rookie.Domain.Tests;
using System.Linq.Expressions;
using AutoFixture;
using FluentAssertions;


namespace Rookie.Application.Tests.Categories.Queries
{
    public class GetByIdQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsFailureResult_WhenRequestIsInValid()
        {
            // Arrange
            var request = _fixture.Build<GetByIdQuery>()
              .With(x => x.Id, "")
              .Create();

            var handler = _fixture.Create<GetByIdQueryHandler>();


            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(false);
            result.Error.Should().Be(CategoryErrors.GetCategoryByIdInvalidData);
        }

        [Fact]
        public async Task ReturnsFailureResult_WhenCategoryIsNotFound()
        {
            // Arrange
            var request = _fixture.Build<GetByIdQuery>()
              .With(x => x.Id, Guid.NewGuid().ToString())
              .Create();

            _mockCategoryRepository.Setup(repo => repo.GetOne(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(null as Category);

            var handler = new GetByIdQueryHandler(
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
        public async Task ReturnsSuccessResult_WhenCategoryIsFound()
        {
            // Arrange
            var request = _fixture.Build<GetByIdQuery>()
              .With(x => x.Id, Guid.NewGuid().ToString())
              .Create();

            var handler = _fixture.Create<GetByIdQueryHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);
            result.Value.Should().NotBeNull();

        }
    }
}
