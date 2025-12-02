using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OegFlow.Domain.Entities;

namespace OrgFlow.Infrastructure.Interfaces
{
    public interface ITeamRepository : IBaseRepository<Team>
    {
        // Specifične metode za Team
        Task<IReadOnlyList<Team>> GetByDepartmentIdAsync(int departmentId);
    }
}
