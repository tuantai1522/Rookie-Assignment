using AutoMapper;
using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.Common;
using Rookie.Application.Products.ViewModels;

namespace Rookie.Application.Categories.Queries.GetListQuery
{
    public class GetListQueryHandler : IRequestHandler<GetListQuery, Result<IEnumerable<CategoryVm>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        public GetListQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<CategoryVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll(null, includeProperties: "Products");

            var categoryVms = _mapper.Map<IEnumerable<CategoryVm>>(categories);

            return Result.Success(categoryVms);
        }
    }
}