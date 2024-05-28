using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Rookie.Application.Contracts.Infrastructure;
using Rookie.Domain.CartEntity;
using Rookie.Domain.ProductEntity;
using StackExchange.Redis;
namespace Rookie.Infrastructure.Carts
{
    public class CartService : ICartService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IDatabase _database;
        private readonly IProductRepository _productRepository;

        public CartService(IDistributedCache distributedCache, IDatabase database,
                            IProductRepository productRepository)
        {
            _distributedCache = distributedCache;
            _database = database;
            _productRepository = productRepository;
        }

        public async Task<Dictionary<string, int>> ChangeCartQuantity(string UserName, string ProductId, int Quantity)
        {
            ProductId = ProductId.ToUpper();
            var cart = await GetCartFromCacheAsync(UserName);
            var cacheKey = GetCacheKey(UserName);

            if (cart.ContainsKey(ProductId))
                cart[ProductId] += Quantity;
            else
                cart[ProductId] = Quantity;


            if (cart[ProductId] <= 0)
            {
                cart.Remove(ProductId);
                await _database.HashDeleteAsync(cacheKey, ProductId);
            }
            else
                await _database.HashSetAsync(cacheKey, ProductId, cart[ProductId]);


            await SetCartToCacheAsync(UserName, cart);

            return cart;
        }

        public async Task<Cart> GetCart(string UserName)
        {
            List<CartItem> items = new List<CartItem>();
            Dictionary<string, int> myDictionary = await GetCartFromCacheAsync(UserName);

            foreach (var temp in myDictionary)
            {
                var product = await _productRepository.GetOne(x => x.Id == new ProductId(temp.Key), "MainImage,Images");
                if (product != null)
                {
                    var cartItem = new CartItem
                    {
                        ProductId = product.Id.Value.ToString(),
                        ProductImage = product.MainImage.Image.Url,
                        ProductPrice = product.Price,
                        ProductName = product.ProductName,
                        Quantity = temp.Value,
                        TotalPrice = temp.Value * product.Price,
                    };
                    items.Add(cartItem);
                }
            }

            var cart = new Cart()
            {
                CartItems = items,
                TotalPrice = CalculateTotalPrice(items),
            };

            return cart;
        }

        private async Task SetCartToCacheAsync(string UserName, Dictionary<string, int> cartItems)
        {
            var cacheKey = GetCacheKey(UserName);

            foreach (var item in cartItems)
                await _database.HashSetAsync(cacheKey, item.Key.ToString(), item.Value);
        }

        private async Task<Dictionary<string, int>> GetCartFromCacheAsync(string UserName)
        {
            var cacheKey = GetCacheKey(UserName);
            var cachedCart = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedCart))
            {
                return JsonConvert.DeserializeObject<Dictionary<string, int>>(cachedCart);
            }

            var hashEntries = await _database.HashGetAllAsync(cacheKey);
            Dictionary<string, int> cart;

            if (hashEntries != null && hashEntries.Length != 0)
                cart = hashEntries.ToDictionary(
                       entry => (string)entry.Name,
                       entry => (int)entry.Value
                   );
            else
                cart = new Dictionary<string, int>();

            await SetCartToCacheAsync(UserName, cart);

            return cart;
        }

        private string GetCacheKey(string UserName) => $"cart:{UserName}";

        private decimal CalculateTotalPrice(List<CartItem> items) => items.Sum(item => item.TotalPrice);
    }
}