using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrgFlow.Domain.Entites;
using OrgFlow.Domain.Entities;

namespace OegFlow.Domain.Entities
{
    public class OfficeLocation : BaseEntity
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public List<User> Users { get; set; }
    }
}
