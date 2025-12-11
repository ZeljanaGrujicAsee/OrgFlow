using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Interfaces;
using OrgFlow.Application.Reports.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Application.Reports.Handlers
{
    public class ExportZipAsyncQueryHandler : IRequestHandler<ExportZipAsyncQuery, string>
    {

        private readonly IEmployeeExportService _service;
        private readonly ILogger<ExportZipAsyncQueryHandler> _logger;

        public ExportZipAsyncQueryHandler(ILogger<ExportZipAsyncQueryHandler> logger, IEmployeeExportService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<string> Handle(ExportZipAsyncQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Export employees to csv file started");
            var fileparth = await _service.GenerateFulExportZipAsync();
            return fileparth;
        }
    }
}
