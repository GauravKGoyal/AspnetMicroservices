using Microsoft.EntityFrameworkCore;
using Discount.API.Entities;

namespace Discount.API.Data
{
    public class DiscountAPIContext : DbContext
    {
        public DiscountAPIContext (DbContextOptions<DiscountAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>()
                .HasIndex(c => new { c.ProductName, c.From });
        }

        public DbSet<Coupon> Coupon { get; set; }
    }
}
