using AutoMapper;
using Rookie.Application.MainImages.ViewModels;
using Rookie.Domain.MainImageEntity;

namespace Rookie.Application.MainImages.Mappers
{
    public class MainImageProfile : Profile
    {
        public MainImageProfile()
        {
            CreateMap<MainImage, MainImageVm>()
                .ForMember(des => des.ProductId, act => act.MapFrom(src => src.ProductId.Value))
                .ForMember(des => des.ImageId, act => act.MapFrom(src => src.ImageId.Value));
        }
    }
}