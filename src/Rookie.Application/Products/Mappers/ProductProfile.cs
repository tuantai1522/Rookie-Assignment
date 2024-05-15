using AutoMapper;
using Rookie.Application.Products.ViewModels;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductVm>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id.Value))
                .ForMember(des => des.ProductName, act => act.MapFrom(src => src.ProductName))
                .ForMember(des => des.Description, act => act.MapFrom(src => src.Description))
                .ForMember(des => des.Price, act => act.MapFrom(src => src.Price))
                .ForMember(des => des.Images, act => act.MapFrom(src => src.Images))
                .ForMember(des => des.CategoryName, act => act.MapFrom(src => src.Category!.Name));
        }
    }
}