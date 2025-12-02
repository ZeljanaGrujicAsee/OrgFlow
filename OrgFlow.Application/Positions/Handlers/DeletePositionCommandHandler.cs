using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Positions.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Positions.Handlers
{
    public class DeletePositionCommandHandler
     : IRequestHandler<DeletePositionCommand>
    {
        private readonly IPositionRepository _repo;
        private readonly ILogger<DeletePositionCommandHandler> _logger;

        public DeletePositionCommandHandler(
            IPositionRepository repo,
            ILogger<DeletePositionCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task Handle(
            DeletePositionCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null)
            {
                _logger.LogWarning("Position {Id} not found for deletion", request.Id);
                return;
            }

            await _repo.DeleteAsync(existing.Id);

            _logger.LogInformation("Position {Id} deleted", request.Id);
        }
    }

}
