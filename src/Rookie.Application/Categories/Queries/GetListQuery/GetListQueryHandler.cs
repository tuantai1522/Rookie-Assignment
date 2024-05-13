using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Categories.ViewModels;
using Rookie.Application.Interface;
using Rookie.Domain.CategoryEntity;

namespace Rookie.Application.Categories.Queries.GetListQuery
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, IEnumerable<CategoryVm>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CategoryVm>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.ToListAsync();

            // map data from Course to CourseVm wich is defined in Mappers
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryVm>>(categories);
        }

    }
}