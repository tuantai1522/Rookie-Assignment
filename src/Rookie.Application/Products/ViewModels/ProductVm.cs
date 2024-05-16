using Rookie.Domain.Common;
using Rookie.Domain.ProductEntity;

namespace Rookie.Application.Products.ViewModels
{
    public sealed record ProductVm(
        ProductId Id,
        string ProductName,
        string Description,
        string Images,
        decimal Price,
        string CategoryName
    );
}