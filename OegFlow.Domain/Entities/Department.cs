using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrgFlow.Domain.Entites;
using OrgFlow.Domain.Entities;

namespace OegFlow.Domain.Entities
{
    public class Department  :BaseEntity
    {
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigations
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
