using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Infrastructure.Images;

namespace Rookie.Infrastructure
{
    public static class Extension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IImageService, ImageService>();

        }
    }
}