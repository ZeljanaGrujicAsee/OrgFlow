using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OegFlow.Domain.DTOs
{
    public class CreateUserDto
    {

        public int? DepartmentId { get; set; }
        public int? TeamId { get; set; }
        public int? PositionId { get; set; }
        public int? OfficeLocationId { get; set; }
        public int? ManagerId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UpdateUserDto
    {
        public int Id { get; set; }


        public int? DepartmentId { get; set; }
        public int? TeamId { get; set; }
        public int? PositionId { get; set; }
        public int? OfficeLocationId { get; set; }
        public int? ManagerId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
