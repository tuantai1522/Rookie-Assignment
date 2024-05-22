using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Infrastructure.Images;
using Rookie.Infrastructure.Token;

namespace Rookie.Infrastructure
{
    public static class Extension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity(config);

            services.AddService();

            services.AddRedis(config);

        }

        private static void AddIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(option =>
                    {
                        option.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(config["JWTSettings:TokenKey"])
                            )
                        };
                    });
            services.AddAuthorization();
        }
        private static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ITokenService, TokenService>();

        }
        private static void AddRedis(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                string connection = config.GetConnectionString("Redis");
                redisOptions.Configuration = connection;
            });
        }
    }
}