namespace Rookie.Application.Contracts.Infrastructure
{
    public interface ICartService
    {
        Task<Dictionary<string, int>> ChangeCartQuantity(string userId, string productId, int quantity);
        Task SetCartToCacheAsync(string userId, Dictionary<string, int> cart);
        Task<Dictionary<string, int>> GetCartFromCacheAsync(string userId);
    }
}