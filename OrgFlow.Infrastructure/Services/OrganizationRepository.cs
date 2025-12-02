using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OegFlow.Domain.DTOs;
using OrgFlow.Domain.Entites;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Infrastructure.Services
{
    public class OrganizationRepository
     : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(OrgFlowDbContext context)
            : base(context)
        {
        }

        public async Task<Organization?> GetByNameAsync(string name)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Name == name);
        }
    }
}