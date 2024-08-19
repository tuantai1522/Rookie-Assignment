using Rookie.Mvc.Areas.Customer.Models.Home;

namespace Rookie.Mvc.Services.Customer.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryVm>> GetAllCategories();
    }
}