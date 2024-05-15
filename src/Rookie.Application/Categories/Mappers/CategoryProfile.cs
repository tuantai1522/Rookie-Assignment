using AutoMapper;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;

namespace Rookie.Application.Categories.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryVm>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id.Value))
                .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
                .ForMember(des => des.Description, act => act.MapFrom(src => src.Description));
        }
    }
}