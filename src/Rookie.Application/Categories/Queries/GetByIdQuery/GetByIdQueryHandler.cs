using AutoMapper;
using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;

namespace Rookie.Application.Categories.Queries.GetByIdQuery
{
    //to define fields or properperties needed to handle this GetByIdQuery
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, CategoryVm>
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;
        public GetByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryVm> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetByIdQueryValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                throw new Exception("Error when fetching data");

            var category = await _categoryRepository.GetOne(x => x.Id.Equals(request.Id), includeProperties: "Products");

            // map data from Course to CourseVm wich is defined in Mappers
            return _mapper.Map<Category, CategoryVm>(category);

        }
    }
}