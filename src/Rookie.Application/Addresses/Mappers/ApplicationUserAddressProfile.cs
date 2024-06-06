using AutoMapper;
using Rookie.Application.Addresses.ViewModels;
using Rookie.Domain.ApplicationUserEntity;

namespace Rookie.Application.Addresses.Mappers
{
    public class ApplicationUserAddressProfile : Profile
    {
        public ApplicationUserAddressProfile()
        {
            CreateMap<ApplicationUserAddress, ApplicationUserAddressVm>()
                .ForMember(des => des.UserName, act => act.MapFrom(src => src.ApplicationUser != null ? src.ApplicationUser.UserName : string.Empty))
                .ForMember(dest => dest.Address, act => act.MapFrom(src => src.Address));
        }
    }
}