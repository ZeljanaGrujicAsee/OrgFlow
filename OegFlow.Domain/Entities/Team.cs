using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrgFlow.Domain.Entities;

namespace OegFlow.Domain.Entities
{
    public class Team : BaseEntity
    {
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? TeamLeadId { get; set; }
        public User? TeamLead { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigations
        public ICollection<User> Members { get; set; } = new List<User>();
    }
}
