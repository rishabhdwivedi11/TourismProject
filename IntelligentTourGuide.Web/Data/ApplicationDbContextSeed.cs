using IntelligentTourGuide.Web.Migrations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTourGuide.Web.Data
{
    // A singleton Class
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedIdentityRolesAsync(RoleManager<IdentityRole> rolemanager)
        {
            foreach (MyIdentityRoleNames rolename in Enum.GetValues(typeof(MyIdentityRoleNames)))
            {
                if (!await rolemanager.RoleExistsAsync(rolename.ToString()))
                {
                    await rolemanager.CreateAsync(
                        new IdentityRole { Name = rolename.ToString() });
                }
            }
        }

        public static async Task SeedIdentityUserAsync(UserManager<IdentityUser> usermanager)
        {
            IdentityUser user;

            user = await usermanager.FindByNameAsync("admin@RD.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@RD.com",
                    Email = "admin@RD.com",
                    EmailConfirmed = true
                };
                await usermanager.CreateAsync(user, password: "Rishabh@98");

                await usermanager.AddToRolesAsync(user, new string[] {
                    MyIdentityRoleNames.AppAdmin.ToString(),
                    MyIdentityRoleNames.AppUser.ToString()
                });
            }

            user = await usermanager.FindByNameAsync("user@RD.com");
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "user@RD.com",
                    Email = "user@RD.com",
                    EmailConfirmed = true
                };
                await usermanager.CreateAsync(user, password: "Rahul@98");
                //await usermanager.AddToRolesAsync(user, new string[] {
                //    MyIdentityRoleNames.AppUser.ToString()
                //});
                await usermanager.AddToRoleAsync(user, MyIdentityRoleNames.AppUser.ToString());
            }
        }
    }
}
