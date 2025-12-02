using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OegFlow.Domain.Entities;

namespace OrgFlow.Infrastructure.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        // Ovde dodaješ specifične metode za Department
        Task<IReadOnlyList<Department>> GetByOrganizationIdAsync(int organizationId);
    }
}
