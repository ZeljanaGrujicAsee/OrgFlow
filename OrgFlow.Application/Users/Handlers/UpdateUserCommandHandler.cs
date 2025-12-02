using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Users.Commands;
using OrgFlow.Domain.Entities;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Users.Handlers
{
    public class UpdateUserCommandHandler
    : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IUserRepository _repo;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(
            IUserRepository repo,
            ILogger<UpdateUserCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<User> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var existing = await _repo.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"User {dto.Id} not found.");

            existing.DepartmentId = dto.DepartmentId;
            existing.TeamId = dto.TeamId;
            existing.PositionId = dto.PositionId;
            existing.OfficeLocationId = dto.OfficeLocationId;
            existing.ManagerId = dto.ManagerId;
            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.IsActive = dto.IsActive;

            await _repo.UpdateAsync(existing);
            _logger.LogInformation("User {Id} updated", existing.Id);

            return existing;
        }
    }
}
