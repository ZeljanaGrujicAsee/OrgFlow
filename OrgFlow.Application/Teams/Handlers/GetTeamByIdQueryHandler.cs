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
    public class GetTeamByIdQueryHandler
      : IRequestHandler<GetTeamByIdQuery, Team?>
    {
        private readonly ITeamRepository _repo;
        private readonly ILogger<GetTeamByIdQueryHandler> _logger;

        public GetTeamByIdQueryHandler(
            ITeamRepository repo,
            ILogger<GetTeamByIdQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Team?> Handle(
            GetTeamByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching team {Id}", request.Id);
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
