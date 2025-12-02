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
    public class DepartmentRepository
    : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(OrgFlowDbContext context)
            : base(context)   // prosleđujemo DbContext baznoj klasi
        {
        }

        // Specifičan query za Department
        public async Task<IReadOnlyList<Department>> GetByOrganizationIdAsync(int organizationId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(d => d.OrganizationId == organizationId && d.IsActive)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }

        // Sve ostalo (GetAllAsync, GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync)
        // NASLEĐUJES iz BaseRepository<Department>, ne moraš ništa da pišeš.
    }
}
