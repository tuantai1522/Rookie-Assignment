using Rookie.Mvc.Implementation;
using Rookie.Mvc.Interface;
using Rookie.Mvc.Services.Customer.Implementation;
using Rookie.Mvc.Services.Customer.Interface;

namespace Rookie.Mvc.Extension
{
    public static class ClientExtension
    {
        public static void AddClient(this IServiceCollection services)
        {
            // Register the user service
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddHttpContextAccessor();

            services.AddHttpClient<IProductService, ProductService>();
            services.AddHttpClient<ICategoryService, CategoryService>();
        }

    }
}