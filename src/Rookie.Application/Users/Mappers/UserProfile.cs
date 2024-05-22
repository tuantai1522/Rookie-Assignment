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
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.FirstName, act => act.MapFrom(src => src.FirstName))
                .ForMember(des => des.LastName, act => act.MapFrom(src => src.LastName))
                .ForMember(des => des.UserName, act => act.MapFrom(src => src.UserName))
                .ForMember(des => des.Email, act => act.MapFrom(src => src.Email))
                .ForMember(des => des.ApplicationUserAddresses, act => act.MapFrom(src => src.ApplicationUserAddresses));
        }
    }
}