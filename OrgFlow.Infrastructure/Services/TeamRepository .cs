using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OegFlow.Domain.Entities;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Infrastructure.Services
{
    public class TeamRepository
     : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(OrgFlowDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<Team>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(t => t.DepartmentId == departmentId && t.IsActive)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        // Sve CRUD metode (GetAllAsync, GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync)
        // dobijaš iz EfBaseRepository<Team> – ne moraš da ih pišeš ponovo.
    }
}
