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
    public class EmploymentContractRepository
     : BaseRepository<EmploymentContract>, IEmploymentContractRepository
    {
        public EmploymentContractRepository(OrgFlowDbContext context)
            : base(context)
        {
        }

        public async Task<IReadOnlyList<EmploymentContract>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.StartDate)
                .ToListAsync();
        }

        public async Task<EmploymentContract?> GetCurrentForUserAsync(int userId)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId && c.IsCurrent);
        }

        public async Task SetCurrentContractAsync(int userId, EmploymentContract contract)
        {
            // poništi sve postojeće "current" za ovog usera
            var existingCurrent = await _dbSet
                .Where(c => c.UserId == userId && c.IsCurrent)
                .ToListAsync();

            foreach (var c in existingCurrent)
            {
                c.IsCurrent = false;
            }

            contract.IsCurrent = true;

            await _context.SaveChangesAsync();
        }
    }
}
