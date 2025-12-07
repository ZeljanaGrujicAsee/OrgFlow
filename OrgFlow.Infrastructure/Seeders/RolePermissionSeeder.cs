using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OegFlow.Domain.Entities;
using OrgFlow.Domain.Entities;
using OrgFlow.Domain.Enums;

namespace OrgFlow.Infrastructure.Seeders
{
    public class RolePermissionSeeder
    {
        public static async Task Seed(RoleManager<IdentityRole> roleManager, OrgFlowDbContext db)
        {
            if (await db.RolePermissions.AnyAsync())
                return;
            var admin = await db.UserRoles.Where(x => x.Id.Equals((int)Role.Admin)).FirstOrDefaultAsync();
            var manager = await db.UserRoles.Where(x => x.Id.Equals((int)Role.Manager)).FirstOrDefaultAsync();
            var lead = await db.UserRoles.Where(x => x.Id.Equals((int)Role.Lead)).FirstOrDefaultAsync();
            var employee = await db.UserRoles.Where(x => x.Id.Equals((int)Role.Employee)).FirstOrDefaultAsync();

            var permissions = db.Permissions.ToList();

            await AssignAllPermissions(admin, permissions, db);
            await AssignOrganizationPermissions(manager, permissions, db);
            await AssignDepartmentPermissions(lead, permissions, db);
            await AssignEmplyeePermissions(employee, permissions, db);
        }

        private static async Task AssignAllPermissions(UserRole role, List<Permission> permissions, OrgFlowDbContext db)
        {
            foreach (var p in permissions)
            {
                if (!db.RolePermissions.Any(rp => rp.RoleId == role.Id && rp.PermissionId == p.Id))
                    db.RolePermissions.Add(new RolePermission { RoleId = role.Id, PermissionId = p.Id });
            }
            await db.SaveChangesAsync();
        }

        private static async Task AssignOrganizationPermissions(UserRole role, List<Permission> permissions, OrgFlowDbContext db)
        {
            var allowed = permissions.Where(p => p.Name.Contains("Organization") && p.Name.Contains("Own"));
            foreach (var p in allowed)
                db.RolePermissions.Add(new RolePermission { RoleId = role.Id, PermissionId = p.Id });
            await db.SaveChangesAsync();
        }

        private static async Task AssignDepartmentPermissions(UserRole role, List<Permission> permissions, OrgFlowDbContext db)
        {
            var allowed = permissions.Where(p => (p.Name.Contains("Own") && p.Name.Contains("Department")) || p.Name.Contains("Requests"));
            foreach (var p in allowed)
                db.RolePermissions.Add(new RolePermission { RoleId = role.Id, PermissionId = p.Id });
            await db.SaveChangesAsync();
        }

        private static async Task AssignEmplyeePermissions(UserRole role, List<Permission> permissions, OrgFlowDbContext db)
        {
            var allowed = permissions.Where(p => p.Name.StartsWith("Requests.Create") || p.Name.StartsWith("Requests.Read.Own"));
            foreach (var p in allowed)
                db.RolePermissions.Add(new RolePermission { RoleId = role.Id, PermissionId = p.Id });
            await db.SaveChangesAsync();
        }
    }
}
