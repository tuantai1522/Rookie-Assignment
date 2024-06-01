using Rookie.Domain.CartEntity;

namespace Rookie.Application.Contracts.Infrastructure
{
    public interface ICartService
    {
        Task<Dictionary<string, int>> ChangeCartQuantity(string UserName, string ProductId, int Quantity);
        Task<Cart> GetCart(string UserName);
        Task ClearCart(string UserName);
    }
}