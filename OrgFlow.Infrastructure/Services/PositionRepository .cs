using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OegFlow.Domain.Entities;
using OegFlow.Domain.Enums;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Infrastructure.Services
{
    public class PositionRepository
     : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(OrgFlowDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<Position>> GetBySeniorityLevelAsync(SeniorityLevel seniorityLevel)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(p => p.SeniorityLevel == seniorityLevel && p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
