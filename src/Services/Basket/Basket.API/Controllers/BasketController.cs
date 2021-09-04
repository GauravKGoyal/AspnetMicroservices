using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Entities;
using Basket.API.Repositories;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName, CancellationToken cancellationToken)
        {
            return Ok(await _basketRepository.GetBasketAsync(userName, cancellationToken));
        }

        [HttpPut]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketAsync(shoppingCart.UserName, cancellationToken);
            if (basket == null) return NotFound();

            await _basketRepository.UpdateBasketAsync(shoppingCart, cancellationToken);
            return Ok(basket);

        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        public async Task<ActionResult> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
            await _basketRepository.DeleteBasketAsync(userName, cancellationToken);
            return Ok();
        }
    }
}
