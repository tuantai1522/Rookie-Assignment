using AutoMapper;
using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;

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

            return Result.Success(_mapper.Map<IEnumerable<Category>, IEnumerable<CategoryVm>>(categories));
        }
    }
}