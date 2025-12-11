using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Application.Interfaces
{
    public interface IEmployeeExportService
    {
        Task<string> ExportEmployeeToJsonAsync();
        Task<string> ExportEmployeeToCsvAsync(string separator);
        Task<string> GenerateFulExportZipAsync();
    }
}
