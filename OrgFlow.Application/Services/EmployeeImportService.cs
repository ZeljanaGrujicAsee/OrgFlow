using Microsoft.AspNetCore.Http;
using OrgFlow.Application.Interfaces;
using OrgFlow.Domain.Entities;
using OrgFlow.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Application.Services
{
    public class EmployeeImportService : IEmployeeImportService
    {
        private readonly OrgFlowDbContext _db;

        public EmployeeImportService(OrgFlowDbContext db)
        {
            _db = db;
        }

        public async Task<(bool Success, string Message)> ImportEmployeesFromCsvAsync(IFormFile file)
        {
            // 1. Validacija
            if (file == null || file.Length == 0)
                return (false, "File is empty.");

            if (!file.FileName.EndsWith(".csv"))
                return (false, "Only CSV files are allowed.");

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream, Encoding.UTF8);

            int importedCount = 0;
            int lineNumber = 0;

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                lineNumber++;

                if (lineNumber == 1) continue; // skip header

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var columns = line.Split(',');

                if (columns.Length < 4)
                    return (false, $"Invalid format in line {lineNumber}");

                var user = new User
                {
                    FirstName = columns[0].Trim(),
                    LastName = columns[1].Trim(),
                    Email = columns[2].Trim(),
                    DepartmentId = int.TryParse(columns[3], out var dep) ? dep : null
                };

                await _db.Users.AddAsync(user);
                importedCount++;
            }

            await _db.SaveChangesAsync();

            return (true, $"{importedCount} employees imported successfully.");
        }


        public async Task<(bool Success, string Message)> ImportEmployeesFromCsvFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
                return (false, "File not found.");

            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(stream, Encoding.UTF8);

            int imported = 0;
            int lineNum = 0;

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                lineNum++;

                if (lineNum == 1) continue;
                if (string.IsNullOrWhiteSpace(line)) continue;

                var columns = line.Split(',');

                if (columns.Length < 3)
                    return (false, $"Invalid format at line {lineNum}");

                var user = new User
                {
                    FirstName = columns[0].Trim(),
                    LastName = columns[1].Trim(),
                    Email = columns[2].Trim(),
                    DepartmentId = int.TryParse(columns.ElementAtOrDefault(3), out var dep) ? dep : null
                };

                await _db.Users.AddAsync(user);
                imported++;
            }

            await _db.SaveChangesAsync();

            return (true, $"{imported} users imported.");
        }

    }
}
