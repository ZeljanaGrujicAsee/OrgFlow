using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OegFlow.Domain.DTOs
{
    public class CreateTeamDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? TeamLeadId { get; set; }
    }

    public class UpdateTeamDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int? TeamLeadId { get; set; }
    }
}
