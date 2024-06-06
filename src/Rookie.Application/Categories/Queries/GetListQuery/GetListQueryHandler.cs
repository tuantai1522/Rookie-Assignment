using AutoMapper;
using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Application.Contracts.Persistence;

namespace Rookie.Application.Categories.Queries.GetListQuery
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, IEnumerable<CategoryVm>>
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        public GetListQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryVm>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll(null, includeProperties: "Products");

            // map data from Course to CourseVm wich is defined in Mappers
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryVm>>(categories);
        }

    }
}