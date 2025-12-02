using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OegFlow.Domain.Entities;
using OrgFlow.Domain.Entites;

namespace OrgFlow.Domain.Entities
{
    public class User : BaseEntity
    {


        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        public int? PositionId { get; set; }
        public Position? Position { get; set; }

        public int? OfficeLocationId { get; set; }
        public OfficeLocation? OfficeLocation { get; set; }

        public int? ManagerId { get; set; }
        public User? Manager { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        // Navigacije
        public ICollection<User> DirectReports { get; set; } = new List<User>();
        public ICollection<EmploymentContract> EmploymentContracts { get; set; } = new List<EmploymentContract>();
        // ovde mogu i Requests, Approvals, Comments, Notifications...

    }
}
