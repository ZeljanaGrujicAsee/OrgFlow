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
    public class GetOrganizationByNameQueryHandler
      : IRequestHandler<GetOrganizationByNameQuery, Organization?>
    {
        private readonly IOrganizationRepository _repo;
        private readonly ILogger<GetOrganizationByNameQueryHandler> _logger;

        public GetOrganizationByNameQueryHandler(
            IOrganizationRepository repo,
            ILogger<GetOrganizationByNameQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Organization?> Handle(
            GetOrganizationByNameQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching organization by name {Name}", request.Name);
            return await _repo.GetByNameAsync(request.Name);
        }
    }
}
