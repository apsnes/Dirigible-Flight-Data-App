using Microsoft.AspNetCore.Identity;
using FlightApp.Database;


using Microsoft.EntityFrameworkCore;
using FlightApp.Helpers;

namespace FlightApp.Service
{
    public class DbInitialiser : IDbInitialiser
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly FlightAppDbContext _db;
        public DbInitialiser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, FlightAppDbContext db)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(StaticRoles.Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(StaticRoles.Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(StaticRoles.Customer)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }
                IdentityUser user = new()
                {
                    UserName = "bob",
                    Email = "bob@gmail.com",
                    EmailConfirmed = true,
                };
                _userManager.CreateAsync(user, "Admin123!").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, StaticRoles.Admin).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
