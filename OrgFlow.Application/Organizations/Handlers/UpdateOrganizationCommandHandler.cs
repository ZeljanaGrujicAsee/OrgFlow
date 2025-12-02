using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Organizations.Commands;
using OrgFlow.Domain.Entites;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Organizations.Handlers
{

    public class UpdateOrganizationCommandHandler
        : IRequestHandler<UpdateOrganizationCommand, Organization>
    {
        private readonly IOrganizationRepository _repo;
        private readonly ILogger<UpdateOrganizationCommandHandler> _logger;

        public UpdateOrganizationCommandHandler(
            IOrganizationRepository repo,
            ILogger<UpdateOrganizationCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Organization> Handle(
            UpdateOrganizationCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var existing = await _repo.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Organization {dto.Id} not found.");

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.IsActive = dto.IsActive;

            await _repo.UpdateAsync(existing);
            _logger.LogInformation("Organization {Id} updated", existing.Id);

            return existing;
        }
    }
}
