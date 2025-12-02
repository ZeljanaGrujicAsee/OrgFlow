using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.EmploymentContracts.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.EmploymentContracts.Handlers
{
    public class DeleteEmploymentContractCommandHandler
      : IRequestHandler<DeleteEmploymentContractCommand>
    {
        private readonly IEmploymentContractRepository _repo;
        private readonly ILogger<DeleteEmploymentContractCommandHandler> _logger;

        public DeleteEmploymentContractCommandHandler(
            IEmploymentContractRepository repo,
            ILogger<DeleteEmploymentContractCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task Handle(
            DeleteEmploymentContractCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null)
            {
                _logger.LogWarning("Employment contract {Id} not found for deletion", request.Id);
                return;
            }

            await _repo.DeleteAsync(existing.Id);
            _logger.LogInformation("Employment contract {Id} deleted", request.Id);
        }
    }
}
