using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Model.Context
{
    public class SQLContext : DbContext
    {
        public DbSet<OrderHeader> Headers { get; set; }
        public DbSet<OrderDetail> Details { get; set; }
        public SQLContext(DbContextOptions<SQLContext> options) : base(options)
        {

        }
        
    }
}
