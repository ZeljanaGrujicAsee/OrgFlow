using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OrgFlow.Application.OfficeLocations.Queries;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.OfficeLocations.Handlers
{
    public class GetOfficeLocationsByOrganizationQueryHandler
    : IRequestHandler<GetOfficeLocationsByOrganizationQuery, OfficeLocation>
    {
        private readonly IOfficeLocationRepository _repo;
        private readonly ILogger<GetOfficeLocationsByOrganizationQueryHandler> _logger;

        public GetOfficeLocationsByOrganizationQueryHandler(
            IOfficeLocationRepository repo,
            ILogger<GetOfficeLocationsByOrganizationQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<OfficeLocation> Handle(
            GetOfficeLocationsByOrganizationQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching office locations for organization {OrgId}", request.OrganizationId);

            return await _repo.GetByOrganizationIdAsync(request.OrganizationId);
        }
    }
}
