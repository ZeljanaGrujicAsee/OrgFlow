using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OegFlow.Domain.Enums;
using OrgFlow.Domain.Entities;

namespace OegFlow.Domain.Entities
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }    
        public SeniorityLevel SeniorityLevel { get; set; }   
        public int DefaultVacationDays { get; set; }   
        public bool IsActive { get; set; } 
        
        public List<User>  Users { get; set; }
    }
}
