using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Rookie.Domain.Tests;
using Rookie.WebApi.Controllers.Base;

namespace Rookie.WebApi.Tests.Controllers.Base
{
    public class BaseApiControllerTests : SetupTest
    {
        [Fact]
        public void Mediator_Should_Return_Resolved_IMediator_Instance()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton(_mockMediator.Object);

            var serviceProvider = services.BuildServiceProvider();

            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider
            };

            var controller = new TestableBaseApiController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };

            // Act
            var mediator = controller.GetMediator();

            // Assert
            Assert.NotNull(mediator);
            Assert.Equal(_mockMediator.Object, mediator);
        }

        private class TestableBaseApiController : BaseApiController
        {
            public IMediator GetMediator()
            {
                return Mediator;
            }
        }
    }
}