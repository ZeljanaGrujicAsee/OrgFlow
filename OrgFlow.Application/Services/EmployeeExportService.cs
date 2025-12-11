using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Application.Services
{
    using Microsoft.EntityFrameworkCore;
    using OrgFlow.Application.Interfaces;
    using OrgFlow.Domain.Entities;
    using OrgFlow.Infrastructure;
    using OrgFlow.Infrastructure.Interfaces;
    using System.IO.Compression;
    using System.Text;
    using System.Text.Json;

    public class EmployeeExportService : IEmployeeExportService
    {
        private readonly IUserRepository _userRepository;
        public EmployeeExportService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> ExportEmployeeToJsonAsync()
        {
            var employees = await _userRepository.GetAllAsync();
            string tempFilePath = Path.Combine(Path.GetTempPath(),
                $"employees_{Guid.NewGuid()}.json");

            await using var stream = File.Create(tempFilePath);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            await JsonSerializer.SerializeAsync(stream, employees, options);

            return tempFilePath;
        }

        public async Task<string> ExportEmployeeToCsvAsync(string separator)
        {
            var employees = await _userRepository.GetAllAsync();
            string tempFilePath = Path.Combine(Path.GetTempPath(),
                $"employees_{Guid.NewGuid()}.csv");

            await using var writer = new StreamWriter(tempFilePath, false, Encoding.UTF8);
            //Header
            await writer.WriteLineAsync("Id, FirstName, LastName, Email, DepartmentID, PositionId, TeamId");

            //Rows

            foreach (var e in employees)
            {
                await writer.WriteLineAsync(
                    $"{e.Id}{separator}{e.FirstName}{separator}{e.LastName}{separator}{e.Email}{separator}{e.DepartmentId}{separator}{e.PositionId}{separator}{e.TeamId}"
                    );
            }

            return tempFilePath;
        }


        public async Task<string> GenerateFulExportZipAsync()
        {
            //Kreiramo privremeni folder
            string tempFolder = Path.Combine(Path.GetTempPath(), $"exployee_export_{Guid.NewGuid()}");
            Directory.CreateDirectory(tempFolder);

            //Kreiramo podatke u razlicitim fajlovima
            string jsonPath = await ExportEmployeeToJsonAsync();
            string csvPath = await ExportEmployeeToCsvAsync(",");

            //Kopiramo fajlove u temp folder
            string jsonDest = Path.Combine(tempFolder, "employees.json");
            string csvDest = Path.Combine(tempFolder, "employees.csv");

            File.Copy(jsonPath, jsonDest);
            File.Copy(csvPath, csvDest);

            //Kreiramo zip u temp folderu
            string zipPath = Path.Combine(Path.GetTempPath(), $"OrgFlow_FullExport_{Guid.NewGuid()}.zip");

            ZipFile.CreateFromDirectory(tempFolder, zipPath);

            Directory.Delete(tempFolder, true);

            return zipPath;
        }
    }

}
