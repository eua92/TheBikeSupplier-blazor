using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBikeSupplier.Common;
using TheBikeSupplier.Models;

namespace TheBikeSupplier.Web.Server.Data
{
    public static class DataSeed
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roles = new ApplicationRoles();
            await CreateRoles(roleManager, roles);
            await CreateAdminUser(userManager);
        }

        private async static Task CreateRoles(RoleManager<ApplicationRole> roleManager, ApplicationRoles roles)
        {
            // admin
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                var role = roles.RoleList.First(x => x.Name == Roles.Admin);
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception();
                }
            }

            // user
            if (!await roleManager.RoleExistsAsync(Roles.User))
            {
                var role = roles.RoleList.First(x => x.Name == Roles.User);
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception();
                }
            }

            // support
            if (!await roleManager.RoleExistsAsync(Roles.Support))
            {
                var role = roles.RoleList.First(x => x.Name == Roles.Support);
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception();
                }
            }
        }

        private async static Task CreateAdminUser(UserManager<ApplicationUser> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@example.com") == null)
            {
                var administratorUser = new ApplicationUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com"
                };

                var result = await userManager.CreateAsync(administratorUser, "Aa1234!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(administratorUser, Roles.Admin);
                }
            }
        }
    }
}
