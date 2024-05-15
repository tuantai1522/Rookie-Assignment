using Rookie.Domain.CategoryEntity;

namespace Rookie.Application.Categories.ViewModels
{
    public sealed record CategoryVm(
        CategoryId Id,
        string Name,
        string Description
    );
}