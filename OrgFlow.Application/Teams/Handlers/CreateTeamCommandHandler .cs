using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OrgFlow.Application.Teams.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Teams.Handlers
{
    public class CreateTeamCommandHandler
      : IRequestHandler<CreateTeamCommand, Team>
    {
        private readonly ITeamRepository _repo;
        private readonly ILogger<CreateTeamCommandHandler> _logger;

        public CreateTeamCommandHandler(
            ITeamRepository repo,
            ILogger<CreateTeamCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Team> Handle(
            CreateTeamCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Team name is required.");

            var team = new Team
            {
                DepartmentId = dto.DepartmentId,
                Name = dto.Name,
                TeamLeadId = dto.TeamLeadId,
                IsActive = true
            };

            await _repo.AddAsync(team);

            _logger.LogInformation("Team {Id} created in department {DeptId}", team.Id, team.DepartmentId);

            return team;
        }
    }
}
