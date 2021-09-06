using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discount.API.Data;
using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/Discount")]
    public class DiscountController : ControllerBase
    {
        private readonly DiscountAPIContext _context;

        public DiscountController(DiscountAPIContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscountAsync(string productName)
        {
            var coupon = await _context.Coupon.FirstOrDefaultAsync(x => x.ProductName == productName);

            if (coupon == null)
            {
                return NotFound();
            }

            return coupon;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscountAsync([FromBody] Coupon coupon)
        {
            _context.Coupon.Add(coupon);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        // This intentionally doesn't follow rest principles
        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateDiscountAsync([FromBody] Coupon coupon)
        {
            _context.Entry(coupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponExists(coupon.ProductName))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        private bool CouponExists(string productName)
        {
            return _context.Coupon.Any(e => e.ProductName == productName);
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteDiscount(string productName)
        {
            var coupon = new Coupon{ ProductName = productName };
            _context.Entry(coupon).State = EntityState.Deleted;

            _context.Coupon.Remove(coupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
