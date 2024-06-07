using AutoFixture;
using FluentAssertions;
using Moq;
using Rookie.Application.Categories.Queries.GetListQuery;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Categories.Queries
{
    public class GetListQueryHandlerTests : SetupTest
    {
        [Fact]
        public async Task ReturnsSuccessResult_WhenRequestIsValid()
        {
            // Arrange

            var request = _fixture.Create<GetListQuery>();

            var handler = _fixture.Create<GetListQueryHandler>();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().Be(true);

        }
    }
}
