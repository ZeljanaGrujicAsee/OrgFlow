using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OrgFlow.Application.EmploymentContracts.Queries;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.EmploymentContracts.Handlers
{
    public class GetEmploymentContractByIdQueryHandler
      : IRequestHandler<GetEmploymentContractByIdQuery, EmploymentContract?>
    {
        private readonly IEmploymentContractRepository _repo;
        private readonly ILogger<GetEmploymentContractByIdQueryHandler> _logger;

        public GetEmploymentContractByIdQueryHandler(
            IEmploymentContractRepository repo,
            ILogger<GetEmploymentContractByIdQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<EmploymentContract?> Handle(
            GetEmploymentContractByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching employment contract {Id}", request.Id);
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
