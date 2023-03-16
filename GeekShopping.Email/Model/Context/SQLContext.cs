using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Model.Context
{
    public class SQLContext : DbContext
    {
        public DbSet<EmailLog> EmailLogs { get; set; }
        public SQLContext(DbContextOptions<SQLContext> options) : base(options)
        {

        }
        
    }
}
