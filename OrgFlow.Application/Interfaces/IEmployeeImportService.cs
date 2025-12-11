using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Application.Interfaces
{
    public interface IEmployeeImportService
    {
        Task<(bool Success, string Message)> ImportEmployeesFromCsvFileAsync(string filePath);
        Task<(bool Success, string Message)> ImportEmployeesFromCsvAsync(IFormFile file);

    }

}
