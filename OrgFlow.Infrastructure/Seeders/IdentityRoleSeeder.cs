using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OrgFlow.Domain.Enums;

namespace OrgFlow.Infrastructure.Seeders
{
    public static class IdentityRoleSeeder
    {
        public static async Task Seed(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.Roles.Any())
                return;
            foreach (var role in Enum.GetNames(typeof(Role)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
