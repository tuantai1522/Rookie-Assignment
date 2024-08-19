using Microsoft.AspNetCore.Mvc;
using Rookie.Mvc.Areas.Customer.Models.Home;
using Rookie.Mvc.Services.Customer.Interface;
namespace Rookie.Mvc.Areas.Customer.Controllers.Home
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService,
                                ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //call products
            ListProductResponse response = await _productService.GetAllProductsAsync();
            if (response.ProductVms != null)
            {
                ViewData["curPage"] = response.CurPage;
                ViewData["totalPage"] = response.TotalPage;
                ViewData["pageSize"] = response.PageSize;
            }

            //call categories
            List<CategoryVm> categoryVms = await _categoryService.GetAllCategories();
            if (categoryVms != null)
                ViewData["categoryList"] = categoryVms;

            return View(response.ProductVms);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            ProductVm product = await _productService.GetProductById(id);
            if (product == null)
                return View();

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}