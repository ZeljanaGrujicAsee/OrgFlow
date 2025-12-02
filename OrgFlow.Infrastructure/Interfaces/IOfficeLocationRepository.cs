using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OegFlow.Domain.Entities;
using OrgFlow.Domain.Entites;

namespace OrgFlow.Infrastructure.Interfaces
{
    public interface IOfficeLocationRepository : IBaseRepository<OfficeLocation>
    {
        Task<OfficeLocation> GetByOrganizationIdAsync(int organizationId);
    }
}
