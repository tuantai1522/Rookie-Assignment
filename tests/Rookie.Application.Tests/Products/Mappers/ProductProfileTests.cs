using AutoFixture;
using FluentAssertions;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.ProductEntity;
using Rookie.Domain.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Tests.Products.Mappers
{
    public class ProductProfileTests : SetupProfileTest
    {
        [Fact]
        public void TestProductVmMapper()
        {
            var productMock = _fixture.Build<Product>()
                                    .Without(x => x.Category)
                                    .Without(x => x.MainImage)
                                    .Without(x => x.Images)
                                    .Without(x => x.OrderItems)
                                    .Create();

            var result = _mapper.Map<ProductVm>(productMock);

            result.Id.Should().Be(productMock.Id.ToString());
        }
    }
}
