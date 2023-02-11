using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context
{
    public class SQLContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public SQLContext(DbContextOptions<SQLContext> options) : base(options)
        {

        }
        
    }
}
