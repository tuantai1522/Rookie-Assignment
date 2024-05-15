using MediatR;
using Rookie.Domain.CategoryEntity;
using Rookie.Domain.Common;
using Rookie.Domain.DomainError;

namespace Rookie.Application.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<int>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCategoryCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
                return Result.Failure<int>(CategoryErrors.DeleteCategoryInvalidData);

            var CategoryDeleted = await _categoryRepository.GetOne(x => x.Id.Equals(new CategoryId(request.CategoryId)));

            if (CategoryDeleted == null)
                return Result.Failure<int>(CategoryErrors.NotFindCategory);

            _categoryRepository.Delete(CategoryDeleted);

            return 1;
        }
    }
}