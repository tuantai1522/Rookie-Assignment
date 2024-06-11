using AutoMapper;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.OrderEntity;
using Rookie.Domain.RatingEntity;

namespace Rookie.Application.Orders.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.ApplicationUser != null ? src.ApplicationUser.UserName : string.Empty))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal))
                .ForMember(dest => dest.DeliveryFee, opt => opt.MapFrom(src => src.DeliveryFee))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems != null ? src.OrderItems : new List<OrderItem>()));

            CreateMap<OrderItem, OrderItemVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : string.Empty))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Rating != null ? src.Rating.Value : 0));
        }
    }
}