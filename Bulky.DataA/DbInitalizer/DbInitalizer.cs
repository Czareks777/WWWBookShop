using Azure.Identity;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataA.DbInitalizer
{
    public class DbInitalizer : IDbInitalizer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitalizer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(StaticDetails.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(StaticDetails.Role_Company)).GetAwaiter().GetResult();

                //if roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@dotnetmastery.com",
                    Email = "admin@dotnetmastery.com",
                    Name = "Cezary Sawicki",
                    PhoneNumber = "123321123",
                    StreetAddress = "test 123 Ave",
                    State = "Warm-Mazur",
                    PostalCode = "23422",
                    City = "Olsztyn"
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@dotnetmastery.com");
                _userManager.AddToRoleAsync(user, StaticDetails.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }

    }
}
