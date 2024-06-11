using AutoMapper;
using Rookie.Application.Ratings.ViewModels;
using Rookie.Domain.RatingEntity;

namespace Rookie.Application.Ratings.Mappers
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<List<Rating>, RatingVm>()
                .ForMember(dest => dest.ProductName, opt => opt.Ignore())
                .ForMember(dest => dest.UserNames, opt => opt.MapFrom(src => src.Select(r => r.ApplicationUser != null ? r.ApplicationUser.UserName : string.Empty).ToList()))
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Select(r => r.Comment ?? string.Empty).ToList()));
        }
    }
}