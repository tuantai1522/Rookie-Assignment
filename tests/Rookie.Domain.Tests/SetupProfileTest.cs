using AutoFixture;
using AutoMapper;
using Rookie.Application.Carts.Mappers;
using Rookie.Application.Categories.Mappers;
using Rookie.Application.MainImages.Mappers;
using Rookie.Application.Orders.Mappers;
using Rookie.Application.PagedList;
using Rookie.Application.Products.Mappers;
using Rookie.Application.Users.Mappers;

namespace Rookie.Domain.Tests
{
    public class SetupProfileTest
    {
        protected readonly Fixture _fixture;

        protected readonly IMapper _mapper;

        public SetupProfileTest()
        {
            _fixture = new Fixture();

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PagedListProfile());

                cfg.AddProfile<CategoryProfile>();
                cfg.AddProfile<ProductProfile>();
                cfg.AddProfile<MainImageProfile>();

                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<CartProfile>();

                cfg.AddProfile<OrderProfile>();

            });
            _mapper = mappingConfig.CreateMapper();
        }

    }
}
