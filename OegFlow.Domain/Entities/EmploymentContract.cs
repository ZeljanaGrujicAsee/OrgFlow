using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrgFlow.Domain.Entities;

namespace OegFlow.Domain.Entities
{
    public class EmploymentContract : BaseEntity
    {

        public int UserId { get; set; }
        public User? User { get; set; }

        public string ContractType { get; set; } = string.Empty; // FullTime, PartTime, B2B...
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public decimal SalaryGross { get; set; }
        public string Currency { get; set; } = "EUR";
    }
}
