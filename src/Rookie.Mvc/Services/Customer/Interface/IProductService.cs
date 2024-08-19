using Rookie.Mvc.Areas.Customer.Models.Home;

namespace Rookie.Mvc.Services.Customer.Interface
{
    public interface IProductService
    {
        Task<ListProductResponse> GetAllProductsAsync();
        Task<ProductVm> GetProductById(string id);
    }
}