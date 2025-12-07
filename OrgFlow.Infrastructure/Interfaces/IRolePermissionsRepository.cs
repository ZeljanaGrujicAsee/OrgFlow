using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OegFlow.Domain.Entities;

namespace OrgFlow.Infrastructure.Interfaces
{
    public interface IRolePermissionsRepository
    {
        Task<List<Permission>> GetRolePermissions(int roleId);
    }
}
