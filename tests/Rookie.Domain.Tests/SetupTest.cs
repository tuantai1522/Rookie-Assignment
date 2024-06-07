using AutoMapper;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Application.Contracts.Persistence;
using Rookie.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Carts.Mappers;
using Rookie.Application.Categories.Mappers;
using Rookie.Application.MainImages.Mappers;
using Rookie.Application.Orders.Mappers;
using Rookie.Application.PagedList;
using Rookie.Application.Products.Mappers;
using Rookie.Application.Users.Mappers;
using Moq;
using Rookie.Persistence.Repositories;
using Microsoft.Data.SqlClient;
using MediatR;
using AutoFixture;



namespace Rookie.Domain.Tests
{
    public class SetupTest : IDisposable
    {

        protected readonly Mock<IMapper> _mockMapper;

        protected readonly ApplicationDbContext _dbContext;

        protected readonly Mock<IMediator> _mockMediator;

        protected readonly Fixture _fixture;



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

            _fixture = new Fixture();
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
