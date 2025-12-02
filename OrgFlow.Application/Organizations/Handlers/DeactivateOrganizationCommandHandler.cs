using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Organizations.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Organizations.Handlers
{
    public class DeactivateOrganizationCommandHandler
      : IRequestHandler<DeactivateOrganizationCommand>
    {
        private readonly IOrganizationRepository _repo;
        private readonly ILogger<DeactivateOrganizationCommandHandler> _logger;

        public DeactivateOrganizationCommandHandler(
            IOrganizationRepository repo,
            ILogger<DeactivateOrganizationCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task Handle(
            DeactivateOrganizationCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing == null)
            {
                _logger.LogWarning("Organization {Id} not found for deactivation", request.Id);
                return;
            }

            existing.IsActive = false;
            await _repo.UpdateAsync(existing);

            _logger.LogInformation("Organization {Id} deactivated", request.Id);
        }
    }
}
