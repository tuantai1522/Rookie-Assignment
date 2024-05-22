using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rookie.Application.Categories.Mappers;
using Rookie.Application.MainImages.Mappers;
using Rookie.Application.PagedList;
using Rookie.Application.Products.Mappers;
using Rookie.Application.Users.Mappers;

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
                    cfg.AddProfile(new PagedListProfile());

                    cfg.AddProfile<CategoryProfile>();
                    cfg.AddProfile<ProductProfile>();
                    cfg.AddProfile<MainImageProfile>();

                    cfg.AddProfile<UserProfile>();
                });
                return config.CreateMapper();
            });

            //Add mediar into my project
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

    }
}