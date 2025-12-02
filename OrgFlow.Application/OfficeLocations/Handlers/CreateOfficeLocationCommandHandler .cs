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
    public class CreateOfficeLocationCommandHandler
     : IRequestHandler<CreateOfficeLocationCommand, OfficeLocation>
    {
        private readonly IOfficeLocationRepository _repo;
        private readonly ILogger<CreateOfficeLocationCommandHandler> _logger;

        public CreateOfficeLocationCommandHandler(
            IOfficeLocationRepository repo,
            ILogger<CreateOfficeLocationCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<OfficeLocation> Handle(
            CreateOfficeLocationCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Office location name is required.");

            if (string.IsNullOrWhiteSpace(dto.Address))
                throw new ArgumentException("Address is required.");

            if (string.IsNullOrWhiteSpace(dto.TimeZone))
                throw new ArgumentException("TimeZone is required.");

            var location = new OfficeLocation
            {
                OrganizationId = dto.OrganizationId,
                Name = dto.Name,
                Address = dto.Address,
                TimeZone = dto.TimeZone,
                IsActive = true
            };

            await _repo.AddAsync(location);

            _logger.LogInformation("OfficeLocation {Id} created for Organization {OrgId}", location.Id, location.OrganizationId);

            return location;
        }
    }
}
