using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OegFlow.Domain.DTOs
{
    public class CreateEmploymentContractDto
    {
        public int UserId { get; set; }
        public string ContractType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public decimal SalaryGross { get; set; }
        public string Currency { get; set; } = "EUR";
    }

    public class UpdateEmploymentContractDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ContractType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public decimal SalaryGross { get; set; }
        public string Currency { get; set; } = "EUR";
    }
}
