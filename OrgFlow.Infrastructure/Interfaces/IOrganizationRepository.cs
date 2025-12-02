using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OegFlow.Domain.DTOs;
using OrgFlow.Domain.Entites;

namespace OrgFlow.Infrastructure.Interfaces
{
    public interface IOrganizationRepository : IBaseRepository<Organization>
    {
        Task<Organization?> GetByNameAsync(string name);
    }
}
