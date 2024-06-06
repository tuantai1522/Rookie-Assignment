using AutoMapper;
using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Application.Contracts.Persistence;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Categories.Queries.GetByIdQuery
{
    //to define fields or properperties needed to handle this GetByIdQuery
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Result<CategoryVm>>
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        public GetByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<CategoryVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetByIdQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<CategoryVm>(CategoryErrors.GetCategoryByIdInvalidData);

            var category = await _categoryRepository.GetOne(x => x.Id.Equals(new CategoryId(request.Id)), includeProperties: "Products");

            if (category != null)
                // map data from Course to CourseVm wich is defined in Mappers
                return _mapper.Map<Category, CategoryVm>(category);
            else
                return Result.Failure<CategoryVm>(CategoryErrors.NotFindCategory);
        }
    }
}