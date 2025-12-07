using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrgFlow.Domain.Entities;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Infrastructure.Services
{
    public class UserRolesRepository : BaseRepository<UserRole>, IUserRolesRepository
    {
        public UserRolesRepository(OrgFlowDbContext context)
          : base(context)
        {
        }
        public async Task<UserRole> GetByNameAsync(string name)
        {
           return await _context.UserRoles.Where(r => r.Name == name).FirstOrDefaultAsync();
        }
    }
}
