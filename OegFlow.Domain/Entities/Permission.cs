using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OegFlow.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!; // npr: "Organizations.Read.All"

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
