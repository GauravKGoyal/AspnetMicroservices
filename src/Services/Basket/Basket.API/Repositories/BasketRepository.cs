using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _distributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken)
        {
            var shoppingCart = await _distributedCache.GetStringAsync(userName, cancellationToken);

            return string.IsNullOrEmpty(shoppingCart) ? 
                null : 
                JsonConvert.DeserializeObject<ShoppingCart>(shoppingCart);
        }

        public Task UpdateBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            return _distributedCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart), cancellationToken);
        }

        public Task DeleteBasketAsync(string userName, CancellationToken cancellationToken)
        {
            return _distributedCache.RemoveAsync(userName, cancellationToken);
        }
    }
}
