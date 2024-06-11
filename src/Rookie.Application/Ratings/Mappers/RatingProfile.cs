using AutoMapper;
using Rookie.Application.Ratings.ViewModels;
using Rookie.Domain.RatingEntity;

namespace Rookie.Application.Ratings.Mappers
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, RatingVm>()
                .ForMember(des => des.UserName, act => act.MapFrom(src => src.ApplicationUser != null ? src.ApplicationUser.UserName : "null"))
                .ForMember(dest => dest.Rating, act => act.MapFrom(x => (int)x.Value))
                .ForMember(dest => dest.CreatedDate, act => act.MapFrom(x => x.CreatedDate))
                .ForMember(dest => dest.Comment, act => act.MapFrom(x => x.Comment));
        }
    }
}