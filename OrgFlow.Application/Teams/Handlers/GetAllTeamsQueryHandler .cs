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
    public class GetAllTeamsQueryHandler
    : IRequestHandler<GetAllTeamsQuery, IReadOnlyList<Team>>
    {
        private readonly ITeamRepository _repo;
        private readonly ILogger<GetAllTeamsQueryHandler> _logger;

        public GetAllTeamsQueryHandler(
            ITeamRepository repo,
            ILogger<GetAllTeamsQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Team>> Handle(
            GetAllTeamsQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all teams");
            return await _repo.GetAllAsync();
        }
    }
}
