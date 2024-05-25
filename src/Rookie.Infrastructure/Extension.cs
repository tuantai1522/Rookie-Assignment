using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Infrastructure.Images;
using Rookie.Infrastructure.Security.TokenGenerator;

namespace Rookie.Infrastructure
{
    public static class Extension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(config);

            services.AddService();

            services.AddRedis(config);

        }

        private static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        }
        private static void AddRedis(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                string connection = config.GetConnectionString("Redis");
                redisOptions.Configuration = connection;
            });
        }
        private static void AddAuthentication(this IServiceCollection services,
                                              IConfiguration config)
        {
            var JwtSettings = new JwtSettings();
            config.Bind(JwtSettings.Section, JwtSettings);

            services.AddSingleton(Options.Create(JwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                    {
                        option.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = JwtSettings.Issuer,
                            ValidAudience = JwtSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(JwtSettings.Secret
                            ))
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));
            });

        }
    }
}