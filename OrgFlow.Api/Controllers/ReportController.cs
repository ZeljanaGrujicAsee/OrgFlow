using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrgFlow.Application.Interfaces;
using OrgFlow.Application.Reports.Commands;
using OrgFlow.Application.Reports.Queries;

namespace OrgFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("employees/export/csv")]
        public async Task<IActionResult> ExportCsvAsync(string separator)
        {
            var path = await _mediator.Send(new ExportCsvAsyncQuery(separator));
            return PhysicalFile(path, "text/csv", "employees.csv");
        }

        [HttpGet("employees/export/json")]
        public async Task<IActionResult> ExportJsonAsync()
        {
            var path = await _mediator.Send(new ExportJsonAsyncQuery());
            return PhysicalFile(path, "application/json", "employees.json");
        }

        [HttpGet("employees/full-export-zip")]
        public async Task<IActionResult> ExportZipAsync()
        {
            var path = await _mediator.Send(new ExportZipAsyncQuery());
            return PhysicalFile(path, "application/zip", "employees.zip");
        }

        [HttpPost("employees/import")]
        public async Task<IActionResult> ImportEmployeesAsync(IFormFile file)
        {
            var result = await _mediator.Send(new ImportEmployeesAsyncCommand(file));

            return Ok(result);

        }
    }

}
