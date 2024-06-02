using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rookie.Application.Contracts.Persistence;
using Rookie.Application.Interface;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.ProductEntity;
using Rookie.Persistence.Repositories;


namespace Rookie.Persistence
{
    public static class Extension
    {
        public static void AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services
                .AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IMainImageRepository, MainImageRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<ApplicationDbContextInitializer>();


        }
    }
}