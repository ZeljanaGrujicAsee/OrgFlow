using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrgFlow.Infrastructure.Helpers;

namespace OrgFlow.Infrastructure.Seeders
{
    public static class RoleEntitySeeder
    {
        public static async Task Seed(OrgFlowDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return; // već ima podataka – PRESKOČI
            var enumRoles = RoleEnumToTableMapper.MapRoles();

            foreach (var role in enumRoles)
            {
                var exists = await context.UserRoles.AnyAsync(r => r.Id == role.Id);
                if (!exists)
                {
                    await context.UserRoles.AddAsync(role);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
