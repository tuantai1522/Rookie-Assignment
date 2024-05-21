using AutoMapper;
using Rookie.Domain.Common;

namespace Rookie.Application.PagedList
{
    public class PagedListProfile : Profile
    {
        public PagedListProfile()
        {
            CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));
        }
    }

    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            // Map the items in the PagedList
            var mappedItems = context.Mapper.Map<List<TDestination>>(source);

            // Create a new PagedList<TDestination> with the mapped items and meta data
            return new PagedList<TDestination>(mappedItems, source.MetaData.TotalCount, source.MetaData.CurPage, source.MetaData.PageSize);
        }
    }
}