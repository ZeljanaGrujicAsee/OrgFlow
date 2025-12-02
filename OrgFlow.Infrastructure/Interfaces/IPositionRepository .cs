using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OegFlow.Domain.Entities;
using OegFlow.Domain.Enums;

namespace OrgFlow.Infrastructure.Interfaces
{
    public interface IPositionRepository : IBaseRepository<Position>
    {
        // Po želji – specifične metode
        Task<IReadOnlyList<Position>> GetBySeniorityLevelAsync(SeniorityLevel seniorityLevel);
    }
}
