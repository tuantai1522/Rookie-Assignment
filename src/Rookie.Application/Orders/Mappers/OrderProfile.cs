using AutoMapper;
using Rookie.Application.Orders.ViewModels;
using Rookie.Domain.OrderEntity;

namespace Rookie.Application.Orders.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.UserName, act => act.MapFrom(src => src.ApplicationUser != null ? src.ApplicationUser.UserName : string.Empty))
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal))
                .ForMember(dest => dest.DeliveryFee, opt => opt.MapFrom(src => src.DeliveryFee))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()));
        }
    }
}