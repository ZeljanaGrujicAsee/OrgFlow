using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OegFlow.Domain.Entities;
using OrgFlow.Domain.Entites;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Infrastructure.Services
{
    public class OfficeLocationRepository
          : BaseRepository<OfficeLocation>, IOfficeLocationRepository
    {
        public OfficeLocationRepository(OrgFlowDbContext context)
          : base(context)   // prosleđujemo DbContext baznoj klasi
        {
        }

        public async Task<OfficeLocation> GetByOrganizationIdAsync(int organizationId)
        {
            return await _dbSet
              .AsNoTracking()
              .Include(o => o.Organization)
              .FirstOrDefaultAsync(d => d.OrganizationId == organizationId && d.IsActive);
        }
    }
}
