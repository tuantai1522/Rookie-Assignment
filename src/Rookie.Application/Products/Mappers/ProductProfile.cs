using AutoMapper;
using Rookie.Application.Images.ViewModels;
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
                .ForMember(des => des.QuantityInStock, act => act.MapFrom(src => src.QuantityInStock))
                .ForMember(des => des.MainImageUrl, act => act.MapFrom(src => src.MainImage != null ? src.MainImage.Image.Url : string.Empty))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images != null
                ? src.Images.Select(img => new ImageProductVm
                {
                    ImageId = img.Id.ToString(),
                    Url = img.Url
                }).ToList()
                : new List<ImageProductVm>()))
                .ForMember(des => des.Price, act => act.MapFrom(src => src.Price))
                .ForMember(des => des.CategoryName, act => act.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));
        }
    }
}