using System.Security.Claims;
using IdentityModel;
using IdentityServer.Configuration;
using IdentityServer.Model;
using IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly SQLContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(SQLContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> role)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _role = role ?? throw new ArgumentNullException(nameof(role));
        }

        public void Initialize()
        {
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();
            ApplicationUser admin = new ApplicationUser()
            {
                UserName="pedro-admin",
                Email="pedro-admin@silva.com.br",
                EmailConfirmed = true,
                PhoneNumber="+55 (11) 91234-5678",
                FirstName="Pedro",
                LastName="Admin"
            };
            _user.CreateAsync(admin, "Pedro123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();
            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name,$"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName,admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName,admin.LastName),
                new Claim(JwtClaimTypes.Role,IdentityConfiguration.Admin)
            }).Result;
            ApplicationUser client = new ApplicationUser()
            {
                UserName="pedro-client",
                Email="pedro-client@silva.com.br",
                EmailConfirmed = true,
                PhoneNumber="+55 (11) 91234-5678",
                FirstName="Pedro",
                LastName="Client"
            };
            _user.CreateAsync(client, "Pedro123$").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();
            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name,$"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName,client.FirstName),
                new Claim(JwtClaimTypes.FamilyName,client.LastName),
                new Claim(JwtClaimTypes.Role,IdentityConfiguration.Client)
            }).Result;
        }
    }
}
