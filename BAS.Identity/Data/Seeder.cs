using BAS.AppCommon;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Identity
{
    public class Seeder
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<long>> roleManager;
        private readonly IdentityContext identityContext;

        public Seeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<long>> roleManager, IdentityContext identityContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.identityContext = identityContext;
        }

        public async Task Seed()
        {
            identityContext.Database.Migrate();

            using(var transaction = await identityContext.Database.BeginTransactionAsync())
            {
                if(!identityContext.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(UserRole.Admin.ToString()));
                    await roleManager.CreateAsync(new IdentityRole<long>(UserRole.User.ToString()));
                }

                if(!identityContext.Users.Any())
                {
                    var user1 = new ApplicationUser()
                    {
                        Name = "Administrator",
                        Surname = "Administrator",
                        Email = "nie@tup.ru",
                        EmailConfirmed = true,
                        UserName = "Admin",
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    await userManager.CreateAsync(user1, "Admin123!");
                    await userManager.AddToRoleAsync(user1, UserRole.Admin.ToString());
                }

                transaction.Commit();
            }
        }
    }
}
