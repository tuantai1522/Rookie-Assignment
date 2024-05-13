using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rookie.Application.Categories.Mappers;

namespace Rookie.Application
{
    public static class Extension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<CategoryProfile>();
                });
                return config.CreateMapper();
            });

            //Add mediar into my project
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

    }
}