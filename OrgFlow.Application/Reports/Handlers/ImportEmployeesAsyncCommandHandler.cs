using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Interfaces;
using OrgFlow.Application.Reports.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Application.Reports.Handlers
{
    public class ImportEmployeesAsyncCommandHandler : IRequestHandler<ImportEmployeesAsyncCommand, string>
    {
        private readonly IEmployeeImportService _importService;
        private readonly ILogger<ImportEmployeesAsyncCommandHandler> _logger;

        public ImportEmployeesAsyncCommandHandler(IEmployeeImportService importService, ILogger<ImportEmployeesAsyncCommandHandler> logger)
        {
            _importService = importService;
            _logger = logger;
        }

        public async Task<string> Handle(ImportEmployeesAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = await _importService.ImportEmployeesFromCsvAsync(request.file);

            return result.Message;
        }
    }
}
