using Newtonsoft.Json;
using Rookie.Domain.Common;
using Rookie.Mvc.Areas.Customer.Models.Home;
using Rookie.Mvc.Utils;

namespace Rookie.Mvc.Services.Customer.Interface
{
  public class ProductService : IProductService
  {
    private readonly HttpClient _client;

    public ProductService(HttpClient client)
    {
      _client = client;
      _client.BaseAddress = Utilities.BASE_ADDRESS;

    }
    public async Task<ListProductResponse> GetAllProductsAsync()
    {
      var result = new ListProductResponse();
      HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/product/GetAllProducts");
      if (response.IsSuccessStatusCode)
      {
        string data = await response.Content.ReadAsStringAsync();

        //get data from headers
        string paginationHeader = response.Headers.GetValues("pagination").FirstOrDefault();
        MetaData paginationData = JsonConvert.DeserializeObject<MetaData>(paginationHeader);
        int curPage = paginationData.CurPage;
        int totalPage = paginationData.TotalPage;
        int pageSize = paginationData.PageSize;

        result.ProductVms = JsonConvert.DeserializeObject<List<ProductVm>>(data);
        result.CurPage = curPage;
        result.TotalPage = totalPage;
        result.PageSize = pageSize;
      }

      return result;

    }


    public async Task<ProductVm> GetProductById(string id)
    {
      ProductVm product = new ProductVm();
      HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/product/GetProductById?ProductId={id}");
      if (response.IsSuccessStatusCode)
      {
        string data = await response.Content.ReadAsStringAsync();
        product = JsonConvert.DeserializeObject<ProductVm>(data);
      }

      return product;
    }
  }
}