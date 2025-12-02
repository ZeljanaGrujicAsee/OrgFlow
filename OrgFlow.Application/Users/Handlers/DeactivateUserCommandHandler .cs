using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Users.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Users.Handlers
{
    public class DeactivateUserCommandHandler
      : IRequestHandler<DeactivateUserCommand>
    {
        private readonly IUserRepository _repo;
        private readonly ILogger<DeactivateUserCommandHandler> _logger;

        public DeactivateUserCommandHandler(
            IUserRepository repo,
            ILogger<DeactivateUserCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task Handle(
            DeactivateUserCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null)
            {
                _logger.LogWarning("User {Id} not found for deactivation", request.Id);
                return;
            }

            existing.IsActive = false;
            await _repo.UpdateAsync(existing);

            _logger.LogInformation("User {Id} deactivated", request.Id);
        }
    }

}
