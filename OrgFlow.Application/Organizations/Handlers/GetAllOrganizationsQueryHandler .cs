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
    public class GetAllOrganizationsQueryHandler
      : IRequestHandler<GetAllOrganizationsQuery, IReadOnlyList<Organization>>
    {
        private readonly IOrganizationRepository _repo;
        private readonly ILogger<GetAllOrganizationsQueryHandler> _logger;

        public GetAllOrganizationsQueryHandler(
            IOrganizationRepository repo,
            ILogger<GetAllOrganizationsQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Organization>> Handle(
            GetAllOrganizationsQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all organizations");
            return await _repo.GetAllAsync();
        }
    }
}
