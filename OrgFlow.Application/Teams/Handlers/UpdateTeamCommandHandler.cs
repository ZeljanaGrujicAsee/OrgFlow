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
    public class UpdateTeamCommandHandler
      : IRequestHandler<UpdateTeamCommand, Team>
    {
        private readonly ITeamRepository _repo;
        private readonly ILogger<UpdateTeamCommandHandler> _logger;

        public UpdateTeamCommandHandler(
            ITeamRepository repo,
            ILogger<UpdateTeamCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Team> Handle(
            UpdateTeamCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var existing = await _repo.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Team {dto.Id} not found.");

            existing.DepartmentId = dto.DepartmentId;
            existing.Name = dto.Name;
            existing.IsActive = dto.IsActive;
            existing.TeamLeadId = dto.TeamLeadId;

            await _repo.UpdateAsync(existing);

            _logger.LogInformation("Team {Id} updated", existing.Id);

            return existing;
        }
    }
}
