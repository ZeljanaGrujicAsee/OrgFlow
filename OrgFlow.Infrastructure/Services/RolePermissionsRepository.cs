using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OegFlow.Domain.Entities;
using OrgFlow.Domain.Entities;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Infrastructure.Services
{
    public class RolePermissionsRepository
        : BaseRepository<RolePermission>, IRolePermissionsRepository
    {
        private readonly OrgFlowDbContext _context;

        public RolePermissionsRepository(OrgFlowDbContext context)
         : base(context)
        {
            _context = context;
        }

        public async Task<List<Permission>> GetRolePermissions(int roleId)
        {
            return await _context.RolePermissions.Include(rp => rp.Permission).Where(rp => rp.RoleId == roleId).Select(rp => rp.Permission).ToListAsync();



           
        }
    }
}
