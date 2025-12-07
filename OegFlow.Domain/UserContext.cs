using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OegFlow.Domain
{
    public class UserContext
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public int DepartmentId { get; set; }
        public string Role { get; set; }
    }
}
