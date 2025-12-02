using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Teams.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Teams.Handlers
{
    public class DeleteTeamCommandHandler
      : IRequestHandler<DeleteTeamCommand>
    {
        private readonly ITeamRepository _repo;
        private readonly ILogger<DeleteTeamCommandHandler> _logger;

        public DeleteTeamCommandHandler(
            ITeamRepository repo,
            ILogger<DeleteTeamCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task Handle(
            DeleteTeamCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null)
            {
                _logger.LogWarning("Team {Id} not found for deletion", request.Id);
                return;
            }

            await _repo.DeleteAsync(existing.Id);
            _logger.LogInformation("Team {Id} deleted", request.Id);
        }
    }
}
