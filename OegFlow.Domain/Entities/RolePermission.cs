using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OrgFlow.Domain.Entities;

namespace OegFlow.Domain.Entities
{
    public class RolePermission
    {
        public int RolePermissionId { get; set; }

        public int RoleId { get; set; } = default!;
        public UserRole Role { get; set; } = default!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = default!;
    }
}
