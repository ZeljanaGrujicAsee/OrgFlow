using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OrgFlow.Application.OfficeLocations.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.OfficeLocations.Handlers
{
    public class UpdateOfficeLocationCommandHandler
     : IRequestHandler<UpdateOfficeLocationCommand, OfficeLocation>
    {
        private readonly IOfficeLocationRepository _repo;
        private readonly ILogger<UpdateOfficeLocationCommandHandler> _logger;

        public UpdateOfficeLocationCommandHandler(
            IOfficeLocationRepository repo,
            ILogger<UpdateOfficeLocationCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<OfficeLocation> Handle(
            UpdateOfficeLocationCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var existing = await _repo.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"OfficeLocation {dto.Id} not found.");

            existing.OrganizationId = dto.OrganizationId;
            existing.Name = dto.Name;
            existing.Address = dto.Address;
            existing.TimeZone = dto.TimeZone;
            existing.IsActive = dto.IsActive;

            await _repo.UpdateAsync(existing);

            _logger.LogInformation("OfficeLocation {Id} updated", existing.Id);

            return existing;
        }
    }
}
