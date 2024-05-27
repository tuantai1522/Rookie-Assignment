using AutoMapper;
using Rookie.Application.Carts.ViewModels;
using Rookie.Domain.CartEntity;

namespace Rookie.Application.Carts.Mappers
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartVm>()
                .ForMember(dest => dest.TotalPrice, act => act.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.CartItems, act => act.MapFrom(src => src.CartItems));

            CreateMap<CartItem, CartItemVm>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.ProductName, act => act.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.ProductPrice, act => act.MapFrom(src => src.ProductPrice))
                .ForMember(dest => dest.ProductImage, act => act.MapFrom(src => src.ProductImage))
                .ForMember(dest => dest.TotalPrice, act => act.MapFrom(src => src.TotalPrice));
        }
    }
}