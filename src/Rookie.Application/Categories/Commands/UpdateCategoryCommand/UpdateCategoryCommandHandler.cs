using AutoMapper;
using MediatR;
using Rookie.Application.Categories.ViewModels;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;
using Rookie.Application.Contracts.Persistence;

namespace Rookie.Application.Categories.Commands.UpdateCategoryCommand
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryVm>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<CategoryVm>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCategoryCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<CategoryVm>(CategoryErrors.UpdateCategoryInvalidData);

            var CategoryUpdated = new Category
            {
                Id = new CategoryId(request.Id),
                Name = request.CategoryName,
                Description = request.Description,
            };

            var temp = await _categoryRepository.Update(CategoryUpdated);

            if (temp == true)
                // map data from Product to ProductVm wich is defined in Mappers
                return _mapper.Map<Category, CategoryVm>(CategoryUpdated);
            else
                return Result.Failure<CategoryVm>(CategoryErrors.NotFindCategory);
        }
    }
}