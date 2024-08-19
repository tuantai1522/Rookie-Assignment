using Newtonsoft.Json;
using Rookie.Mvc.Areas.Customer.Models.Home;
using Rookie.Mvc.Services.Customer.Interface;
using Rookie.Mvc.Utils;

namespace Rookie.Mvc.Services.Customer.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;

        public CategoryService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = Utilities.BASE_ADDRESS;

        }

        public async Task<List<CategoryVm>> GetAllCategories()
        {
            List<CategoryVm> categoryVms = new List<CategoryVm>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/category/GetAllCategories");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();

                categoryVms = JsonConvert.DeserializeObject<List<CategoryVm>>(data);
            }

            return categoryVms;
        }
    }
}