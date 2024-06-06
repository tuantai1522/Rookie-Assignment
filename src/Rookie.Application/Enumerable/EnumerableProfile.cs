using AutoMapper;
using Rookie.Application.PagedList;
using Rookie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Enumerable
{
    public class EnumerableProfile : Profile
    {
        public EnumerableProfile()
        {
            CreateMap(typeof(IEnumerable<>), typeof(IEnumerable<>)).ConvertUsing(typeof(EnumerableConverter<,>));
        }
    }

    public class EnumerableConverter<TSource, TDestination> : ITypeConverter<IEnumerable<TSource>, IEnumerable<TDestination>>
    {
        public IEnumerable<TDestination> Convert(IEnumerable<TSource> source, IEnumerable<TDestination> destination, ResolutionContext context)
        {
            // Map the items in the source enumerable to a list of destination items
            var mappedItems = context.Mapper.Map<List<TDestination>>(source);

            // Return the mapped items as an IEnumerable<TDestination>
            return mappedItems;
        }
    }
}
