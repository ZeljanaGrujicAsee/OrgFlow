using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrgFlow.Domain.Entities;
using OrgFlow.Domain.Enums;

namespace OrgFlow.Infrastructure.Helpers
{
    public static class RoleEnumToTableMapper
    {
        public static List<UserRole> MapRoles()
        {
            return Enum.GetValues(typeof(Role))
                .Cast<Role>()
                .Select((value, index) => new UserRole
                {
                    Id = index + 1,
                    Name = value.ToString(),
                })
                .ToList();
        }
    }
}
