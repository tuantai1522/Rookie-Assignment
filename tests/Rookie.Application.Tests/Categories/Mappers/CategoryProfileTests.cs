using AutoFixture;
using FluentAssertions;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Categories.Mappers
{
    public class CategoryProfileTests : SetupProfileTest
    {
        [Fact]
        public void TestCategoryVmMapper()
        {
            var categoryMock = _fixture.Build<Category>()
                                    .Create();

            var result = _mapper.Map<CategoryVm>(categoryMock);

            result.Id.Should().Be(categoryMock.Id.ToString());

        }
    }
}
