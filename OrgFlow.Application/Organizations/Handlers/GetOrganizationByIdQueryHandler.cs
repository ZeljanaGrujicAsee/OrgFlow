using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Organizations.Queries;
using OrgFlow.Domain.Entites;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Organizations.Handlers
{
    public class GetOrganizationByIdQueryHandler
     : IRequestHandler<GetOrganizationByIdQuery, Organization?>
    {
        private readonly IOrganizationRepository _repo;
        private readonly ILogger<GetOrganizationByIdQueryHandler> _logger;

        public GetOrganizationByIdQueryHandler(
            IOrganizationRepository repo,
            ILogger<GetOrganizationByIdQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Organization?> Handle(
            GetOrganizationByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching organization {Id}", request.Id);
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
