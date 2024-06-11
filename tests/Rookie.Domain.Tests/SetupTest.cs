using AutoMapper;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Data.SqlClient;
using MediatR;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Identity;
using Rookie.Domain.ApplicationUserEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.EntityFrameworkCore.Storage;
using CloudinaryDotNet;


namespace Rookie.Domain.Tests
{
    public class SetupTest : IDisposable
    {
        protected readonly Mock<IMapper> _mockMapper;

        protected readonly ApplicationDbContext _dbContext;

        protected readonly Mock<IMediator> _mockMediator;

        protected readonly IFixture _fixture;

        protected readonly Mock<ICategoryRepository> _mockCategoryRepository;
        protected readonly Mock<IProductRepository> _mockProductRepository;
        protected readonly Mock<IImageRepository> _mockImageRepository;
        protected readonly Mock<IMainImageRepository> _mockMainImageRepository;
        protected readonly Mock<IOrderRepository> _mockOrderRepository;
        protected readonly Mock<IUserRepository> _mockUserRepository;

        protected readonly Mock<IAddressRepository> _mockAddressRepository;

        protected readonly Mock<ICartService> _mockCartService;
        protected readonly Mock<IImageService> _mockImageService;
        protected readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;

        public SetupTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMapper = new Mock<IMapper>();

            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));


            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "data source=.; initial catalog=Rookie; integrated security=true; Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseSqlServer(conn)
                            .Options;
            _dbContext = new ApplicationDbContext(options);

            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockImageRepository = new Mock<IImageRepository>();
            _mockMainImageRepository = new Mock<IMainImageRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockCartService = new Mock<ICartService>();
            _mockImageService = new Mock<IImageService>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }



    }
}
