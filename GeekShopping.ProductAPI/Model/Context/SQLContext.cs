using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
{
    public class SQLContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public SQLContext(DbContextOptions<SQLContext> options) : base(options)
        {

        }
        
    }
}
