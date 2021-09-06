using System.Threading;
using System.Threading.Tasks;
using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken);
        Task UpdateBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken);
        Task DeleteBasketAsync(string userName, CancellationToken cancellationToken);
    }
}