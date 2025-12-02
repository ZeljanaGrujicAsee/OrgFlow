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
    public class CreateOrganizationCommandHandler
     : IRequestHandler<CreateOrganizationCommand, Organization>
    {
        private readonly IOrganizationRepository _repo;
        private readonly ILogger<CreateOrganizationCommandHandler> _logger;

        public CreateOrganizationCommandHandler(
            IOrganizationRepository repo,
            ILogger<CreateOrganizationCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Organization> Handle(
            CreateOrganizationCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Organization name is required.");

            var existing = await _repo.GetByNameAsync(dto.Name);
            if (existing != null)
                throw new InvalidOperationException($"Organization '{dto.Name}' already exists.");

            var entity = new Organization
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = true
            };

            await _repo.AddAsync(entity);
            _logger.LogInformation("Organization {Id} created", entity.Id);

            return entity;
        }
    }
}
