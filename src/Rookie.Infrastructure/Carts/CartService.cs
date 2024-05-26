using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Rookie.Application.Contracts.Infrastructure;
using StackExchange.Redis;
namespace Rookie.Infrastructure.Carts
{
    public class CartService : ICartService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IDatabase _database;

        public CartService(IDistributedCache distributedCache, IDatabase database)
        {
            _distributedCache = distributedCache;
            _database = database;
        }

        public async Task<Dictionary<string, int>> ChangeCartQuantity(string UserId, string ProductId, int Quantity)
        {
            var cart = await GetCartFromCacheAsync(UserId);

            if (cart.ContainsKey(ProductId))
                cart[ProductId] += Quantity;
            else
                cart[ProductId] = Quantity;


            if (cart[ProductId] <= 0)
            {
                cart.Remove(ProductId);
                await _database.HashDeleteAsync(UserId, ProductId);
            }
            else
                await _database.HashSetAsync(UserId, ProductId, cart[ProductId]);


            await SetCartToCacheAsync(UserId, cart);

            return cart;
        }

        public async Task SetCartToCacheAsync(string UserId, Dictionary<string, int> cart)
        {
            var cacheKey = GetCacheKey(UserId);
            var serializedCart = JsonConvert.SerializeObject(cart);
            await _distributedCache.SetStringAsync(cacheKey, serializedCart);
        }

        public async Task<Dictionary<string, int>> GetCartFromCacheAsync(string UserId)
        {
            var cacheKey = GetCacheKey(UserId);
            var cachedCart = await _distributedCache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedCart))
            {
                return JsonConvert.DeserializeObject<Dictionary<string, int>>(cachedCart);
            }

            var hashEntries = await _database.HashGetAllAsync(UserId);
            Dictionary<string, int> cart;

            if (hashEntries != null && hashEntries.Length != 0)
                cart = hashEntries.ToDictionary(
                       entry => (string)entry.Name,
                       entry => (int)entry.Value
                   );
            else
                cart = new Dictionary<string, int>();

            await SetCartToCacheAsync(UserId, cart);

            return cart;
        }

        private string GetCacheKey(string UserId) => $"cart:{UserId}";
    }
}