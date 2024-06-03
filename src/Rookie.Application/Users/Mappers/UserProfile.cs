using AutoMapper;
using Rookie.Application.Users.ViewModels;
using Rookie.Domain.ApplicationUserEntity;

namespace Rookie.Application.Users.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserLoginVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ApplicationUserAddresses, opt => opt.MapFrom(src => src.ApplicationUserAddresses))
                .ForMember(dest => dest.Token, opt => opt.Ignore());


            CreateMap<ApplicationUser, UserRegisterVm>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

            CreateMap<ApplicationUserAddress, UserAddressVm>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        }
    }
}