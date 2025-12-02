using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OegFlow.Domain.DTOs
{
    public class CreatePositionDto
    {
        public string Name { get; set; } = string.Empty;
        public int SeniorityLevelId { get; set; }  
        public int DefaultVacationDays { get; set; }
    }

    public class UpdatePositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SeniorityLevelId { get; set; } 
        public int DefaultVacationDays { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
