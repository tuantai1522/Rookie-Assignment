using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Domain.Common;
using Rookie.Application.Categories.ViewModels;
using Rookie.Application.Products.ViewModels;
using PagedList;
namespace Rookie.Mvc.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly Uri baseAddress = new Uri("http://localhost:5201/api");

        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string KeyWord, string OrderBy, string[] CategoryType,
                                                string PageNumber = "1", string PageSize = "6")
        {
            //call products
            string CategoryTypeFormat = string.Join(",", CategoryType ?? Enumerable.Empty<string>());

            List<ProductVm> productList = new List<ProductVm>();
            HttpResponseMessage response1 = await _client.GetAsync(_client.BaseAddress +
                        $"/product?OrderBy={OrderBy}&KeyWord={KeyWord}&CategoryType={CategoryTypeFormat}&PageNumber={PageNumber}&PageSize={PageSize}");

            if (response1.IsSuccessStatusCode)
            {
                string data = await response1.Content.ReadAsStringAsync();
                productList = JsonConvert.DeserializeObject<List<ProductVm>>(data);

                //get info of pagination from current api
                string paginationHeader = response1.Headers.GetValues("pagination").FirstOrDefault();
                MetaData paginationData = JsonConvert.DeserializeObject<MetaData>(paginationHeader);
                int curPage = paginationData.CurPage;
                int totalPage = paginationData.TotalPage;
                int pageSize = paginationData.PageSize;
                ViewData["curPage"] = curPage;
                ViewData["totalPage"] = totalPage;
                ViewData["pageSize"] = pageSize;

            }

            //call categories
            List<CategoryVm> categoryList = new List<CategoryVm>();
            HttpResponseMessage response2 = await _client.GetAsync(_client.BaseAddress + "/category");
            if (response2.IsSuccessStatusCode)
            {
                string data = await response2.Content.ReadAsStringAsync();
                categoryList = JsonConvert.DeserializeObject<List<CategoryVm>>(data);
            }
            ViewData["categoryList"] = categoryList;


            return View(productList);
        }
    }
}