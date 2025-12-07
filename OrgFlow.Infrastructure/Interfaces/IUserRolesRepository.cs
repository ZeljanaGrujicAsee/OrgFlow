using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrgFlow.Domain.Entities;

namespace OrgFlow.Infrastructure.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<UserRole> GetByNameAsync(string name);
    }
}
