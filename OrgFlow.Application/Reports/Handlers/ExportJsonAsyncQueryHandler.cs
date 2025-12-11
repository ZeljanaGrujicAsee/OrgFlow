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
    public class ExportJsonAsyncQueryHandler : IRequestHandler<ExportJsonAsyncQuery, string>
    {
        private readonly IEmployeeExportService _service;
        private readonly ILogger<ExportJsonAsyncQueryHandler> _logger;


        public ExportJsonAsyncQueryHandler(IEmployeeExportService service, ILogger<ExportJsonAsyncQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<string> Handle(ExportJsonAsyncQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Export employees to csv file started");
            var fileparth = await _service.ExportEmployeeToJsonAsync();
            return fileparth;
        }
    }
}
