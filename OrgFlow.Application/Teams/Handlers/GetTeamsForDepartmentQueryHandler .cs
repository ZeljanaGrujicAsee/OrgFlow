using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OrgFlow.Application.Teams.Queries;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Teams.Handlers
{
    public class GetTeamsForDepartmentQueryHandler
       : IRequestHandler<GetTeamsForDepartmentQuery, IReadOnlyList<Team>>
    {
        private readonly ITeamRepository _repo;
        private readonly ILogger<GetTeamsForDepartmentQueryHandler> _logger;

        public GetTeamsForDepartmentQueryHandler(
            ITeamRepository repo,
            ILogger<GetTeamsForDepartmentQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Team>> Handle(
            GetTeamsForDepartmentQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching teams for department {DeptId}", request.DepartmentId);

            return await _repo.GetByDepartmentIdAsync(request.DepartmentId);
        }
    }
}
