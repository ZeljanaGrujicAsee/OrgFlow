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
    public class GetOfficeLocationByIdQueryHandler
      : IRequestHandler<GetOfficeLocationByIdQuery, OfficeLocation?>
    {
        private readonly IOfficeLocationRepository _repo;
        private readonly ILogger<GetOfficeLocationByIdQueryHandler> _logger;

        public GetOfficeLocationByIdQueryHandler(
            IOfficeLocationRepository repo,
            ILogger<GetOfficeLocationByIdQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<OfficeLocation?> Handle(
            GetOfficeLocationByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching office location {Id}", request.Id);
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
