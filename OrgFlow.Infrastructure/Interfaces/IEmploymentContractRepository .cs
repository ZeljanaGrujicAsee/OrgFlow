using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OegFlow.Domain.Entities;

namespace OrgFlow.Infrastructure.Interfaces
{
    public interface IEmploymentContractRepository : IBaseRepository<EmploymentContract>
    {
        Task<IReadOnlyList<EmploymentContract>> GetByUserIdAsync(int userId);
        Task<EmploymentContract?> GetCurrentForUserAsync(int userId);
        /*SetCurrentContractAsync ćemo iskoristiti da:

sve ostale ugovore za tog usera setuje IsCurrent = false

ovaj jedan setuje IsCurrent = true*/
        Task SetCurrentContractAsync(int userId, EmploymentContract contract);
    }
}
