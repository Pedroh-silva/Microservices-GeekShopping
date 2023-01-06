using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Model.Context
{
    public class SQLContext : IdentityDbContext<ApplicationUser>
    {
        public SQLContext(DbContextOptions<SQLContext> options) :base(options){ }
    }
}
