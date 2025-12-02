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
    public class GetAllOfficeLocationsQueryHandler
     : IRequestHandler<GetAllOfficeLocationsQuery, IReadOnlyList<OfficeLocation>>
    {
        private readonly IOfficeLocationRepository _repo;
        private readonly ILogger<GetAllOfficeLocationsQueryHandler> _logger;

        public GetAllOfficeLocationsQueryHandler(
            IOfficeLocationRepository repo,
            ILogger<GetAllOfficeLocationsQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<OfficeLocation>> Handle(
            GetAllOfficeLocationsQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all office locations");
            return await _repo.GetAllAsync();
        }
    }
}
